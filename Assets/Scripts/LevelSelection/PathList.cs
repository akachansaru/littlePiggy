using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathList : MonoBehaviour {
    public static LinkedList<string> pathList = new LinkedList<string>();
    public static LinkedListNode<string> currentPathNode;

    void Start() {
        AssemblePath();
        currentPathNode = pathList.First;
        Debug.Log("Starting path: " + currentPathNode.Value);
    }

    void AssemblePath() {
        //foreach (Transform child in transform) {
        //    if (child.gameObject.GetComponent<PathNode>()) {
        //        pathList.AddFirst(child.GetComponent<PathNode>());
        //    }
        //}
        iTweenPath[] paths = GetComponentsInChildren<iTweenPath>();
        foreach (iTweenPath path in paths) {
            Debug.Log("PathList: " + path.pathName);
            pathList.AddLast(path.pathName);
        }
    }
}
