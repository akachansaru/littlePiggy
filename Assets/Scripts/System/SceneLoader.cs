using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {
	
    public void LoadScene(string sceneName) {
		GlobalControl.Instance.Load();
		SceneManager.LoadScene(sceneName);
	}

	public static void LoadItemCustomization(HeadItem headItem) {
		GlobalControl.Instance.itemToModify = headItem;
		Debug.Log ("Name " + headItem);
		SceneManager.LoadScene("ItemCustomization");
	}
}
