using System;
using UnityEngine;

public abstract class PiggyMovement : MonoBehaviour {
	public abstract void MovePiggy(Rigidbody2D rb, Animator piggyAnimator, GameObject standingOn, float jumpMovementScale);
}

