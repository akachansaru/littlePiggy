using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OneTimeItemInfo : ItemInfo {

	public int statDuration;
	public int donutPrice;

	public override Item MakeItem() {
		return new OneTimeItem (base.itemName, base.itemStat, base.statIncrease, statDuration);
	}
}
