using UnityEngine;
using System.Collections;

public class Mud : MonoBehaviour {
	public float speedModifier;
	public float jumpModifier;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag(ConstantValues.tags.player)) {
			other.gameObject.GetComponent<Pig> ().ModifySpeed (speedModifier);
			other.gameObject.GetComponent<Pig> ().ModifyJump (LevelManager.levelManager.levelInstance.levelDonutCount / jumpModifier 
				- LevelManager.levelManager.levelInstance.levelDonutCount);
		}
		if (other.gameObject.CompareTag(ConstantValues.tags.enemy)) {
			other.gameObject.GetComponent<Enemy> ().ModifySpeed (speedModifier);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag(ConstantValues.tags.player)) {
			other.gameObject.GetComponent<Pig> ().ModifySpeed (1f);
			other.gameObject.GetComponent<Pig> ().ModifyJump (0);
		}
		if (other.gameObject.CompareTag(ConstantValues.tags.enemy)) {
			other.gameObject.GetComponent<Enemy> ().ModifySpeed (1/speedModifier);
		}
	}
}
