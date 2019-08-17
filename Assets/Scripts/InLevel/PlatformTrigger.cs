using UnityEngine;
using System.Collections;

public class PlatformTrigger : MonoBehaviour {
	
	public BoxCollider2D platform;
	public bool above = false;

	void OnTriggerEnter2D(Collider2D other) {
		above = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		above = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		above = false;
	}
}