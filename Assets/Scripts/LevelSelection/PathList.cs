using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathList : MonoBehaviour {
    public static LinkedList<PathNode> pathList = new LinkedList<PathNode>();
    public static LinkedListNode<PathNode> currentPath;

    void Start() {
        foreach (Transform child in transform) {
            if (child.gameObject.GetComponent<PathNode>()) {
                pathList.AddFirst(child.GetComponent<PathNode>());
            }
        }
        currentPath = pathList.First;
        Debug.Log("PathList: " + pathList);
        Debug.Log("CurrentPath: " + currentPath);
    }
}
