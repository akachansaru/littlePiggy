using UnityEngine;
using System.Collections;

public class Door : DoOnButtonPress {

	public override void Activate () {
		CameraController.cameraController.PanCamera (transform.position);
		gameObject.SetActive (false);
	}

	public override void Deactivate () {
		CameraController.cameraController.PanCamera (transform.position);
		gameObject.SetActive (true);
	}
}
