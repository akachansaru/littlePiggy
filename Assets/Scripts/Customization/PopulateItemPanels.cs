using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopulateItemPanels : MonoBehaviour {
	public Canvas canvas;
	public GameObject hatContent;
	public GameObject capeContent;
	public GameObject shoeContent;
	public GameObject itemUIPrefab;

	// Use this for initialization
	void Start () {
		List<HeadItem> headItems = GlobalControl.Instance.savedData.headItems;
		foreach (HeadItem headItem in headItems) {
			Debug.Log ("Head item " + headItem.headItemStyle);
			GameObject hatUI = Instantiate(HeadItemMethods.LoadUIPrefab(), hatContent.transform) as GameObject;
			HeadItemMethods.ApplyUIAttributes (headItem, hatUI, canvas);
			if (headItem.currentlyWearing) {
				ItemsOnPig.itemsOnPig.WearingHeadItem = true;
				ItemsOnPig.itemsOnPig.HeadItem = Instantiate(HeadItemMethods.LoadPrefab (headItem));
				HeadItemMethods.ApplyAttributes (headItem, ItemsOnPig.itemsOnPig.HeadItem);
				ItemsOnPig.itemsOnPig.HeadItemUI = hatUI;
				ItemsOnPig.itemsOnPig.HeadItemUI.GetComponent<DragWearableItem>().GrayOutListOption(true);
			}
//			hatUI.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/WearableItems/" + headItem.headItemStyle);
//			hatUI.GetComponent<Image> ().color = SerializableColor.ToColor(headItem.headItemColor);
//			hatUI.GetComponent<RectTransform> ().localScale = Vector3.one;
//			hatUI.GetComponent<DragWearableItem> ().canvas = canvas;
//			hatUI.GetComponent<DragWearableItem> ().itemPrefab = Resources.Load<GameObject> ("Prefabs/WearableItems/" + headItem.headItemStyle);
		}
	}
}
