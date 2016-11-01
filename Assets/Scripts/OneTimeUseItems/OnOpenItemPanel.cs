using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// For in level when player clicks on the item button to view purchased items
public class OnOpenItemPanel : MonoBehaviour {

	public GameObject itemManager;
	public GameObject itemPanel;

	void Start() {
		LoadItems ();
	}

	void LoadItems() {
		foreach (OneTimeItem item in GlobalControl.Instance.savedData.oneTimeItems) {
			DisplayItem (item);
		}
	}

	// Each itemButton is a LayoutElement so it will be put into the correct place on the ItemPanel automatically
	void DisplayItem(OneTimeItem item) {
		Debug.Log (item.ToString());
		GameObject itemButton = Instantiate (Resources.Load ("Prefabs/InLevelCanvas/OneTimeItems/UsingItems/OneTimeItemButton"), itemPanel.transform) as GameObject;
		itemButton.transform.localScale = Vector3.one;
		item.AddInfo (itemButton);
		itemButton.transform.GetComponentInChildren<Text> ().text = itemButton.GetComponent<OneTimeItemInfo> ().itemName;
		itemButton.GetComponent<Button>().onClick.AddListener(() => itemManager.GetComponent<UseItem>().OpenUseItemPanel());
	}
}
