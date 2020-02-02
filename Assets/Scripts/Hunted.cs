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
    [Range(0, 1)] public float backToNormal;
    public float rest;
    public float exWhileRunning;
    [SerializeField]
    private float exhaustion;
    public float minBpm;
    public float maxBpm;
    private float deltaBpm;
    [SerializeField]
    private float bpm;
    public AnimationCurve bpmCurve;

    public bool isHidden;
    private bool isMoving;

    private Animator anim;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        exhaustion = 0f;
        deltaBpm = maxBpm - minBpm;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateExhaustion();
        UpdateBPM();
    }


    public void UpdateExhaustion()
    {
        if (anim.GetBool("isExhausted") && (exhaustion / maxExhaustion < backToNormal)) anim.SetBool("isExhausted", false);

        if (anim.GetBool("goingForward") || anim.GetBool("goingLeft") || anim.GetBool("goingRight")) isMoving = true;

        if (anim.GetBool("isExhausted") && isMoving)
        {
            anim.speed = exhaustedSpeed;
            Rest();
        }
        else if (anim.GetBool("isRunning") && isMoving)
        {
            anim.speed = runningSpeed;
            exhaustion += exWhileRunning * Time.deltaTime;

            if (exhaustion > maxExhaustion)
            {
                exhaustion = maxExhaustion;
                anim.SetBool("isExhausted", true);
                anim.Play("ExhaustedAnim", 0);
            }
        }
        else
        {
            anim.speed = normalSpeed;
            Rest();
        }
    }

    public void UpdateBPM()
    {
        float larpPos = exhaustion / maxExhaustion;
        bpm = (bpmCurve.Evaluate(larpPos) * deltaBpm + minBpm) > minBpm ? (bpmCurve.Evaluate(larpPos) * deltaBpm + minBpm) : minBpm;
    }

    public void Rest()
    {
        exhaustion -= rest * Time.deltaTime;

        if (exhaustion < 0f)
        {
            exhaustion = 0f;
        }
    }
}
