using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class MyCharacterController : MonoBehaviour
{
    private Animator animator;
    public float rotateSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
}
