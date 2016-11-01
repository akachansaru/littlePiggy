using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Attached to the UI number display gameObject
public class DisplayAmountCollected : MonoBehaviour {

	private Text countText; // The text component on the display box
	public bool donut;
	public bool cloth;
	public bool inLevel;

	void Start () {
		countText = GetComponent<Text>();
	}

	void Update () {
		// TODO Don't need this every update. Update the count when a donut or cloth is collected
		UpdateDonutCount(inLevel);
	}

	void UpdateDonutCount(bool inLevel) {
		if (inLevel) {
			if (donut) {
			LevelManager.levelManager.levelInstance.levelDonutCount = LevelManager.levelPayment + 
				LevelManager.levelManager.levelInstance.donutsCollected;
			countText.text = LevelManager.levelManager.levelInstance.levelDonutCount.ToString();
			}
			if (cloth) {
				countText.text = LevelManager.levelManager.levelInstance.clothCollected.ToString();
			}
		} else {
			if (donut) {
				countText.text = GlobalControl.Instance.savedData.SafeDonutCount.ToString ();
			}
			if (cloth) {
				countText.text = GlobalControl.Instance.savedData.SafeClothCount.ToString ();
			}
		}
	}
}
