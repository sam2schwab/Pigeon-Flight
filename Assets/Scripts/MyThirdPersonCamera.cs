using UnityEngine;

public class MyThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public bool useDefaultControls = true;
    public bool autoLockCursor;
    public bool followTarget;

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
    
    private Transform swivel;


    private void Start() {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        swivel = new GameObject("CameraSwivel").transform;
        swivel.position = target.position;
        swivel.rotation = Quaternion.Euler(0, target.rotation.eulerAngles.y, 0);
        //swivel.hideFlags = HideFlags.HideInHierarchy;

        Cursor.lockState = (autoLockCursor)?CursorLockMode.Locked:CursorLockMode.None;

        if (followTarget) this.transform.parent = swivel; //Melchi

    }

    private void LateUpdate()
    {
        if (!followTarget)
        {
            var targetPosition = target.position;
            //follow target
            swivel.position = targetPosition;

            if (useDefaultControls)
            {
                //rotate swivel
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    RotateCamera(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
                }

                //manage cursor lock
                if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
                {
                    Cursor.lockState = CursorLockMode.None;
                }

                ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
            }

            //update position accordingly
            transform.position = targetPosition + swivel.forward * distance;
            transform.LookAt(target.transform);
        }

        //Melchi
        if (followTarget)
        {
            Vector3 swivelPosition = new Vector3(target.position.x, 0f, target.position.z);
            swivel.position = swivelPosition;

            Vector3 swivelRot = new Vector3();
            verticalAngle = Mathf.Lerp(verticalAngle, verticalAngle - Input.GetAxis("Mouse Y") * rotateSpeed, Time.deltaTime * smoothRotation);
            //verticalAngle -= Input.GetAxis("Mouse Y") * rotateSpeed;
            verticalAngle = Clamp180(verticalAngle);
            verticalAngle = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);
            swivelRot.x = verticalAngle;
            swivelRot.y = target.rotation.eulerAngles.y;
            swivelRot.z = 0f;
            swivel.rotation = Quaternion.Euler(swivelRot);//Quaternion.Slerp(swivel.rotation, Quaternion.Euler(swivelRot),Time.deltaTime * smoothRotation);


            ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
            transform.localPosition = Vector3.back * distance;
        }

    }

    public void ZoomCamera(float zoomDelta)
    {
        distance -= zoomDelta * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    public void RotateCamera(float verticalDelta, float horizontalDelta)
    {
        verticalAngle -= verticalDelta * rotateSpeed;
        verticalAngle = Clamp180(verticalAngle);
        verticalAngle = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);
        horizontalAngle += horizontalDelta * rotateSpeed;
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
