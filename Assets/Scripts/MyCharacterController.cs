using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class MyCharacterController : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider col;
    public float rotateSpeed = 5;
    public float colPushBack;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") >= 0.1f) animator.SetBool("goingForward", true);
        else animator.SetBool("goingForward", false);

        if (Input.GetButton("Fire3")) animator.SetBool("isRunning", true);
        else animator.SetBool("isRunning", false);

        if (Input.GetAxis("Horizontal") <= -0.1f) animator.SetBool("goingLeft", true);
        else animator.SetBool("goingLeft", false);

        if (Input.GetAxis("Horizontal") >= 0.1f) animator.SetBool("goingRight", true);
        else animator.SetBool("goingRight", false);

        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * rotateSpeed, 0.0f);
    }

    private void OnTriggerStay(Collider collider)
    {
        Debug.Log($"Collision with {collider.gameObject.name}");
        animator.applyRootMotion = false;
        Vector3 pushBack = (transform.position - collider.transform.position) * colPushBack;
        pushBack.y = 0f;
        transform.position += pushBack;
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log($"Collision Exit with {collider.gameObject.name}");
        animator.applyRootMotion = true;
    }

}
