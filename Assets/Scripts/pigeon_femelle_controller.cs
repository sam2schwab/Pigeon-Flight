using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pigeon_femelle_controller : MonoBehaviour
{
    private Animator animator;
    private bool isGrounded;
    private float t;
    public float smootht;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Vertical") <= 0.01f)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            Debug.Log($"isWalking = {animator.GetBool("isWalking")}, isRunning = {animator.GetBool("isRunning")}");

        }
        else if (Input.GetButton("Fire3"))
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            Debug.Log($"isWalking = {animator.GetBool("isWalking")}, isRunning = {animator.GetBool("isRunning")}");
        }
        else
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            Debug.Log($"isWalking = {animator.GetBool("isWalking")}, isRunning = {animator.GetBool("isRunning")}");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Rotate(new Vector3(0.0f, Mathf.Lerp(0f,180f,t), 0.0f));
        }

    }
}
