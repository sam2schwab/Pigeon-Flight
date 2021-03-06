using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MLAPI;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Animator))]

public class MyCharacterController : NetworkedBehaviour
{
    private Animator animator;
    private CapsuleCollider col;
    private MyThirdPersonCamera _camera;
    public float rotateSpeed = 5;
    public float colPushBack;

    // Start is called before the first frame update
    void Start()
    {
        if (NetworkingManager.Singleton != null && !IsOwner) { 
            enabled = false;
            return;
        }
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        Assert.IsNotNull(Camera.main);
        _camera = Camera.main.gameObject.AddComponent<MyThirdPersonCamera>();
        _camera.target = transform;
        _camera.enableHorizontalRotation = false;
        _camera.enableZoom = false;
        _camera.horizontalAngle += 180;
        _camera.height = 1.9f;

        switch (GetComponent<NetworkedObject>().PrefabHashGenerator)
        {
            case "Hunted":
                break;
            case "Hunter":
                Camera.main.cullingMask &= ~(1 << 8);
                ChangeLayerRecursively(gameObject, 8);
                _camera.minDistance = _camera.maxDistance = _camera.distance = 0.1f;
                break;
            default:
                throw new InvalidDataException();
        }
    }

    private static void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, layer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) animator.SetBool("goingForward", true);
        else animator.SetBool("goingForward", false);

        if(Input.GetKey("s")) animator.SetBool("goingBackward", true);
        else animator.SetBool("goingBackward", false);

        if (Input.GetKey("a")) animator.SetBool("goingLeft", true);
        else animator.SetBool("goingLeft", false);

        if (Input.GetKey("d")) animator.SetBool("goingRight", true);
        else animator.SetBool("goingRight", false);

        if (Input.GetButton("Fire3")) animator.SetBool("isRunning", true);
        else animator.SetBool("isRunning", false);

        if(animator.GetBool("goingForward") || animator.GetBool("goingBackward") || animator.GetBool("goingLeft") || animator.GetBool("goingRight"))
        {
            animator.SetBool("isMoving", true);
        } else animator.SetBool("isMoving", false);

        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * rotateSpeed, 0.0f);
        _camera.RotateCamera(0f, Input.GetAxis("Mouse X") * rotateSpeed);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (NetworkingManager.Singleton != null && !IsOwner) { 
            return;
        }
        Debug.Log($"Collision with {collision.gameObject.name}");
        animator.applyRootMotion = false;
        Vector3 pushBack = collision.contacts.Select(x => x.normal)
            .Aggregate(Vector3.zero, (sum, normal) => sum + normal).normalized;
        pushBack = pushBack.normalized * colPushBack; 
        pushBack.y = 0f;
        transform.position += pushBack;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (NetworkingManager.Singleton != null && !IsOwner) { 
            return;
        }
        Debug.Log($"Collision Exit with {collision.gameObject.name}");
        animator.applyRootMotion = true;
    }


//    public void RecenterCamera()
//    {
//        Debug.Log("RECENTERING!!!!!!!!!");
//        _camera.horizontalAngle += transform.rotation.eulerAngles.y - _camera.transform.rotation.eulerAngles.y;
//    }
}
