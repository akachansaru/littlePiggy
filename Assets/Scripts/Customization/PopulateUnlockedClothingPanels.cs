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
			Debug.Log ("Head item " + headItem.headItemStyle);
			GameObject hatUI = Instantiate(Resources.Load<GameObject> ("Prefabs/WearableItems/UI/UnlockedItemUI"),
				hatContent.transform) as GameObject;
			hatUI.GetComponent<RectTransform> ().localScale = Vector3.one;
			hatUI.name = headItem.headItemStyle + "UI";
			hatUI.GetComponent<Image>().sprite = Resources.Load<Sprite> ("Sprites/WearableItems/" + headItem.headItemStyle);
			hatUI.GetComponent<Button>().onClick.AddListener(() => GetComponent<BuyClothingItem>().OpenBuyItemPanel(hatUI.GetComponent<WearableItemInfo>()));
			hatUI.GetComponent<WearableItemInfo> ().itemName = headItem.headItemStyle;
			hatUI.GetComponent<WearableItemInfo> ().stat = "Jump";
			hatUI.GetComponent<WearableItemInfo> ().statIncrease = headItem.statBoost;
			hatUI.GetComponent<WearableItemInfo> ().clothPrice = headItem.clothPrice;
		}
	}
}
