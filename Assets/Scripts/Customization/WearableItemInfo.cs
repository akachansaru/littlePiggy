using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WearableItemInfo : ItemInfo {

	public Color itemColor;
	public int clothPrice;

	public override Item MakeItem () {
		return new HeadItem (base.itemName, new SerializableColor(itemColor), new SpriteRenderer(), base.statIncrease, clothPrice, false);
	}
}
