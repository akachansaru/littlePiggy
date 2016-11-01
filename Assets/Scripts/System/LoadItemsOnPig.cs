using UnityEngine;
using System.Collections;

public class LoadItemsOnPig : MonoBehaviour {

	void Start() {
		HeadItem current = FindCurrentHeadItem ();
		if (current != null) {
			LoadItems (current);
			if (LevelManager.levelManager != null) {
				ApplyStats (current);
			}
		}
	}

	HeadItem FindCurrentHeadItem() {
		return GlobalControl.Instance.savedData.headItems.Find (h => h.currentlyWearing);
	}

	void LoadItems(HeadItem current) {
		Debug.Log ("Loading pig items. CurrentHeadItem = " + current.ToString());
		GameObject headItem = Instantiate (HeadItemMethods.LoadPrefab(current), transform) as GameObject;
		HeadItemMethods.ApplyAttributes (current, headItem); // Should change the color
		headItem.GetComponent<BoxCollider2D> ().isTrigger = true;
		headItem.transform.localScale = Vector3.one;
		headItem.transform.localPosition = new Vector3 (0.5f, 0.7f, 0f);
	}

	void ApplyStats(HeadItem current) {
		LevelManager.levelManager.jumpModifier += current.statBoost;
		Debug.Log ("Stat boost " + LevelManager.levelManager.jumpModifier);
	}
}
