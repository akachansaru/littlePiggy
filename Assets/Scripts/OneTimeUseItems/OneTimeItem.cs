using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[Serializable]
public class OneTimeItem : Item {
	public int itemStatDuration;
	public int amountOwned;

	public OneTimeItem(String name, string stat, int statIncrease, int statDuration) {
		base.itemName = name;
		base.itemStat = stat;
		base.itemStatIncrease = statIncrease;
		itemStatDuration = statDuration;
		amountOwned = 1;
	}
	// FIXME Timers are messed up now

	/// <summary>
	/// Adds the info to the OneTimeItemInfo script component of the GameObject.
	/// </summary>
	/// <param name="oneTimeItem">One time item.</param>
	public override void AddInfo(ItemInfo itemInfo) {
		OneTimeItemInfo oneTimeItemInfo = itemInfo as OneTimeItemInfo;
		oneTimeItemInfo.name = base.itemName;
		oneTimeItemInfo.itemName = base.itemName;
		// UNDONE Add image
		oneTimeItemInfo.itemStat = base.itemStat;
		oneTimeItemInfo.statIncrease = base.itemStatIncrease;
		oneTimeItemInfo.statDuration = itemStatDuration;
	}

	public override string ToString () {
		return (itemName + ": " + itemStat + " + " + itemStatIncrease.ToString() + " for " + itemStatDuration.ToString() + " seconds.");
	}

	public override bool Equals (object obj) {
		if (obj == null) return false;
		OneTimeItem objAsItem = obj as OneTimeItem;
		if (objAsItem == null) return false;
		else return Equals(objAsItem);
	}

	public bool Equals (OneTimeItem other) {
		if (other == null) return false;
		return (this.itemName.Equals (other.itemName) && this.itemStat.Equals (other.itemStat) && 
			this.itemStatIncrease.Equals (other.itemStatIncrease) && this.itemStatDuration.Equals (other.itemStatDuration));
	}

	public override int GetHashCode() {
		int hash = 13;
		hash = (hash * 7) + itemName.GetHashCode ();
		hash = (hash * 7) + itemStat.GetHashCode ();
		hash = (hash * 7) + itemStatIncrease.GetHashCode ();
		hash = (hash * 7) + itemStatDuration.GetHashCode ();
		return hash;
	}
}

