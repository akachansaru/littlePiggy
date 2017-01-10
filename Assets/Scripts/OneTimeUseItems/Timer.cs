using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	private float currentTime;
	private float endTime;
    private Text timerText;

	void Start() {
		timerText = GetComponent<Text> ();
	}

	void Update() {
		if (currentTime < endTime) {
			currentTime += Time.deltaTime;
			DisplayTime();
		} else {
			gameObject.SetActive(false);
		}
	}

	void DisplayTime() {
		timerText.text = "Time: " + Mathf.Round((endTime - currentTime) * 10f) / 10f;
	}

	public void StartTimer(float time) {
		gameObject.SetActive (true);
		currentTime = 0f;
		endTime = time;
	}
}
