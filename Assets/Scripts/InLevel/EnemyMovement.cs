using UnityEngine;
using System;
using System.Collections;

namespace Enemy {
	[RequireComponent (typeof(ParticleSystem))]
	[RequireComponent (typeof(EnemyDropManager))]
	[RequireComponent (typeof(SpriteRenderer))]
	[RequireComponent (typeof(BoxCollider2D))]
	[RequireComponent (typeof(Rigidbody2D))]
	[RequireComponent (typeof(DestroyOnFall))]
	[RequireComponent(typeof(Animator))]
	public class EnemyMovement : MonoBehaviour {

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
		private bool isDead;
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

		void Start() {
			rb = GetComponent<Rigidbody2D>();
			anim = GetComponent<Animator>();
			rightEdge = platform.GetComponent<Renderer>().bounds.max.x;
			leftEdge = platform.GetComponent<Renderer>().bounds.min.x;
			direction = initialDirection;
			following = false;

			isDead = false;

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
			GetComponent<EnemyDropManager>().SpawnDonuts(baseDonutDrop);
			GetComponent<EnemyDropManager>().SpawnCloth (clothDropChance);
			GetComponent<EnemyDropManager>().SpawnUpgrade (upgradeDropChance);
			StartCoroutine(GetComponent<EnemyDropManager>().InstantiateDrops());
			LevelManager.levelManager.levelInstance.enemiesKilled.Add(gameObject);
		}
	}
}
