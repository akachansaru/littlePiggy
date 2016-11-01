using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float maxHp;
	public float attackRate;
	public int damage;
	public int touchDamage;
	public float speed;
	public float attackRange;
	public float agroRange;
	public Vector2 initialDirection;
	public int baseDonutDrop;
	public float clothDropChance;
	public float upgradeDropChance;
	public GameObject platform;

	private Rigidbody2D rb;
	private Vector2 direction;
	private float rightEdge;
	private float leftEdge;
	private bool following;
	private float currentHp;
	private float nextAttackTime;
	private ArrayList spawnList;
	private bool isDead;
	private float deadDelayTime; // Time between losing all hitpoints and the body disapearing
	private Animator anim;
	private static bool enemyAtEdge;

	public bool IsDead {
		get { return isDead; }
		set { isDead = value; }
	}

	public static bool EnemyAtEdge {
		get { return enemyAtEdge; }
		set { enemyAtEdge = value; }
	}

	public void ModifySpeed(float speedModifier) {
		speed *= speedModifier;
	}

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		rightEdge = platform.GetComponent<Renderer>().bounds.max.x;
		leftEdge = platform.GetComponent<Renderer>().bounds.min.x;
		direction = initialDirection;
		following = false;
		spawnList = new ArrayList();
		isDead = false;
		deadDelayTime = 2f;
