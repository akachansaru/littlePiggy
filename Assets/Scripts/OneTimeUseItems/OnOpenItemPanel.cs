using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// For in level when player clicks on the item button to view purchased items
public class OnOpenItemPanel : MonoBehaviour {

	public UseItem itemManager;
	public GameObject itemPanel;

	void Start() {
		LoadItems ();
	}

	void LoadItems() {
		foreach (OneTimeItem item in GlobalControl.Instance.savedData.oneTimeItems) {
			DisplayItem (item);
		}
	}

	/// <summary>
	/// Each itemButton is a LayoutElement so it will be put into the correct place on the ItemPanel automatically.
	/// </summary>
	/// <param name="item">Item.</param>
	void DisplayItem(OneTimeItem item) {
		Debug.Log (item.ToString());
		GameObject itemButton = Instantiate (Resources.Load ("Prefabs/OneTimeItems/UsingItems/OneTimeItemButton"), itemPanel.transform) as GameObject;
		itemButton.transform.localScale = Vector3.one;
		item.AddInfo (itemButton.GetComponent<OneTimeItemInfo> ());
		itemButton.transform.GetComponentInChildren<Text> ().text = itemButton.GetComponent<OneTimeItemInfo> ().itemName;
		itemButton.GetComponent<Button>().onClick.AddListener(() => itemManager.DisplayItemInfo());
	}
}
