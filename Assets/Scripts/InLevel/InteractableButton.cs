using UnityEngine;
using System.Collections;

public class InteractableButton : MonoBehaviour {

	public bool kickButton;
	public bool stickyButton;
	public bool permanentPress;
	public GameObject linked; // GameObject with a script inherited from DoOnButtonPress.cs
	public bool inverse; // If pressing deactivates linked instead of activating

	private bool buttonPressed;

	public void KickButton() {
		if (kickButton) {
            // Toggle the kick button
            PressButton (!buttonPressed);
		}
	}

	// Use this for initialization
	void Start () {
		buttonPressed = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag (ConstantValues.tags.player) && !kickButton) {
			Debug.Log ("Stepped on button");
			if (!buttonPressed) {
				// Unpressed => pressed
				if (!inverse) {
					PressButton (true);
				} else {
					PressButton (false);
				}
			} else if (buttonPressed && stickyButton && !permanentPress) {
				// Sticky pressed => unpressed
				if (!inverse) {
					PressButton (false);
				} else {
					PressButton (true);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.CompareTag(ConstantValues.tags.player) && buttonPressed && !stickyButton) {
			// Pressed => unpressed
			if (!inverse) {
				PressButton (false);
			} else {
				PressButton (true);
			}
		}
	}

	void PressButton(bool activate) {
		transform.GetChild(0).gameObject.SetActive(buttonPressed);
		gameObject.GetComponent<SpriteRenderer> ().enabled = !buttonPressed;
		buttonPressed = !buttonPressed;
		if (linked) {
			if (activate) {
				linked.GetComponent<DoOnButtonPress> ().Activate ();
			} else {
				linked.GetComponent<DoOnButtonPress> ().Deactivate ();
			}
		}
		Debug.Log ("Pressed button " + gameObject.name);
	}
}
