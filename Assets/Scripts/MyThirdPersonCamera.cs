using UnityEngine;

public class MyThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    [Header("Controls")]
    [Tooltip("When useDefaultControls is true, use this section to fine tune the controls")]
    public bool useDefaultControls = true;

    public bool enableHorizontalRotation = true;
    public bool enableVerticalRotation = true;
    public bool enableZoom = true;
    public bool enableCursorLock = true;
    public bool autoLockCursor;

    [Header("Rotation")] 
    public float verticalAngle;
    public float horizontalAngle;
    public float rotateSpeed = 5;
    public float smoothRotation = 12f;
    public float maxVerticalAngle = 70;
    public float minVerticalAngle = -10;
    
    [Header("Distance")]
    public float distance = 5;
    public float zoomSpeed = 5;
    public float minDistance = 3;
    public float maxDistance = 10;
    public float height = 3;
    
    private Transform swivel;


    private void Start() {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        swivel = new GameObject("CameraSwivel").transform;
        swivel.position = target.position;
        swivel.rotation = Quaternion.Euler(0, target.rotation.eulerAngles.y, 0);
        swivel.hideFlags = HideFlags.HideInHierarchy;

        Cursor.lockState = (autoLockCursor)?CursorLockMode.Locked:CursorLockMode.None;

    }

    private void LateUpdate()
    {
        var targetPosition = target.position + height * Vector3.up;
        //follow target
        swivel.position = targetPosition;

        if (useDefaultControls)
        {
            //rotate swivel
            if (enableCursorLock && Cursor.lockState == CursorLockMode.Locked)
            {
                RotateCamera(
                    enableVerticalRotation ? Input.GetAxis("Mouse Y")  * rotateSpeed : 0f,
                    enableHorizontalRotation ? Input.GetAxis("Mouse X") * rotateSpeed : 0f);
            }

            //manage cursor lock
            if (enableCursorLock)
            {
                if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }

            if (enableZoom)
            {
                ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
            }
        }

        //update position accordingly
        transform.position = targetPosition + swivel.forward * distance;
        transform.LookAt(targetPosition);
    }

    public void ZoomCamera(float zoomDelta)
    {
        distance -= zoomDelta * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    
    public void RotateCamera(float verticalDelta, float horizontalDelta)
    {
        verticalAngle -= verticalDelta;
        verticalAngle = Clamp180(verticalAngle);
        verticalAngle = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);
        horizontalAngle += horizontalDelta;
        swivel.rotation = Quaternion.Slerp(swivel.rotation, Quaternion.Euler(-verticalAngle, horizontalAngle, 0),
            Time.deltaTime * smoothRotation);
    }

    /// <summary>
    /// clamps an euler angle between -180 and 180
    /// </summary>
    /// <param name="eulerAngles"></param>
    /// <returns></returns>
    private static float Clamp180(float eulerAngles)
    {
        eulerAngles = eulerAngles + 180;
        var result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0)
        {
            result += 360f;
        }
        return result - 180;
    }
}
