using UnityEngine;
using System.Collections;

namespace Enemy {
	public class EnemyDropManager : MonoBehaviour {
		private ArrayList spawnList = new ArrayList();
		private float deadDelayTime = 2f; // Time between losing all hitpoints and the body disapearing

		public IEnumerator InstantiateDonut(GameObject donutPrefab, Vector2 position) {
            yield return new WaitForSeconds(2f);
			Instantiate(donutPrefab, position, Quaternion.identity);
		}


		public void SpawnDonuts(int donutDropCount) {
			spawnList.Clear();
			PopulateSpawnList(donutDropCount);
		}

		public void SpawnCloth(float dropChance) {
			if (UnityEngine.Random.Range(0f, 1f) < dropChance) {
				Debug.Log ("Cloth created.");
				spawnList.Add (Resources.Load ("Prefabs/SpawnItems/Cloth"));
			}
		}

		public void SpawnUpgrade(float dropChance) {
			if (UnityEngine.Random.Range(0f, 1f) < dropChance) {
				Debug.Log ("Upgrade created.");
				int i = UnityEngine.Random.Range (0, GlobalControl.Instance.savedData.unlockedHeadItems.Count);
				spawnList.Add (Resources.Load ("Prefabs/SpawnItems/" + GlobalControl.Instance.savedData.unlockedHeadItems[i].itemName));
			}
		}


		// Instatiates all of the donuts in spawnList and adds a force to each, spreading them uniformly.
		// Also destroys the enemy since this is the last thing that needs to be done
		public IEnumerator InstantiateDrops() {
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

		// Splits up the total value of donuts that need to be dropped into the least amount of donuts 
		// (mostly. donutDropCount of 40 does not give 2 chocolates).
		// Then fills spawnList with the prefabs of the appropriate donuts.
		// TODO Make this have better drop distribution
		void PopulateSpawnList(int donutDropCount) {
			if (donutDropCount == 1) {
				spawnList.Add(GetDonutPrefab(ConstantValues.donutNames.cinnamonHole));
			} else if (donutDropCount < 5) {
				spawnList.Add(GetDonutPrefab(ConstantValues.donutNames.cinnamonHole));
				PopulateSpawnList(donutDropCount - 1);
			} else if (donutDropCount == 5) {
				spawnList.Add(GetDonutPrefab(ConstantValues.donutNames.chocolateHole));
			} else if ((donutDropCount > 5) && (donutDropCount < 10)) {
				PopulateSpawnList(5);
				PopulateSpawnList(donutDropCount - 5);
			} else if (donutDropCount == 10) {
				spawnList.Add(GetDonutPrefab(ConstantValues.donutNames.sprinklesHole));
			} else if ((donutDropCount > 10) && (donutDropCount < 20)) {
				PopulateSpawnList(10);
				PopulateSpawnList(donutDropCount - 10);
			} else if (donutDropCount == 20) {
				spawnList.Add(GetDonutPrefab(ConstantValues.donutNames.chocolate));
			} else if ((donutDropCount > 20) && (donutDropCount < 25)) {
				PopulateSpawnList(20);
				PopulateSpawnList(donutDropCount - 20);
			} else if (donutDropCount == 25) {
				spawnList.Add(GetDonutPrefab(ConstantValues.donutNames.strawberry));
			} else {
				PopulateSpawnList(25);
				PopulateSpawnList(donutDropCount - 25);
			}
		}

		GameObject GetDonutPrefab(string donutName) {
			GameObject donutPrefab;
			string donutPath = "Prefabs/SpawnItems/";
			if (donutName == ConstantValues.donutNames.cinnamonHole) {
				donutPrefab = Resources.Load(donutPath + "CinnamonHoleP") as GameObject;
			} else if (donutName == ConstantValues.donutNames.chocolateHole) {
				donutPrefab = Resources.Load(donutPath + "ChocolateHoleP") as GameObject;
			} else if (donutName == ConstantValues.donutNames.sprinklesHole) {
				donutPrefab = Resources.Load(donutPath + "SprinklesHoleP") as GameObject;
			} else if (donutName == ConstantValues.donutNames.chocolate) {
				donutPrefab = Resources.Load(donutPath + "ChocolateP") as GameObject;;
			} else if (donutName == ConstantValues.donutNames.strawberry) {
				donutPrefab = Resources.Load(donutPath + "StrawberryP") as GameObject;;
			} else {
				Debug.Log ("Incorrect donut name");
				donutPrefab = Resources.Load(donutPath + "CinnamonHoleP") as GameObject;;
			}
			return donutPrefab;
		}
	}
}

