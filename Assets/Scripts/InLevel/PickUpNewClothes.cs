using UnityEngine;
using System.Collections;

public class PickUpNewClothes : MonoBehaviour {

	public bool alreadyCollected;

//	void Start() {
//		if (alreadyCollected) {
//			Destroy (gameObject);
//			// TODO Don't show the item in the level if it's been unlocked
//		}
//	}

	// Allows the piggy to collect the item
	void OnTriggerEnter2D(Collider2D other) {		
		if (other.gameObject.CompareTag(ConstantValues.tags.player)){
			HeadItem headItem = new HeadItem (GetComponent<WearableItemInfo> ().itemName, SerializableColor.white,
				new SpriteRenderer (), GetComponent<WearableItemInfo> ().statIncrease, GetComponent<WearableItemInfo> ().clothPrice, false);
			if (!alreadyCollected) {
				GlobalControl.Instance.savedData.unlockedHeadItems.Add(headItem);
			} else {
				GlobalControl.Instance.savedData.unlockedHeadItems.Find(h => h.Equals(headItem)).LevelUp();
				foreach (HeadItem item in GlobalControl.Instance.savedData.headItems) {
					if (item.headItemStyle == headItem.headItemStyle) {
						if (item.headItemStyle == GlobalControl.Instance.savedData.headItems.Find (h => h.currentlyWearing).headItemStyle) {
							LevelManager.levelManager.jumpModifier -= GlobalControl.Instance.savedData.headItems.Find (h => h.currentlyWearing).statBoost;
						}
						item.LevelUp ();
						if (item.headItemStyle == GlobalControl.Instance.savedData.headItems.Find (h => h.currentlyWearing).headItemStyle) {
							LevelManager.levelManager.jumpModifier += GlobalControl.Instance.savedData.headItems.Find (h => h.currentlyWearing).statBoost;
						}
					}
				}
			}
			gameObject.SetActive(false);
		}
	}
}
