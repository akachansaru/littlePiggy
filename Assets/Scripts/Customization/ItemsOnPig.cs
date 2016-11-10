using UnityEngine;
using System.Collections;

/// <summary>
///  Attached to the pig area box in the Customizaton scene.
/// </summary>
public class ItemsOnPig : MonoBehaviour {

	public static ItemsOnPig itemsOnPig;
	public static bool inPigArea;

	public GameObject pig;

	private bool wearingHeadItem;
	private GameObject headItem; // The item currently on the pig
	private GameObject headItemUI;

	public GameObject HeadItem {
		get { return headItem; }
		set { headItem = value; }
	}

	public GameObject HeadItemUI {
		get { return headItemUI; }
		set { headItemUI = value; }
	}

	public bool WearingHeadItem {
		set { wearingHeadItem = value; }
	}

	void Start() {
		itemsOnPig = this;
		wearingHeadItem = GlobalControl.Instance.savedData.headItems.Exists(h => h.currentlyWearing);
		inPigArea = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Contains(ConstantValues.tags.wearable)) {
			inPigArea = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag.Contains(ConstantValues.tags.wearable)) {
			inPigArea = false;
		}
	}

	public void PlaceOnPig(GameObject wearableItem, GameObject wearableItemUI) {
		if (wearableItem.tag.Contains(ConstantValues.tags.head)) {
			if (!wearingHeadItem) {
				AddNewHat(wearableItem, wearableItemUI);
			} else {
				ReplaceHat(wearableItem, wearableItemUI);
			}
		}

		GlobalControl.Instance.Save ();
		inPigArea = false;
	}

	// The hat is a child of pig
	void AddNewHat(GameObject wearableItem, GameObject wearableItemUI) {
		wearableItem.transform.position = new Vector2 (-4.8f, 2.7f);
		wearableItem.transform.parent = pig.transform;
		wearingHeadItem = true;
		headItem = wearableItem;
		headItemUI = wearableItemUI;
//		GlobalControl.Instance.savedData.currentHeadItem = HeadItemMethods.FromGameObject(headItem);
		HeadItem newHat = GlobalControl.Instance.savedData.headItems.Find(h => h.Equals(headItem));
		Debug.Log ("Found " + newHat);
		newHat.currentlyWearing = true;
		// Apply stat boost for item pig is wearing
//		LevelManager.levelManager.jumpModifier += newHat.statBoost;
		Debug.Log ("Placed " + wearableItem.name + " on pig.");
	}

	void ReplaceHat(GameObject wearableItem, GameObject wearableItemUI) {
		headItemUI.GetComponent<DragWearableItem>().GrayOutListOption(false);
		HeadItem oldHat = GlobalControl.Instance.savedData.headItems.Find(h => h.Equals(headItem));
		oldHat.currentlyWearing = false;
		// Remove stat boost for item pig was wearing before adding the new stats
//		LevelManager.levelManager.jumpModifier -= oldHat.statBoost;
		Destroy (headItem);
		AddNewHat (wearableItem, wearableItemUI);
	}
}
