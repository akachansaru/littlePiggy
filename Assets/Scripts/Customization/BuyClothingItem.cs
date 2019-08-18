using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Used to buy an unlocked clothing item for cloth and brings the player to the item customization screen.
/// </summary>
public class BuyClothingItem : MonoBehaviour {

	public GameObject buyItemPanel;
	public Image itemImage;
	public Text statBoostText;
	public Text priceText;

	private WearableItemInfo wearableItemInfo;
	private int price;

	/// <summary>
	/// Opens the panel with information about specific item.
	/// </summary>
	/// <param name="itemInfo">Item info.</param>
	public void OpenBuyItemPanel(WearableItemInfo itemInfo) {
//		oneTimeItem = itemInfo.MakeItem();
		wearableItemInfo = itemInfo;
		price = itemInfo.clothPrice;
		itemImage = itemInfo.image;
		statBoostText.text = itemInfo.stat + " +" + itemInfo.statIncrease;
		priceText.text = "Buy for " + priceText.text + itemInfo.clothPrice.ToString() + " cloth";
		buyItemPanel.SetActive(true);
	}

	public void Buy() {
		if (price <= GlobalControl.Instance.savedData.SafeClothCount) {
			SceneLoader.LoadItemCustomization (new HeadItem(wearableItemInfo.itemName, SerializableColor.white, new SpriteRenderer(), 
				wearableItemInfo.statIncrease, price, false));
			GlobalControl.Instance.savedData.SafeClothCount -= price;
			GlobalControl.Instance.Save();
			ClosePanel();
		} else {
			Debug.Log ("Not enough cloth.");
			// UNDONE Put in message about not having enough cloth to buy the item
		}
	}

	public void ClosePanel() {
		priceText.text = "";
		buyItemPanel.SetActive (false);
	}
}
