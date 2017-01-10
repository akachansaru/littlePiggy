using UnityEngine;
using System.Collections;

// Attach to any gameObject that can fall off platforms
public class DestroyOnFall : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag(ConstantValues.tags.catcher)) {
            // Object has fallen off
            Destroy(gameObject);
		}
	}
}
