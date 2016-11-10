using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UseItem : MonoBehaviour, IDisplayItemInfo {

	public GameObject useItemPanel;
	public Image itemImage;
	public Text statBoostText;
	public Text numberOwnedText;
	public Timer jumpTimer;
	public Timer speedTimer;
	public Timer damageTimer;

	private OneTimeItem oneTimeItem;
	private GameObject selectedItem;
	private bool usingJumpItem = false;
	private bool usingSpeedItem = false;
	// TODO Add in other stats

	/// <summary>
	/// Opens the panel for the specific item tapped on.
	/// </summary>
	public void DisplayItemInfo() {
		selectedItem = EventSystem.current.currentSelectedGameObject;
		OneTimeItemInfo itemInfo = selectedItem.GetComponent<OneTimeItemInfo> ();
		oneTimeItem = itemInfo.MakeItem() as OneTimeItem;
		itemImage = itemInfo.itemImage;
		statBoostText.text = itemInfo.itemStat + " +" + itemInfo.statIncrease + " for " + itemInfo.statDuration + " seconds";
		numberOwnedText.text = "Currently have " + AmountOwned();
		useItemPanel.SetActive (true);
	}

	/// <summary>
	/// Applies the stats and starts the timer once the panel is closed.
	/// </summary>
	public void AcceptItem() {
		Debug.Log ("oneTimeItem: " + oneTimeItem.itemStat);
		// FIXME I think the itemStat is probably not set correctly somewhere
		if (((oneTimeItem.itemStat == "Jump") && !usingJumpItem) ||
			((oneTimeItem.itemStat == "Speed") && !usingSpeedItem)) {
			if (oneTimeItem.itemStat == "Jump") {
				usingJumpItem = true;
			}
			if (oneTimeItem.itemStat == "Speed") {
				usingSpeedItem = true;
			}
			RemoveSavedItem (oneTimeItem);
			GlobalControl.Instance.Save();
			ApplyStats (selectedItem.GetComponent<OneTimeItemInfo> ());
			// Won't start the countdown until the game is unpaused because using WaitForSeconds
			StartCoroutine (StartTimer (selectedItem.GetComponent<OneTimeItemInfo> ()));
		} else {
			Debug.Log ("Already using that kind of item.");
			// TODO Add in a message that you can't use two of the same kind of item
		}
	}

	public void CloseItemInfoPanel() {
		useItemPanel.SetActive (false);
		oneTimeItem = null;
	}

	string AmountOwned() {
		if (GlobalControl.Instance.savedData.oneTimeItems.Exists(i => i.Equals(oneTimeItem))) {
			return GlobalControl.Instance.savedData.oneTimeItems.Find(i => i.Equals(oneTimeItem)).amountOwned.ToString();
		} else {
			return "0";
		}
	}

	void RemoveSavedItem(OneTimeItem itemToRemove) {
		if (GlobalControl.Instance.savedData.oneTimeItems.Exists(i => i.Equals(itemToRemove))) {
			if (GlobalControl.Instance.savedData.oneTimeItems.Find (i => i.Equals (itemToRemove)).amountOwned > 1) {
				GlobalControl.Instance.savedData.oneTimeItems.Find (i => i.Equals (itemToRemove)).amountOwned--;
				Debug.Log ("Decreased amount of " + itemToRemove.ToString ());
			} else {
				GlobalControl.Instance.savedData.oneTimeItems.Remove(itemToRemove);
				Destroy(selectedItem);
				Debug.Log ("Removed item " + itemToRemove.ToString ());
			}
		} else {
			Debug.Log ("!The item does not exist.!");
		}
//		foreach (OneTimeItem item in GlobalControl.Instance.savedData.oneTimeItems) {
//			if (item.Equals(itemToRemove)) {
//				GlobalControl.Instance.savedData.oneTimeItems.Remove(item);
//				Debug.Log("Removed item " + item.ToString());
//				break;
//			}
//		}
	}

	IEnumerator StartTimer(OneTimeItemInfo itemInfo) {
		Debug.Log ("Timer start");
		if (itemInfo.itemStat == "Jump") {
			jumpTimer.StartTimer (itemInfo.statDuration);
		} else if (itemInfo.itemStat == "Jump") {
			speedTimer.StartTimer (itemInfo.statDuration);
		}
		yield return new WaitForSeconds ((float)itemInfo.statDuration);
//		Debug.Log ("oneTimeItem: " + oneTimeItem.itemStat);
		if (itemInfo.itemStat == "Jump") {
			usingJumpItem = false;
		}
		if (itemInfo.itemStat == "Jump") {
			usingSpeedItem = false;
		}
		RemoveItemEffects(itemInfo);
		Debug.Log ("Timer end");
	}

	void ApplyStats(OneTimeItemInfo info) {
		if (info.itemStat == "Jump") {
			LevelManager.levelManager.jumpModifier = info.statIncrease;
			Debug.Log ("Aplied stats. Jump now " + LevelManager.piggyJump);
		} else if (info.itemStat == "Speed") {
			LevelManager.levelManager.speedModifier = info.statIncrease;
			Debug.Log ("Aplied stats. Speed now " + LevelManager.piggySpeed);
		}

	}

	void RemoveItemEffects(OneTimeItemInfo info) {
		if (info.itemStat == "Jump") {
			LevelManager.levelManager.jumpModifier = 0;
			// TODO Make levelManager update stats whenever something is changed
			Debug.Log ("Removed stats. Jump now " + LevelManager.piggyJump);
		} else if (info.itemStat == "Speed") {
			LevelManager.levelManager.speedModifier = 1;
			Debug.Log ("Removed stats. Speed now " + LevelManager.piggySpeed);
		} else {
			Debug.Log ("Not a valid stat.");
		}
	}
}

