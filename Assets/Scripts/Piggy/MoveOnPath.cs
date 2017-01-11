using UnityEngine;
using System.Collections;

public class MoveOnPath : MonoBehaviour {
    public string controlPath;
    public Transform character;
    public enum Direction { Forward, Reverse };

    //private float pathPosition = 0f;
    private float pathPercent = 0f;
    private RaycastHit2D hit;
    private float speed = 0.2f;
    private float rayLength = 5;
    private Direction characterDirection;
    private Vector2 floorPosition;
    private float lookAheadAmount = 0.01f;
    private float ySpeed = 0;
    private float gravity = 0.5f;
    private float jumpForce = 0.2f;
    private uint jumpState = 0; //0=grounded 1=jumping

    //void OnDrawGizmos() {
    //    iTween.DrawPath(iTweenPath.GetPath(controlPath), Color.blue);
    //}


    void Start() {
        //plop the character pieces in the "Ignore Raycast" layer so we don't have false raycast data:	
        foreach (Transform child in character) {
            child.gameObject.layer = 2;
        }
    }


    void Update() {
        DetectKeys();
        FindFloorAndRotation();
        MoveCharacter();
        MoveCamera();
    }

    void DetectKeys() {
        //forward path movement:
        if (Input.GetKeyDown(KeyCode.D)) {
            characterDirection = Direction.Forward;
            Debug.Log("Forward");
        }
        if (Input.GetKey(KeyCode.D)) {
            //pathPosition = (pathPosition + Time.deltaTime * speed);
            pathPercent = Mathf.Clamp01(pathPercent + speed * Time.deltaTime);

        }

        //reverse path movement:
        if (Input.GetKeyDown(KeyCode.A)) {
            characterDirection = Direction.Reverse;
            Debug.Log("Backward");
        }
        if (Input.GetKey(KeyCode.A)) {
            //handle path loop around since we can't interpolate a path percentage that's negative(well duh):
            //pathPosition -= (Time.deltaTime * speed);
            pathPercent = Mathf.Clamp01(pathPercent - speed * Time.deltaTime);
        }

        //jump:
        if (Input.GetKeyDown("space") && jumpState == 0) {
            ySpeed -= jumpForce;
            jumpState = 1;
            Debug.Log("Jump");
        }
    }


    void FindFloorAndRotation() {
        //float pathPercent = pathPosition % 1;

        Vector2 coordinateOnPath = iTween.PointOnPath(iTweenPath.GetPath(controlPath), pathPercent);

        //Vector3 lookTarget;

        ////calculate look data if we aren't going to be looking beyond the extents of the path:
        //if (pathPercent - lookAheadAmount >= 0 && pathPercent + lookAheadAmount <= 1) {

        //    //leading or trailing point so we can have something to look at:
        //    if (characterDirection == Direction.Forward) {
        //        lookTarget = iTween.PointOnPath(iTweenPath.GetPath(controlPath), pathPercent + lookAheadAmount);
        //    } else {
        //        lookTarget = iTween.PointOnPath(iTweenPath.GetPath(controlPath), pathPercent - lookAheadAmount);
        //    }

        //    //look:
        //    transform.LookAt(lookTarget);

            //nullify all rotations but y since we just want to look where we are going:
            //float zRot = transform.eulerAngles.z;
            //transform.eulerAngles = new Vector3(0, 0, zRot);
        //}

        // Send a raycast down from the path to see where the floor is
        if (Physics2D.Raycast(coordinateOnPath, Vector2.down, rayLength)) {
            hit = Physics2D.Raycast(coordinateOnPath, Vector2.down, rayLength);
            Debug.DrawRay(coordinateOnPath, Vector2.down * hit.distance);
            floorPosition = hit.point;
            Debug.Log("hit " + hit.transform);
        }
    }


    void MoveCharacter() {
        //add gravity:
        ySpeed += gravity * Time.deltaTime;

        //apply gravity:
        character.position = new Vector2(floorPosition.x, character.position.y - ySpeed);

        //floor checking:
        if (character.position.y < floorPosition.y) {
            ySpeed = 0;
            jumpState = 0;
            character.position = new Vector2(floorPosition.x, floorPosition.y);
            Debug.Log("Floor check");
        }
    }


    void MoveCamera() {
        iTween.MoveUpdate(Camera.main.gameObject, new Vector3(character.position.x, 2.7f, character.position.z - 5f), .9f);
    }
}