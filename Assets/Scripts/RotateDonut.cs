using UnityEngine;
using System.Collections;

public class RotateDonut : MonoBehaviour {

	public float rotationSpeed;

	void Update () {
		transform.Rotate (new Vector3 (0f, 0f, rotationSpeed));
	}
}
