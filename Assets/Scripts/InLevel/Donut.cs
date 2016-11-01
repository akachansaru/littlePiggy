using UnityEngine;
using System.Collections;

// FIXME Donuts appear in front of platforms sometimes and in front of level complete screen
public class Donut : MonoBehaviour {

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

	// Allows the piggy to collect the donut and ends the level if the donut is the goal donut
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag(ConstantValues.tags.player)){
			LevelManager.levelManager.levelInstance.donutsCollected += value;
			LevelManager.levelManager.levelInstance.donutObjectsCollected.Add(transform.parent.gameObject);
			if (gameObject.transform.parent.tag.Contains(ConstantValues.tags.goal)) {
				Debug.Log ("Gooooal!");
				LevelManager.levelComplete = true;
			}
			transform.parent.gameObject.SetActive(false);
		}
	}
}
