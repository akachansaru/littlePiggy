using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class Ads : MonoBehaviour {

	public void PlayAd() {
		if (Advertisement.IsReady ()) {
			Advertisement.Show ("rewardedVideo", new ShowOptions (){ resultCallback = HandleAdResult });
		}
	}

    void HandleAdResult(ShowResult result) {
		switch (result) {
		case ShowResult.Finished:
			Debug.Log ("Ad finished.");
			Analytics.CustomEvent ("Ad finished");
			RewardPlayer ();
			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad skipped.");
			Analytics.CustomEvent("Ad skipped");
			break;
		case ShowResult.Failed:
			Debug.Log ("Ad failed.");
			Analytics.CustomEvent("Ad failed");
			break;
		}
	}

	void RewardPlayer() {
		// UNDONE Add in chance of other rewards like new dyes or patterns
		GlobalControl.Instance.savedData.SafeClothCount += 5;
		Debug.Log ("Rewarded 5 cloth.");
		GlobalControl.Instance.Save ();
	}
}
