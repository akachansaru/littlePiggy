using UnityEngine;
using System.Collections;

// Same as donut as far as movement
public class ClothObject : MonoBehaviour {

	public float maxDeltaY;
	public float oSpeed;
	public float rotScale;
	public int value;

	private float yPos;
	private float theta;
	private float fromDegrees;

	void Start() {
		yPos = transform.localPosition.y;
		fromDegrees = Mathf.PI / 180F;
		theta = 0F;
	}

	void Update() {
		if (!LevelManager.paused) {
			theta += oSpeed * Time.deltaTime;
			if (theta > 360F) {
				theta -= 360F;
			}
			transform.localPosition = new Vector3(transform.localPosition.x, yPos + maxDeltaY * Mathf.Sin(theta * fromDegrees), transform.localPosition.z);
			transform.Rotate(new Vector3(0F, 0F, (maxDeltaY * Mathf.Sin(theta * fromDegrees))/rotScale));
		}
	}

	// Allows the piggy to collect the cloth
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag(ConstantValues.tags.player)){
			LevelManager.levelManager.levelInstance.clothCollected += value;
			gameObject.SetActive(false);
		}
	}
}
