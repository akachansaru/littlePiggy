using UnityEngine;
using System.Collections;

public class BounceWithPig : MonoBehaviour {
	public float bounceHeight = 0.5f;
	public float animationInterval = 0.05f;
	public float nextAnimation = 0f;

	void Update () {
		if (Time.time > nextAnimation) {
			transform.position = new Vector2 (transform.position.x, transform.position.y + bounceHeight);
			bounceHeight = -bounceHeight;
			nextAnimation = Time.time + animationInterval;
		}
	}
}
