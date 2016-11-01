using UnityEngine;
using System.Collections;

public class LevelEntries : MonoBehaviour {
	public LevelSetup levelSetup;
	public string levelName;
	public int minLevelpayment;
	public int maxLevelpayment;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag(ConstantValues.tags.player)) {
			levelSetup.SelectLevelPayment(levelName + LevelSetup.delimiter + minLevelpayment + LevelSetup.delimiter + maxLevelpayment);
			other.gameObject.GetComponent<Pig> ().StopForward ();
			other.gameObject.GetComponent<Pig> ().StopBackward ();
		}
	}
}
