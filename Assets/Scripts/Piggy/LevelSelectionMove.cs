using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionMove : PiggyMovement {

	/// <summary>
	/// Moves the piggy on ground or in air based on input if not over max horizontal speed.
	/// </summary>
	public override void MovePiggy(Rigidbody2D rb, Animator piggyAnimator, GameObject standingOn, float jumpMovementScale) {
		// FIXME Piggy spazzes when forward and backward are pressed at the same time (queue it up?)
		if ((rb.velocity.x <= LevelManager.piggySpeed) && piggyAnimator.GetBool (ConstantValues.piggyAnimatorParameterNames.forward)) {
			//			if (!piggyAnimator.GetBool (ConstantValues.piggyAnimatorParameterNames.backward)) {
			if (standingOn) {
				// Move forward on the ground
				rb.AddForce (Vector3.right * LevelManager.piggySpeed, ForceMode2D.Force);
			} else {
				// Move forward in the air
				rb.AddForce (Vector3.right * LevelManager.piggySpeed * jumpMovementScale, ForceMode2D.Force);
			}
			//			}
		} 
		if ((-rb.velocity.x <= LevelManager.piggySpeed) && piggyAnimator.GetBool (ConstantValues.piggyAnimatorParameterNames.backward)) {
			//			if (!piggyAnimator.GetBool (ConstantValues.piggyAnimatorParameterNames.forward)) {
			if (standingOn) {
				// Move backward on the ground
				rb.AddForce (Vector3.left * LevelManager.piggySpeed, ForceMode2D.Force);
			} else {
				// Move backward in the airS
				rb.AddForce (Vector3.left * LevelManager.piggySpeed * jumpMovementScale, ForceMode2D.Force);
			}
			//			}
		}
	}
}
