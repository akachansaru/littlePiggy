using UnityEngine;
using System.Collections;

public class MoveWithPig : MonoBehaviour {

	// Moves the kick box and items so they're always in the right position relative to the pig
	public void ChangeSides() {
		gameObject.transform.localPosition = new Vector2(-gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
	}

}
