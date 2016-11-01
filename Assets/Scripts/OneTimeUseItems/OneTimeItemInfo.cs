using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OneTimeItemInfo : MonoBehaviour {

	public string itemName;
	public Image image;
	public string stat;
	public int statIncrease;
	public int statDuration;
	public int price;

	public OneTimeItem MakeItem() {
		return new OneTimeItem (itemName, stat, statIncrease, statDuration);
	}
}
