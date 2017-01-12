using UnityEngine;
using System.Collections;

public class MoveOnPath : MonoBehaviour {
    public GameObject currentPath;
    //public Transform character;
    public enum Direction { Forward, Reverse };

    //private float pathPosition = 0f;
    private float pathPercent = 0f;
    private RaycastHit2D hit;
    private string controlPath;
    private float speed = 0.2f;
    private float rayLength = 5;
    private Direction characterDirection;
    private Vector2 floorPosition;
    private float lookAheadAmount = 0.01f;
    private float ySpeed = 0;
    private float gravity = 0.5f;
    private float jumpForce = 0.19f;
    //private uint jumpState = 0; //0=grounded 1=jumping

    //void OnDrawGizmos() {
    //    iTween.DrawPath(iTweenPath.GetPath(controlPath), Color.blue);
    //}


    void Start() {
        //plop the character pieces in the "Ignore Raycast" layer so we don't have false raycast data:	
        foreach (Transform child in transform) {
            child.gameObject.layer = 2;
        }
        if (!PathDictionary.paths.TryGetValue(currentPath, out controlPath)) {
            Debug.LogError("Could not find path " + currentPath.name);
        }
    }


    void Update() {
        DetectKeys();
        FindFloorAndRotation();
        MoveCharacter();
        MoveCamera();
    }

    void DetectKeys() {

        if (PigControlInput.piggyAnimator.GetBool(ConstantValues.piggyAnimatorParameterNames.forward)) {
            characterDirection = Direction.Forward;
            pathPercent = Mathf.Clamp01(pathPercent + speed * Time.deltaTime);
            Debug.Log("Forward");
        }

        if (PigControlInput.piggyAnimator.GetBool(ConstantValues.piggyAnimatorParameterNames.backward)) {
            characterDirection = Direction.Reverse;
            pathPercent = Mathf.Clamp01(pathPercent - speed * Time.deltaTime);
            Debug.Log("Backward");
        }

        if (GetComponentInChildren<PigControlInput>().jump) {
            ySpeed -= jumpForce;
            GetComponentInChildren<PigControlInput>().StartJumpAnimation();
            GetComponentInChildren<PigControlInput>().jump = false;
            Debug.Log("Jump");
        }

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
        if (Input.GetKeyDown("space")) {
            ySpeed -= jumpForce;
            //jumpState = 1;
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
            if (!hit.transform.gameObject.Equals(currentPath)) {
                currentPath = hit.transform.gameObject;
                if (!PathDictionary.paths.TryGetValue(currentPath, out controlPath)) {
                    Debug.LogError("Could not find path " + currentPath.name);
                }
                Debug.Log("Switched paths.");
            }
            Debug.DrawRay(coordinateOnPath, Vector2.down * hit.distance);
            floorPosition = hit.point;
            Debug.Log("hit " + hit.transform);
        }
    }


    void MoveCharacter() {
        //add gravity:
        ySpeed += gravity * Time.deltaTime;

        //apply gravity:
        transform.position = new Vector2(floorPosition.x, transform.position.y - ySpeed);

        //floor checking:
        if (transform.position.y < floorPosition.y) {
            ySpeed = 0;
            //jumpState = 0;
            transform.position = new Vector2(floorPosition.x, floorPosition.y);

            if (PigControlInput.piggyAnimator.GetBool(ConstantValues.piggyAnimatorParameterNames.jump)) {
                PigControlInput.piggyAnimator.SetBool(ConstantValues.piggyAnimatorParameterNames.jump, false);
                GetComponentInChildren<PigControlInput>().ChangeButtonStatusAll(true);
            }
        }
    }

    void MoveCamera() {
        iTween.MoveUpdate(Camera.main.gameObject, new Vector3(transform.position.x, 2.7f, transform.position.z - 5f), .9f);
    }
}