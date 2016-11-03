using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// Displays all the items that the player has created. These items can be dragged onto the pig to wear them.
// Goes onto the canvas.
[RequireComponent (typeof (Canvas))]
public class PopulateItemPanels : MonoBehaviour {
	public GameObject player;
	public GameObject hatContent;
	public GameObject capeContent;
	public GameObject shoeContent;

	void Start () {
		List<HeadItem> headItems = GlobalControl.Instance.savedData.headItems;
		foreach (HeadItem headItem in headItems) {
			Debug.Log ("Head item " + headItem.headItemStyle);
			GameObject hatUI = Instantiate(HeadItemMethods.LoadUIPrefab(), hatContent.transform) as GameObject;
			HeadItemMethods.ApplyUIAttributes (headItem, hatUI, GetComponent<Canvas>());
			// TODO See if I can use LoadItemsOnPig instead for consistancy
			if (headItem.currentlyWearing) {
				ItemsOnPig.itemsOnPig.WearingHeadItem = true;
				ItemsOnPig.itemsOnPig.HeadItem = Instantiate(HeadItemMethods.LoadPrefab (headItem), player.transform) as GameObject;
				HeadItemMethods.ApplyAttributes (headItem, ItemsOnPig.itemsOnPig.HeadItem);
				ItemsOnPig.itemsOnPig.HeadItemUI = hatUI;
				ItemsOnPig.itemsOnPig.HeadItemUI.GetComponent<DragWearableItem>().GrayOutListOption(true);
			}
		}
	}
}
