using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		// Save the game at checkpoints in case the player falls off
		if (other.gameObject.tag.Contains(ConstantValues.tags.player)) {
			Debug.Log("Checkpoint");
			LevelManager.levelManager.levelInstance.lastCheckpoint = gameObject;
            //			LevelManager.levelManager.SaveLevelInstance();
        }
    }
}
