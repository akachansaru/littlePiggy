using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class LevelSaveValues {

	// □ donutsCollected
	// □ lvlDonutCount (?)
	// □ Where the player is in the level and what direction and state it's in
	// □ Which enemies have been killed
	// □ Which donuts have been collected
	// □ Any blocks etc. the player has moved (for puzzles)

	public GameObject lastCheckpoint;
	public int donutsCollected;
	public int clothCollected;
	public int levelDonutCount; // Starting donuts when entering the level from level payment
	public List<GameObject> donutObjectsCollected;
	public List<GameObject> enemiesKilled;

	// I think this is only needed if progress will reset when player falls off
	public void Clone(LevelSaveValues toClone) {
		this.lastCheckpoint = toClone.lastCheckpoint;
		// this.donutsCollected = toClone.donutsCollected;
		// this.levelDonutCount = toClone.levelDonutCount;
		// this.donutObjectsCollected = toClone.donutObjectsCollected;
		// this.enemiesKilled = toClone.enemiesKilled;
	}
	
}
