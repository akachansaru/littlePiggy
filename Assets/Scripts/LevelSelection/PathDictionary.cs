using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDictionary : MonoBehaviour {
    public static Dictionary<GameObject, string> paths = new Dictionary<GameObject, string>();

	void Start () {
        for (int c = 0; c < transform.childCount; c++) {
            if (gameObject.transform.GetChild(c).gameObject.GetComponent<PathPart>()) {
                paths.Add(gameObject.transform.GetChild(c).gameObject, 
                    gameObject.transform.GetChild(c).gameObject.GetComponent<PathPart>().pathName);
            }
        }
	}
}
