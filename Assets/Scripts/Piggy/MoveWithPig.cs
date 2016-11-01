using UnityEngine;
using System.Collections;

public class MoveWithPig : MonoBehaviour {

	// Moves the kick box and items so they're always in the right position relative to the pig
	public void ChangeSides() {
		// FIXME Hat is slow to switch sides when piggy turns around in the air
		gameObject.transform.localPosition = new Vector2(-gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
	}

}
