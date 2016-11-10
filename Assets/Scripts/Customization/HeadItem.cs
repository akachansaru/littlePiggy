using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[Serializable]
public class HeadItem : Item {
	public SerializableColor headItemColor;
//	public SpriteRenderer headItemPattern;
	public int clothPrice;
	public bool currentlyWearing;

	private int level;

	public int Level {
		get { return level; }
	}

	public HeadItem(string style, SerializableColor color, SpriteRenderer pattern, int statIncrease, int price, bool wearing) {
		base.itemName = style;
		base.itemStat = "Jump";
		headItemColor = color;
//		headItemPattern = pattern;
		base.itemStatIncrease = statIncrease;
		clothPrice = price;
		currentlyWearing = wearing;
		level = 0;
	}

	public override void AddInfo(ItemInfo itemInfo) {
		WearableItemInfo wearableItemInfo = itemInfo as WearableItemInfo;
		wearableItemInfo.name = base.itemName;
		wearableItemInfo.itemName = base.itemName;
		// UNDONE Add image
		wearableItemInfo.itemStat = base.itemStat;
		wearableItemInfo.statIncrease = base.itemStatIncrease;
		wearableItemInfo.clothPrice = clothPrice;
	}

	public void LevelUp() {
		level++;
		base.itemStatIncrease++;
		clothPrice += 5;
	}

	public override string ToString () {
		return ("Head item in " + base.itemName + ", stat boost " + base.itemStatIncrease + ", color " + headItemColor + ". Currently wearing: " + currentlyWearing);
	}

	public override bool Equals (object obj) {
		if (obj == null) return false;
		HeadItem objAsHeadItem = obj as HeadItem;
		if (objAsHeadItem == null) return false;
		else return Equals(objAsHeadItem);
	}

	public bool Equals (HeadItem other) {
		if (other == null) return false;
		return (this.itemName.Equals (other.itemName) && this.headItemColor.Equals (other.headItemColor));
	}

	public bool Equals(GameObject other) {
		if (other == null) return false;
		return (other.name.StartsWith(this.itemName) && (this.headItemColor.Equals(SerializableColor.FromColor(other.GetComponent<SpriteRenderer>().color))));
	}

	public override int GetHashCode() {
		int hash = 13;
		hash = (hash * 7) + itemName.GetHashCode ();
		hash = (hash * 7) + headItemColor.GetHashCode ();
		return hash;
	}
}

public class HeadItemMethods : MonoBehaviour {

	public static void ApplyAttributes(HeadItem headItem, GameObject headItemGameObject) {
		Debug.Log ("ApplyAttributes");
		headItemGameObject.GetComponent<SpriteRenderer>().color = SerializableColor.ToColor(headItem.headItemColor);
	}

	public static GameObject LoadPrefab(HeadItem headItem) {
		Debug.Log ("LoadPrefab");
		return Resources.Load<GameObject>("Prefabs/WearableItems/" + headItem.itemName);
	}

	public static GameObject LoadUIPrefab() {
		return Resources.Load<GameObject> ("Prefabs/WearableItems/UI/ItemUI");
	}

	public static void ApplyUIAttributes(HeadItem headItem, GameObject headItemGameObject, Canvas canvas) {
//		Instantiate (itemUIPrefab, hatContent.transform) as GameObject;
//		GameObject ui = Resources.Load<GameObject> ("Prefabs/WearableItems/UI/ItemUI");
		Debug.Log("ApplyUIAttributes");
		headItemGameObject.name = headItem.itemName + "UI";
		headItemGameObject.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/WearableItems/" + headItem.itemName);
		headItemGameObject.GetComponent<Image> ().color = SerializableColor.ToColor(headItem.headItemColor);
		headItemGameObject.GetComponent<RectTransform> ().localScale = Vector3.one;
		headItemGameObject.GetComponent<DragWearableItem> ().canvas = canvas;
		headItemGameObject.GetComponent<DragWearableItem> ().itemPrefab = Resources.Load<GameObject> ("Prefabs/WearableItems/" + headItem.itemName);
//		return ui;
	}

//	public static HeadItem FromGameObject(GameObject headItem) {
//		Debug.Log ("FromGameObject");
//		string style = headItem.GetComponent<SpriteRenderer>().sprite.name;
//		Color color = headItem.GetComponent<SpriteRenderer>().color;
//		return (new HeadItem(style, SerializableColor.FromColor(color), new SpriteRenderer(), false));
//	}
}
