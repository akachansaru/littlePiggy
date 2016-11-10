using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// Displays all the items that the player has unlocked and can create with materials.
[RequireComponent (typeof (BuyClothingItem))]
public class PopulateUnlockedClothingPanels : MonoBehaviour {
	public GameObject hatContent;
	public GameObject capeContent;
	public GameObject shoeContent;

	void Start () {
		List<HeadItem> headItems = GlobalControl.Instance.savedData.unlockedHeadItems;
		foreach (HeadItem headItem in headItems) {
			Debug.Log ("Head item " + headItem.itemName);
			GameObject hatUI = Instantiate(Resources.Load<GameObject> ("Prefabs/WearableItems/UI/UnlockedItemUI"),
				hatContent.transform) as GameObject;
			hatUI.GetComponent<RectTransform> ().localScale = Vector3.one;
			hatUI.name = headItem.itemName + "UI";
			hatUI.GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/WearableItems/" + headItem.itemName);
			hatUI.GetComponent<Button>().onClick.AddListener(() => GetComponent<BuyClothingItem>().DisplayItemInfo());
			headItem.AddInfo (hatUI.GetComponent<WearableItemInfo> ());
//			hatUI.GetComponent<WearableItemInfo> ().itemName = headItem.headItemStyle;
//			hatUI.GetComponent<WearableItemInfo> ().itemStat = "Jump";
//			hatUI.GetComponent<WearableItemInfo> ().statIncrease = headItem.itemStatIncrease;
//			hatUI.GetComponent<WearableItemInfo> ().clothPrice = headItem.clothPrice;
		}
	}
}
