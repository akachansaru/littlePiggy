using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Used to purchase a clothing item that hasn't been unlocked from the IAP screen.
/// </summary>
public class PurchaseNewClothingItem : MonoBehaviour, IDisplayItemInfo {

	public GameObject buyItemPanel;
	public Image itemImage;
	public Text statBoostText;
	public Text priceText;
	public Purchaser purchaser;

	private WearableItemInfo wearableItemInfo;
	private string price;
	private GameObject selectedItem;

	/// <summary>
	/// Opens the panel with information about the specific item.
	/// </summary>
	/// <param name="itemInfo">Item info.</param>
	public void DisplayItemInfo() {
		selectedItem = EventSystem.current.currentSelectedGameObject;
		wearableItemInfo = selectedItem.GetComponent<WearableItemInfo>();
		price = selectedItem.GetComponentInChildren<Text>().text;
		itemImage = wearableItemInfo.itemImage;
		statBoostText.text = wearableItemInfo.itemStat + " +" + wearableItemInfo.statIncrease;
		priceText.text = "Unlock this item for " + price;
		buyItemPanel.SetActive(true);
	}

	public void AcceptItem() {
		purchaser.BuyNonConsumable (wearableItemInfo.itemName);
	}

	public void CloseItemInfoPanel() {
		priceText.text = "";
		buyItemPanel.SetActive (false);
	}
}
