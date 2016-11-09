using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Attached to each pig control icon to make it into a button.
/// </summary>
public class PigControlButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public void OnPointerDown(PointerEventData eventData) {
		if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.forward) {
			Pig.player.GetComponent<Pig>().MoveForward ();
		} else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.backward) {
			Pig.player.GetComponent<Pig>().MoveBackward ();
		} else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.jump) {
			Pig.player.GetComponent<Pig> ().Jump ();
		} else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.kick) {
			Pig.player.GetComponent<Pig> ().Kick ();
		}
	}

	public void OnPointerUp(PointerEventData eventData) {
		if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.forward) {
			Pig.player.GetComponent<Pig> ().StopForward ();
		} else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.backward) {
			Pig.player.GetComponent<Pig> ().StopBackward ();
		}
	}
}
