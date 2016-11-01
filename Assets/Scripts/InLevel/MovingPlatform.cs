using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public float speed; // Speed of platform
	public Vector2 direction; // Direction platform moves
	public bool alwaysMoving; // If false, waits for player to jump on it to start moving

	private bool canMove;

	void Start () {
		if (alwaysMoving) {
			canMove = true;
		} else {
			canMove = false;
		}
	}

	void Update () {
		if (canMove) {
			transform.Translate(new Vector2(Time.deltaTime * speed * direction.x, Time.deltaTime * speed * direction.y));
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag(ConstantValues.tags.player) && !alwaysMoving) {
			Debug.Log("Player onboard");
			canMove = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(ConstantValues.tags.path)) {
			direction = other.gameObject.GetComponent<Path>().pathChange;
		}
	}
}
