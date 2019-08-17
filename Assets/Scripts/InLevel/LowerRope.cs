using UnityEngine;
using System.Collections;

public class LowerRope : DoOnButtonPress {
	public float speed = 3f;
	public Transform lowestPoint;
	public Transform highestPoint;

	private float ropeBottom; // Position of rope bottom as it moves
	private bool active;

	public void Start() {
		active = false;
	}

	public void Update() {
		ropeBottom = transform.position.y - (transform.localScale.y / 2);
		if (active && (ropeBottom >= lowestPoint.position.y)) {
//			Debug.Log ("Lowering");
			transform.Translate(new Vector2 (0, -Time.deltaTime * speed));
		} else if (!active && (ropeBottom <= highestPoint.position.y)) {
//			Debug.Log ("Raising");
			transform.Translate(new Vector2 (0, Time.deltaTime * speed));
		}
	}

	public override void Activate () {
		// Lower rope
		active = true;
	}

	public override void Deactivate () {
		// Raise rope
		active = false;
	}
} 