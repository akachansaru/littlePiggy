using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoreEntry : MonoBehaviour {
	public string storeName;

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag(ConstantValues.tags.player)) {
			SceneManager.LoadScene (storeName);
		}
	}
}
