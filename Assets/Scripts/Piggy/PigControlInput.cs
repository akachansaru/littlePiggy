using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PigControlInput : MonoBehaviour {

    public static GameObject pig;
    public static Animator piggyAnimator;

    public AnimationClip kickAnimation; // Can be forward or backward since they should be the same time

    #region UI button objects
    public GameObject forwardButton;
    public GameObject backwardButton;
    public GameObject jumpButton;
    public GameObject kickButton;
    #endregion

    public bool jump = false;
    private bool kicked = false;
    private float kickAnimationTime;

    public void Start() {
        piggyAnimator = GetComponentInChildren<Animator>();
        kickAnimationTime = kickAnimation.length;
        pig = gameObject;
        Debug.Log("Animator: " + piggyAnimator);
    }

    public bool Kicking {
        get { return piggyAnimator.GetBool(ConstantValues.piggyAnimatorParameterNames.kick); }
    }

    //public bool Jump {
    //    get { return jump; }
    //}

    public bool Jumping {
        get { return piggyAnimator.GetBool(ConstantValues.piggyAnimatorParameterNames.jump); }
    }

    public bool Kicked {
        get { return kicked; }
        set { kicked = value; }
    }

    public Animator PiggyAnimator {
        get { return piggyAnimator; }
    }

    public void MoveForward() {
        //		if (!piggyAnimator.GetBool (ConstantValues.piggyAnimatorParameterNames.backward)) {
        piggyAnimator.SetBool(ConstantValues.piggyAnimatorParameterNames.forward, true);
        //		}
    }

    public void MoveBackward() {
        //		if (!piggyAnimator.GetBool(ConstantValues.piggyAnimatorParameterNames.forward)) {
        piggyAnimator.SetBool(ConstantValues.piggyAnimatorParameterNames.backward, true);
        //		}
    }

    public void StopForward() {
        piggyAnimator.SetBool(ConstantValues.piggyAnimatorParameterNames.forward, false);
    }

    public void StopBackward() {
        piggyAnimator.SetBool(ConstantValues.piggyAnimatorParameterNames.backward, false);
    }

    public void Jump() {
        Debug.Log("Jump button");
        jump = true;
    }

    public void StartJumpAnimation() {
        piggyAnimator.SetBool(ConstantValues.piggyAnimatorParameterNames.jump, true);
        ChangeButtonStatusIndividual(kickButton, false);
        ChangeButtonStatusIndividual(jumpButton, false);
        if (GetComponent<Pig>()) {
            GetComponent<Pig>().StandingOn = null; // TODO I think this causes a problem sometimes when checking if piggy is leaving what he was standing on
        }
        Debug.Log("Start jump animation");
    }

    public void Kick() {
        Debug.Log("Kick button");
        piggyAnimator.SetBool(ConstantValues.piggyAnimatorParameterNames.kick, true);
        ChangeButtonStatusAll(false);
        StartCoroutine(KickDuration());
    }

    IEnumerator KickDuration() {
        yield return new WaitForSeconds(kickAnimationTime);
        piggyAnimator.SetBool(ConstantValues.piggyAnimatorParameterNames.kick, false);
        kicked = false;
        if (CameraController.cameraController) {
            if (!CameraController.cameraController.Panning) {
                ChangeButtonStatusAll(true);
            }
        } else {
            ChangeButtonStatusAll(true);
        }
    }

    /// <summary>
    /// Activate or deactivate buttons while an animation is happening.
    /// </summary>
    /// <param name="interactable">If set to <c>true</c> interactable.</param>
    public void ChangeButtonStatusAll(bool interactable) {
        ChangeButtonStatusIndividual(forwardButton, interactable);
        ChangeButtonStatusIndividual(backwardButton, interactable);
        ChangeButtonStatusIndividual(jumpButton, interactable);
        ChangeButtonStatusIndividual(kickButton, interactable);
    }

    public void ChangeButtonStatusIndividual(GameObject button, bool interactable) {
        Debug.Log("Changed button " + button.name + " to " + interactable);
        float alpha;
        if (!interactable) {
            alpha = 0.5f;
        } else {
            alpha = 1f;
        }
        Image buttonImage = button.GetComponent<Image>();
        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, alpha);
        buttonImage.raycastTarget = interactable;
    }

    /// <summary>
    /// SetActive(isActive) is called for all of the pig control buttons.
    /// </summary>
    /// <param name="isActive">If set to <c>true</c> is active.</param>
    public void ToggleActiveMovementButtons(bool isActive) {
        forwardButton.SetActive(isActive);
        backwardButton.SetActive(isActive);
        jumpButton.SetActive(isActive);
        kickButton.SetActive(isActive);
    }
}
