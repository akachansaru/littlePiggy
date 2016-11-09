using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Used to purchase a clothing item that hasn't been unlocked from the IAP screen.
/// </summary>
public class PurchaseNewClothingItem : MonoBehaviour {

	public GameObject buyItemPanel;
	public Image itemImage;
	public Text statBoostText;
	public Text priceText;
	public Purchaser purchaser;

	private WearableItemInfo wearableItemInfo;
	private string price;

	/// <summary>
	/// Opens the panel with information about specific item.
	/// </summary>
	/// <param name="itemInfo">Item info.</param>
	public void OpenBuyItemPanel(GameObject item) {
		//		oneTimeItem = itemInfo.MakeItem();
		wearableItemInfo = item.GetComponent<WearableItemInfo>();
		price = item.GetComponentInChildren<Text>().text;
		itemImage = wearableItemInfo.image;
		statBoostText.text = wearableItemInfo.stat + " +" + wearableItemInfo.statIncrease;
		priceText.text = "Unlock this item for " + price;
		buyItemPanel.SetActive(true);
	}

	public void Buy() {
		purchaser.BuyNonConsumable (wearableItemInfo.itemName);
	}

	public void ClosePanel() {
		priceText.text = "";
		buyItemPanel.SetActive (false);
	}
}
