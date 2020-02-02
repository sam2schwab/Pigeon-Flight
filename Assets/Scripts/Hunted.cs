using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]

public class Hunted : MonoBehaviour
{
    public float exhaustedSpeed;
    public float normalSpeed;
    public float runningSpeed;

    public float maxExhaustion;
    public float exhaustion;
    public int minBpm;
    public int bpm;
    public int maxBpm;

    public bool isHidden;
    private bool isMoving;

    private Animator anim;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("goingForward") || anim.GetBool("goingLeft") || anim.GetBool("goingRight")) isMoving = true;

        if (anim.GetBool("isExhausted") && isMoving) anim.speed = exhaustedSpeed;
        else if (anim.GetBool("isRunning") && isMoving)
        {
            anim.speed = runningSpeed;
        }
        else anim.speed = normalSpeed;

        bpm = 
    }


    public void UpdateExhaustion()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("")
        {

        }
    }

    public void UpdateBPM()
    {

    }

    private void UpdateStamina()
    {

    }
}
