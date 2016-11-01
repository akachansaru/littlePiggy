using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[Serializable]
public class OneTimeItem {

	public string itemName;
	public string itemStat;
	public int itemStatIncrease;
	public int itemStatDuration;
	public int amountOwned;

	public OneTimeItem(String name, string stat, int statIncrease, int statDuration) {
		itemName = name;
		itemStat = stat;
		itemStatIncrease = statIncrease;
		itemStatDuration = statDuration;
		amountOwned = 1;
	}

	// Adds the info to the OneTimeItemInfo script component of the GameObeject
	public void AddInfo(GameObject oneTimeItem) {
		OneTimeItemInfo itemInfo = oneTimeItem.GetComponent<OneTimeItemInfo> ();
		itemInfo.name = this.itemName;
		itemInfo.itemName = this.itemName;
		// UNDONE Add image
		itemInfo.stat = this.itemStat;
		itemInfo.statIncrease = this.itemStatIncrease;
		itemInfo.statDuration = this.itemStatDuration;
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

