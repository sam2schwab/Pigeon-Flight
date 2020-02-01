using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class MyCharacterController : NetworkedBehaviour
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
        if (!IsLocalPlayer) return;
        if (Input.GetAxis("Vertical") <= 0.01f)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            //Debug.Log($"isWalking = {animator.GetBool("isWalking")}, isRunning = {animator.GetBool("isRunning")}");

        }
        else if (Input.GetButton("Fire3"))
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            //Debug.Log($"isWalking = {animator.GetBool("isWalking")}, isRunning = {animator.GetBool("isRunning")}");
        }
        else
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            //Debug.Log($"isWalking = {animator.GetBool("isWalking")}, isRunning = {animator.GetBool("isRunning")}");
        }
    
        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * rotateSpeed, 0.0f);



    }
}
