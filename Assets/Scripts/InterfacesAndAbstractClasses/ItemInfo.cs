using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public abstract class ItemInfo : MonoBehaviour {

	public string itemName;
	public Image itemImage;
	public string itemStat;
	public int statIncrease;

	public abstract Item MakeItem ();
}

