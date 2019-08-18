using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[Serializable]
public class HeadItem {
	public string headItemStyle;
	public SerializableColor headItemColor;
//	public SpriteRenderer headItemPattern;
	public int statBoost;
	public int clothPrice;
	public bool currentlyWearing;

	private int level;

	public HeadItem(string style, SerializableColor color, SpriteRenderer pattern, int stat, int price, bool wearing) {
		headItemStyle = style;
		headItemColor = color;
//		headItemPattern = pattern;
		statBoost = stat;
		clothPrice = price;
		currentlyWearing = wearing;
		level = 0;
	}

	public int Level {
		get { return level; }
	}

	public void LevelUp() {
		level++;
		statBoost++;
		clothPrice += 5;
	}

	public override string ToString () {
		return ("Head item in " + headItemStyle + ", stat boost " + statBoost + ", color " + headItemColor + ". Currently wearing: " + currentlyWearing);
	}

	public override bool Equals (object obj) {
		if (obj == null) return false;
		HeadItem objAsHeadItem = obj as HeadItem;
		if (objAsHeadItem == null) return false;
		else return Equals(objAsHeadItem);
	}

	public bool Equals (HeadItem other) {
		if (other == null) return false;
		return (this.headItemStyle.Equals (other.headItemStyle) && this.headItemColor.Equals (other.headItemColor));
	}

	public bool Equals(GameObject other) {
		if (other == null) return false;
		Debug.Log ("Other.name = " + other.name + ". Other.color = " + SerializableColor.FromColor(other.GetComponent<SpriteRenderer>().color));
		Debug.Log ("This.name = " + this.headItemStyle + ". This.color = " + this.headItemColor);
		Debug.Log ("Names equate: " + other.name.StartsWith(this.headItemStyle));
		Debug.Log ("Colors equate: " + this.headItemColor.Equals(SerializableColor.FromColor(other.GetComponent<SpriteRenderer>().color)));
		return (other.name.StartsWith(this.headItemStyle) && (this.headItemColor.Equals(SerializableColor.FromColor(other.GetComponent<SpriteRenderer>().color))));
	}

	public override int GetHashCode() {
		int hash = 13;
		hash = (hash * 7) + headItemStyle.GetHashCode ();
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
		return Resources.Load<GameObject>("Prefabs/WearableItems/" + headItem.headItemStyle);
	}

	public static GameObject LoadUIPrefab() {
		return Resources.Load<GameObject> ("Prefabs/WearableItems/UI/ItemUI");
	}

	public static void ApplyUIAttributes(HeadItem headItem, GameObject headItemGameObject, Canvas canvas) {
//		Instantiate (itemUIPrefab, hatContent.transform) as GameObject;
//		GameObject ui = Resources.Load<GameObject> ("Prefabs/WearableItems/UI/ItemUI");
		Debug.Log("ApplyUIAttributes");
		headItemGameObject.name = headItem.headItemStyle + "UI";
		headItemGameObject.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/WearableItems/" + headItem.headItemStyle);
		headItemGameObject.GetComponent<Image> ().color = SerializableColor.ToColor(headItem.headItemColor);
		headItemGameObject.GetComponent<RectTransform> ().localScale = Vector3.one;
		headItemGameObject.GetComponent<DragWearableItem> ().canvas = canvas;
		headItemGameObject.GetComponent<DragWearableItem> ().itemPrefab = Resources.Load<GameObject> ("Prefabs/WearableItems/" + headItem.headItemStyle);
//		return ui;
	}

//	public static HeadItem FromGameObject(GameObject headItem) {
//		Debug.Log ("FromGameObject");
//		string style = headItem.GetComponent<SpriteRenderer>().sprite.name;
//		Color color = headItem.GetComponent<SpriteRenderer>().color;
//		return (new HeadItem(style, SerializableColor.FromColor(color), new SpriteRenderer(), false));
//	}
}
