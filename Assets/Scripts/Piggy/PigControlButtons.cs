using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PigControlButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public void OnPointerDown(PointerEventData eventData) {
		if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.forward) {
//			Pig.player.GetComponent<Pig>().MoveForward ();
			Pig.player.GetComponent<Pig>().MoveWithInput(ConstantValues.piggyAnimatorParameterNames.forward, true);
		} else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.backward) {
//			Pig.player.GetComponent<Pig>().MoveBackward ();
			Pig.player.GetComponent<Pig>().MoveWithInput(ConstantValues.piggyAnimatorParameterNames.backward, true);
		} else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.jump) {
			Pig.player.GetComponent<Pig> ().Jump ();
		} else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.kick) {
			Pig.player.GetComponent<Pig> ().Kick ();
		}
	}

	public void OnPointerUp(PointerEventData eventData) {
		if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.forward) {
//			Pig.player.GetComponent<Pig> ().StopForward ();
			Pig.player.GetComponent<Pig>().MoveWithInput(ConstantValues.piggyAnimatorParameterNames.forward, false);
		} else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.backward) {
//			Pig.player.GetComponent<Pig> ().StopBackward ();
			Pig.player.GetComponent<Pig>().MoveWithInput(ConstantValues.piggyAnimatorParameterNames.backward, false);
		}
	}
}
