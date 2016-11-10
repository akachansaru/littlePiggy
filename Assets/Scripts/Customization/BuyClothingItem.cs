using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Used to buy an unlocked clothing item for cloth and brings the player to the item customization screen.
/// </summary>
public class BuyClothingItem : MonoBehaviour, IDisplayItemInfo {

	public GameObject buyItemPanel;
	public Image itemImage;
	public Text statBoostText;
	public Text priceText;

	private WearableItemInfo wearableItemInfo;
	private int price;
	private GameObject selectedItem;

	/// <summary>
	/// Opens the panel with information about specific item.
	/// </summary>
	public void DisplayItemInfo() {
		selectedItem = EventSystem.current.currentSelectedGameObject;
		wearableItemInfo = selectedItem.GetComponent<WearableItemInfo>();
		price = wearableItemInfo.clothPrice;
		itemImage = wearableItemInfo.itemImage;
		statBoostText.text = wearableItemInfo.itemStat + " +" + wearableItemInfo.statIncrease;
		priceText.text = "Buy for " + priceText.text + wearableItemInfo.clothPrice.ToString() + " cloth";
		buyItemPanel.SetActive(true);
	}

	public void AcceptItem() {
		if (price <= GlobalControl.Instance.savedData.SafeClothCount) {
			SceneLoader.LoadItemCustomization (new HeadItem(wearableItemInfo.itemName, SerializableColor.white, new SpriteRenderer(), 
				wearableItemInfo.statIncrease, price, false));
			GlobalControl.Instance.savedData.SafeClothCount -= price;
			GlobalControl.Instance.Save();
		} else {
			Debug.Log ("Not enough cloth.");
			// UNDONE Put in message about not having enough cloth to buy the item
		}
	}

	public void CloseItemInfoPanel() {
		priceText.text = "";
		buyItemPanel.SetActive (false);
	}
}
