using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDictionary : MonoBehaviour {
    public static Dictionary<GameObject, string> paths = new Dictionary<GameObject, string>();
    public static int pathLayer = 13;

    void Start() {
        foreach (Transform child in transform) {
            if (child.gameObject.GetComponent<PathPart>()) {
                paths.Add(child.gameObject,
                    child.gameObject.GetComponent<PathPart>().pathName);
                child.gameObject.layer = 2; // Ignore raycast layer
            }
        }
    }

    public static void DetectPaths() {
        foreach (GameObject path in paths.Keys) {
            path.layer = pathLayer; // Paths layer
        }
    }

    /// <summary>
    /// Puts all paths except the current path into layer 2 so they will not be detected by raycasts
    /// </summary>
    /// <param name="currentPath"></param>
    public static void HidePaths(GameObject currentPath) {
        foreach (GameObject path in paths.Keys) {
            if (!path.Equals(currentPath)) {
                path.layer = 2; // Ignore raycast layer
            }
        }
    }
}