//		nextAttackTime = float.MaxValue;
		// TODO Make touch damage work again
		nextAttackTime = 0f;
		currentHp = maxHp;

		// Turn to walk in correct direction
		if (direction == Vector2.left) {
			anim.SetTrigger(ConstantValues.enemyAnimatorParameterNames.turn);
		}
	}
	
	void FixedUpdate() {
		Move();
	}

	void OnCollisionEnter2D(Collision2D other) {
//		Debug.Log("Entering " + other.collider.gameObject.name);
		if (other.collider.gameObject.CompareTag(ConstantValues.tags.player) && (nextAttackTime == float.MaxValue)) {
//			Debug.Log("Entered player");
			DealDamage(touchDamage, -1f);
			nextAttackTime = Time.time + attackRate;
		} else if (other.collider.gameObject.tag.Contains(ConstantValues.tags.impassable) && !other.collider.gameObject.Equals(platform)) {
			// Change direction if bumping into edge of platform or hitting a wall, but not the platform they are standing on
			ChangeDirection();
//			Debug.Log ("Shmoopy hit impassable");
		}
	}

	void OnCollisionExit2D(Collision2D other) {
//		Debug.Log("Exiting " + other.collider.gameObject.name);
		if (other.collider.gameObject.CompareTag(ConstantValues.tags.player)) {
			nextAttackTime = float.MaxValue;
		}
	}

	void Move() {
		if (!isDead) {
			if (AtEdge(gameObject)) {
				// Don't walk off the edge of platform
				ChangeDirection();
				enemyAtEdge = true;
//				Debug.Log ("Shmoopy at edge");
			}
			if (InRange(attackRange)) {
				// Stop moving and deal damage
//				Debug.Log("In attack range");
				anim.SetTrigger(ConstantValues.enemyAnimatorParameterNames.stop);
				DealDamage(damage, attackRate);
			} else if (InRange(agroRange) && !InRange(attackRange) && !AtEdge(Pig.player)) {
				// Follow pig to attack
//				Debug.Log("In agro range");
				following = true;
				anim.SetTrigger(ConstantValues.enemyAnimatorParameterNames.move);
				if (MovingAwayFromPlayer()) {
					ChangeDirection();
				}
				rb.velocity = direction * speed;
			} else if (!AtEdge(gameObject) && following) {
//				Debug.Log("Piggy off platform");
				following = false; 
				ChangeDirection();
			} else {
//				Debug.Log("Not in range");
				anim.SetTrigger(ConstantValues.enemyAnimatorParameterNames.move);
				rb.velocity = direction * speed;
			}
		}
	}

	public void ChangeDirection() {
//		Debug.Log ("Shmoopy turned");
		anim.SetTrigger(ConstantValues.enemyAnimatorParameterNames.turn);
		direction = new Vector2(-direction.x, direction.y);
	}

	public bool AtEdge(GameObject g) {
		return ((g.transform.position.x <= (leftEdge + 2)) || (g.transform.position.x >= (rightEdge - 2)));
	}

	bool MovingAwayFromPlayer() {
		return (((DirectionOfPlayer() == Vector2.right) && (Mathf.Sign(direction.x) < 0)) || 
				((DirectionOfPlayer() == Vector2.left) && (Mathf.Sign(direction.x) > 0)));
	}

	Vector2 DirectionOfPlayer() {
		return (gameObject.transform.position.x < Pig.player.transform.position.x) ? Vector2.right : Vector2.left;
	}

	bool InRange(float targetRange) {
		return (Math.Abs(gameObject.transform.position.x - Pig.player.transform.position.x) <= targetRange);
	}

	// A rate of -1 indicates a one-time attack (like touch damage)
	void DealDamage(int amount, float rate) {
//		Debug.Log ("Deal damage: " + rate);
//		Debug.Log("Level donut count = " + LevelManager.levelManager.levelInstance.levelDonutCount);
		// Make the enemy wake up so the pig can deal damage even if the enemy can't deal damage to pig
		if (rb.IsSleeping()) {
			rb.WakeUp();
		}
		if ((rate == -1f) && (LevelManager.levelManager.levelInstance.levelDonutCount > 0)) {
//			Debug.Log ("Touch damage dealt");
			LevelManager.levelManager.levelInstance.donutsCollected = (LevelManager.levelManager.levelInstance.levelDonutCount - amount >= 0) ? (LevelManager.levelManager.levelInstance.donutsCollected - amount) : -LevelManager.levelPayment;
		} else if ((Time.time > nextAttackTime) && (LevelManager.levelManager.levelInstance.levelDonutCount > 0)) {
//			Debug.Log("Level donut count = " + LevelManager.levelManager.levelInstance.levelDonutCount);
//			Debug.Log ("Damage dealt");
			nextAttackTime = Time.time + rate;
			LevelManager.levelManager.levelInstance.donutsCollected = (LevelManager.levelManager.levelInstance.levelDonutCount - amount >= 0) ? (LevelManager.levelManager.levelInstance.donutsCollected - amount) : -LevelManager.levelPayment;
		}
	}

	public void TakeDamage(float damage) {
		if (currentHp > damage) {
			currentHp -= damage;
		} else if (!isDead) {
			currentHp = 0;
			Die();
		}
	}

	void Die() {
		Debug.Log(gameObject.name + " is dead.");
		isDead = true;
		// TODO Make better enemy dying indicator
		GetComponent<ParticleSystem>().Play();
		SpawnDonuts(baseDonutDrop);
		SpawnCloth (clothDropChance);
		SpawnUpgrade (upgradeDropChance);
		StartCoroutine(InstantiateDrops());
		LevelManager.levelManager.levelInstance.enemiesKilled.Add(gameObject);
	}

	void SpawnDonuts(int donutDropCount) {
		spawnList.Clear();
		PopulateSpawnList(donutDropCount);
	}

	void SpawnCloth(float dropChance) {
		if (UnityEngine.Random.Range(0f, 1f) < dropChance) {
			Debug.Log ("Cloth created.");
			spawnList.Add (Resources.Load ("Prefabs/SpawnItems/Cloth"));
		}
	}

	void SpawnUpgrade(float dropChance) {
		if (UnityEngine.Random.Range(0f, 1f) < dropChance) {
			Debug.Log ("Upgrade created.");
			int i = UnityEngine.Random.Range (0, GlobalControl.Instance.savedData.unlockedHeadItems.Count);
			spawnList.Add (Resources.Load ("Prefabs/SpawnItems/" + GlobalControl.Instance.savedData.unlockedHeadItems[i].headItemStyle));
		}
	}

	// Splits up the total value of donuts that need to be dropped into the least amount of donuts 
	// (mostly. donutDropCount of 40 does not give 2 chocolates).
	// Then fills spawnList with the prefabs of the appropriate donuts.
	// TODO Make this have better drop distribution
	void PopulateSpawnList(int donutDropCount) {
		if (donutDropCount == 1) {
			spawnList.Add(LevelManager.levelManager.GetDonutPrefab(ConstantValues.donutNames.cinnamonHole));
		} else if (donutDropCount < 5) {
			spawnList.Add(LevelManager.levelManager.GetDonutPrefab(ConstantValues.donutNames.cinnamonHole));
			PopulateSpawnList(donutDropCount - 1);
		} else if (donutDropCount == 5) {
			spawnList.Add(LevelManager.levelManager.GetDonutPrefab(ConstantValues.donutNames.chocolateHole));
		} else if ((donutDropCount > 5) && (donutDropCount < 10)) {
			PopulateSpawnList(5);
			PopulateSpawnList(donutDropCount - 5);
		} else if (donutDropCount == 10) {
			spawnList.Add(LevelManager.levelManager.GetDonutPrefab(ConstantValues.donutNames.sprinklesHole));
		} else if ((donutDropCount > 10) && (donutDropCount < 20)) {
			PopulateSpawnList(10);
			PopulateSpawnList(donutDropCount - 10);
		} else if (donutDropCount == 20) {
			spawnList.Add(LevelManager.levelManager.GetDonutPrefab(ConstantValues.donutNames.chocolate));
		} else if ((donutDropCount > 20) && (donutDropCount < 25)) {
			PopulateSpawnList(20);
			PopulateSpawnList(donutDropCount - 20);
		} else if (donutDropCount == 25) {
			spawnList.Add(LevelManager.levelManager.GetDonutPrefab(ConstantValues.donutNames.strawberry));
		} else {
			PopulateSpawnList(25);
			PopulateSpawnList(donutDropCount - 25);
		}
	}

	// Instatiates all of the donuts in spawnList and adds a force to each, spreading them uniformly.
	// Also destroys the enemy since this is the last thing that needs to be done
	IEnumerator InstantiateDrops() {
		yield return new WaitForSeconds(deadDelayTime);

		float absoluteMaxAngle = Mathf.PI;
		float minAngle = 0f;
		float increment = (absoluteMaxAngle - minAngle) / spawnList.Count;
		float maxAngle = increment;
		float magnitude = 5f;

		foreach (GameObject prefab in spawnList) {
			GameObject drop = Instantiate(prefab, gameObject.transform.position, Quaternion.identity) as GameObject;
			float angle = UnityEngine.Random.Range(minAngle, maxAngle);
			drop.GetComponent<Rigidbody2D>().AddForce(new Vector2(magnitude*Mathf.Cos(angle), magnitude*Mathf.Sin(angle)), ForceMode2D.Impulse);
			minAngle = maxAngle;
			maxAngle += increment;
		}
		Destroy(gameObject);
	}
}
