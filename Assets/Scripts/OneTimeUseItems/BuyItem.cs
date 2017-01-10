using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuyItem : MonoBehaviour, IDisplayItemInfo {

	public GameObject buyItemPanel;
	public Image itemImage;
	public Text statBoostText;
	public Text numberOwnedText;
	public Text priceText;

    private OneTimeItem oneTimeItem;
	private int price;
	private GameObject selectedItem;

	/// <summary>
	///  Opens the panel with information about specific item, for example, coffee.
	/// </summary>
	public void DisplayItemInfo() {
		selectedItem = EventSystem.current.currentSelectedGameObject;
		OneTimeItemInfo itemInfo = selectedItem.GetComponent<OneTimeItemInfo> ();
		oneTimeItem = itemInfo.MakeItem() as OneTimeItem;
		price = itemInfo.donutPrice;
		itemImage = itemInfo.itemImage;
		statBoostText.text = itemInfo.itemStat + " +" + itemInfo.statIncrease + " for " + itemInfo.statDuration + " seconds";
		numberOwnedText.text = "Currently have " + AmountOwned();
		priceText.text = "Buy for " + priceText.text + itemInfo.donutPrice.ToString() + " donuts";
		buyItemPanel.SetActive(true);
	}

	public void AcceptItem() {
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
		} else {
			Debug.Log ("Not enough donuts.");
			// UNDONE Put in message about not having enough donuts to buy the item
		}
	}

	public void CloseItemInfoPanel() {
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
