using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject piggy;
	public float cameraOffset;

	public static CameraController cameraController;
	private static GameObject newGround;

	private GameObject ground;
	private bool panning;
	private float panTime = 0.6F;
	private float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;
	private Vector3 targetPosition;

	public bool Panning {
		get { return panning; }
	}

	public GameObject Ground {
		get { return ground; }
		set { ground = value; }
	}

	public void PanCamera(Vector2 objectPosition) {
		piggy.GetComponent<Pig> ().ChangeButtonStatusAll (false);
		panning = true;
		// Keep camera's z position
		targetPosition = new Vector3(objectPosition.x, objectPosition.y, transform.position.z);
		Debug.Log ("Panning camera");
	}
		
	void Start() {
		panning = false;
		cameraController = this;
	}

	void Update () {
		if (!piggy.GetComponent<Pig> ().GameStart && !panning) {
//			if (newGround) {
			targetPosition = new Vector3 (piggy.transform.position.x, (float)ground.GetComponent<Ground>().heightLevel + cameraOffset, transform.position.z);
			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime);
//				Debug.Log ("New ground");
//				if (velocity.magnitude <= 0.01f) {
//					ground = newGround;
//					newGround = null;
//				}
//			} else {
//				Debug.Log ("Normal camera");
//				transform.position = new Vector3 (piggy.transform.position.x, (float)ground.GetComponent<Ground>().heightLevel + cameraOffset, transform.position.z);
//			}
		} else if (panning) {
			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, panTime);
			if (velocity.magnitude <= 0.01f) {
				panning = false;
				transform.position =  new Vector3 (piggy.transform.position.x, (float)ground.GetComponent<Ground>().heightLevel + cameraOffset, transform.position.z);
				piggy.GetComponent<Pig> ().ChangeButtonStatusAll (true);
				Debug.Log ("Done panning");
			}
		} 
	}
}
