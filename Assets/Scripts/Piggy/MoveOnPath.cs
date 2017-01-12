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

        // Put current path on the path layer so raycasts will detect it
        //if (PathDictionary.paths.TryGetValue(currentPath, out controlPath)) {
        //    currentPath.layer = PathDictionary.pathLayer; // Default layer. Will be detected by raycasts while all other paths are ignored
        //} else {
        //    Debug.LogError("Could not find path " + currentPath.name);
        //}
    }


    void Update() {
        DetectInput();
        FindFloorAndRotation();
        MoveCharacter();
        MoveCamera();
    }

    void GoToNextLevel() {
        // Deactivate user controls and switch to path around curve
        GetComponentInChildren<PigControlInput>().ChangeButtonStatusAll(false);
        controlPath = PathList.currentPath.Next.Value.pathName;
        Debug.Log("controlPath: " + controlPath);
    }

    void DetectInput() {
        if (PigControlInput.piggyAnimator.GetBool(ConstantValues.piggyAnimatorParameterNames.forward)) {
            characterDirection = Direction.Forward;
            pathPercent = Mathf.Clamp01(pathPercent + speed * Time.deltaTime);
            Debug.Log("Forward");
            if (pathPercent == 1) {
                Debug.Log("End of path");
                GoToNextLevel();
            }
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
            PathDictionary.DetectPaths();
            Debug.Log("Jump");
        }

#region Keyboard input
#if UNITY_EDITOR // Same thing PigControlButtons.cs does
        if (Input.GetKeyDown(KeyCode.D)) {
            GetComponentInChildren<PigControlInput>().MoveForward();
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            GetComponentInChildren<PigControlInput>().MoveBackward();
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            GetComponentInChildren<PigControlInput>().StopForward();
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            GetComponentInChildren<PigControlInput>().StopBackward();
        }
        if (!GetComponentInChildren<PigControlInput>().Jumping && Input.GetKeyUp(KeyCode.K)) {
            GetComponentInChildren<PigControlInput>().Kick();
        }
        if (!GetComponentInChildren<PigControlInput>().Jumping && Input.GetKeyDown(KeyCode.L)) {
            GetComponentInChildren<PigControlInput>().Jump();
        }
#endif
#endregion

        //#region Keyboard input
        ////forward path movement:
        //if (Input.GetKeyDown(KeyCode.D)) {
        //    characterDirection = Direction.Forward;
        //    Debug.Log("Forward");
        //}
        //if (Input.GetKey(KeyCode.D)) {
        //    pathPercent = Mathf.Clamp01(pathPercent + speed * Time.deltaTime);

        //    //pathPosition += (Time.deltaTime * speed);
        //    //float temp = pathPosition + (Time.deltaTime * speed);
        //    //if (temp > 1) {
        //    //    pathPosition = 1;
        //    // Put on new path
        //    //} else {
        //    //pathPosition -= (Time.deltaTime * speed);
        //    //}
        //}

        ////reverse path movement:
        //if (Input.GetKeyDown(KeyCode.A)) {
        //    characterDirection = Direction.Reverse;
        //    Debug.Log("Backward");
        //}
        //if (Input.GetKey(KeyCode.A)) {
        //    pathPercent = Mathf.Clamp01(pathPercent - speed * Time.deltaTime);
        //}

        ////jump:
        //if (Input.GetKeyDown("space")) {
        //    ySpeed -= jumpForce;
        //    //jumpState = 1;
        //    Debug.Log("Jump");
        //}
        //#endregion
    }


    void FindFloorAndRotation() {
        //float pathPercent = pathPosition % 1;

        Vector2 coordinateOnPath = iTween.PointOnPath(iTweenPath.GetPath(controlPath), pathPercent);

        #region Rotate to look ahead
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
        #endregion

        // Send a raycast down from the path to see where the floor is
        hit = Physics2D.Raycast(coordinateOnPath, Vector2.down, rayLength);
        if (hit) {
            //if (!hit.transform.gameObject.Equals(currentPath)) {
            //    currentPath = hit.transform.gameObject;
            //    if (!PathDictionary.paths.TryGetValue(currentPath, out controlPath)) {
            //        Debug.LogError("Could not find path " + currentPath.name);
            //    }
            //    Debug.Log("Switched paths.");
            //}

            Debug.DrawRay(coordinateOnPath, Vector2.down * hit.distance);
            floorPosition = hit.point;
            Debug.Log("hit " + hit.transform);
        }
    }


    void MoveCharacter() {
        if (!InLevelSettings.paused) {
            //add gravity:
            ySpeed += gravity * Time.deltaTime;

            //apply gravity:
            transform.position = new Vector2(floorPosition.x, transform.position.y - ySpeed);

            //floor checking:
            if (transform.position.y < floorPosition.y) {
                ySpeed = 0;
                //jumpState = 0;
                transform.position = new Vector2(floorPosition.x, floorPosition.y);
                Land();
            }
        }
    }

    void Land() {
        if (PigControlInput.piggyAnimator.GetBool(ConstantValues.piggyAnimatorParameterNames.jump)) {
            PigControlInput.piggyAnimator.SetBool(ConstantValues.piggyAnimatorParameterNames.jump, false);
            GetComponentInChildren<PigControlInput>().ChangeButtonStatusAll(true);
            PathDictionary.HidePaths(currentPath);
        }
    }

    void MoveCamera() {
        iTween.MoveUpdate(Camera.main.gameObject, new Vector3(transform.position.x, 2.7f, transform.position.z - 5f), .9f);
    }
}