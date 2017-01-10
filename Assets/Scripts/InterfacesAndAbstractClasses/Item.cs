using System;

[Serializable]
public abstract class Item {
	public string itemName;
	public string itemStat;
	public int itemStatIncrease;

	public abstract void AddInfo (ItemInfo itemInfo);
}