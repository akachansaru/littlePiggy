using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class SaveValues {

	public int SafeDonutCount; // Donut count before entering the level
	public int SafeClothCount; // Cloth count before entering the level
	public bool[] CompletedLevels;
	public bool musicMuted;
	public float musicVolume;
	public List<HeadItem> headItems;
	public List<HeadItem> unlockedHeadItems;
	public List<OneTimeItem> oneTimeItems;
}
