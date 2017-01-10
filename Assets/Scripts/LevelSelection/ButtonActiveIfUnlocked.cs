using UnityEngine;
using UnityEngine.UI;

public class ButtonActiveIfUnlocked : MonoBehaviour {

	public int levelNumber;

	void Start() {
		SpriteRenderer boxColor = gameObject.GetComponent<SpriteRenderer> ();

		if (!GlobalControl.Instance.savedData.CompletedLevels[levelNumber]) {
			// Piggy can go through the box without anything happening
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			boxColor.color = new Color(boxColor.color.r, boxColor.color.g, boxColor.color.b, 0.5f);
		} else if (GlobalControl.Instance.savedData.CompletedLevels[levelNumber] && !gameObject.GetComponent<BoxCollider2D>().enabled) {
			// Piggy will hit the box and can enter the corresponding level
			gameObject.GetComponent<BoxCollider2D>().enabled = true;
			boxColor.color = new Color(boxColor.color.r, boxColor.color.g, boxColor.color.b, 1f);
		}
	}
}
