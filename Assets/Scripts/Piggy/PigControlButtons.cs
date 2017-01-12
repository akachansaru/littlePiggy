using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Attached to each pig control icon to make it into a button.
/// </summary>
public class PigControlButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public void OnPointerDown(PointerEventData eventData) {
        if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.forward) {
            PigControlInput.pig.GetComponent<PigControlInput>().MoveForward();
        } else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.backward) {
            PigControlInput.pig.GetComponent<PigControlInput>().MoveBackward();
        } else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.jump) {
            PigControlInput.pig.GetComponent<PigControlInput>().Jump();
        } else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.kick) {
            PigControlInput.pig.GetComponent<PigControlInput>().Kick();
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.forward) {
            PigControlInput.pig.GetComponent<PigControlInput>().StopForward();
        } else if (gameObject.name == ConstantValues.piggyAnimatorParameterNames.backward) {
            PigControlInput.pig.GetComponent<PigControlInput>().StopBackward();
        }
    }
}
