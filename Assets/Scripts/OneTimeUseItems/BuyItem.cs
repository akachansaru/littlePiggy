using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour {

	public GameObject buyItemPanel;
	public Image itemImage;
	public Text statBoostText;
	public Text numberOwnedText;
	public Text priceText;

	private OneTimeItem oneTimeItem;
	private int price;

	// Opens the panel with information about specific item, for example, coffee
	public void OpenBuyItemPanel(OneTimeItemInfo itemInfo) {
		oneTimeItem = itemInfo.MakeItem();
		price = itemInfo.price;
		itemImage = itemInfo.image;
		statBoostText.text = itemInfo.stat + " +" + itemInfo.statIncrease + " for " + itemInfo.statDuration + " seconds";
		numberOwnedText.text = "Currently have " + AmountOwned();
		priceText.text = "Buy for " + priceText.text + itemInfo.price.ToString() + " donuts";
		buyItemPanel.SetActive(true);
	}

	public void AddToInventory() {
		if (price <= GlobalControl.Instance.savedData.SafeDonutCount) {
			if (GlobalControl.Instance.savedData.oneTimeItems.Exists (i => i.Equals (oneTimeItem))) {
				GlobalControl.Instance.savedData.oneTimeItems.Find(i => i.Equals (oneTimeItem)).amountOwned+=1;
				Debug.Log("Buying another " + oneTimeItem.ToString());
			} else {
				GlobalControl.Instance.savedData.oneTimeItems.Add(oneTimeItem);
				Debug.Log ("Buying new item " + oneTimeItem.ToString());
			}
			GlobalControl.Instance.savedData.SafeDonutCount -= price;
			GlobalControl.Instance.Save();
			ClosePanel();
		} else {
			Debug.Log ("Not enough donuts.");
			// UNDONE Put in message about not having enough donuts to buy the item
		}
	}

	public void ClosePanel() {
		priceText.text = "";
		buyItemPanel.SetActive (false);
	}

	string AmountOwned() {
		if (GlobalControl.Instance.savedData.oneTimeItems.Exists(i => i.Equals(oneTimeItem))) {
			return GlobalControl.Instance.savedData.oneTimeItems.Find(i => i.Equals(oneTimeItem)).amountOwned.ToString();
		} else {
			return "0";
		}
	}
}
