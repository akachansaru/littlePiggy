using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoreEntry : MonoBehaviour {
	public string storeName;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag(ConstantValues.tags.player)) {
			SceneManager.LoadScene (storeName);
		}
	}
}
