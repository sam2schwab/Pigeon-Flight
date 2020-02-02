using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendScript : StateMachineBehaviour
{
    private float oldX;
    private float oldY;
    private float newX;
    private float newY;
    public float lerpSpeed;
    private float expectedTimeX;
    private float expectedTimeY;
    private float diffX;
    private float maxDiffX;
    private float diffY;
    private float maxDiffY;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        oldX = animator.GetFloat("X");

        newX = 0f;
        if (animator.GetBool("goingLeft")) newX += -1f;
        if (animator.GetBool("goingRight")) newX += 1f;

        diffX = Mathf.Abs(oldX - newX);
        maxDiffX = 2f;

        expectedTimeX = (diffX / maxDiffX) * lerpSpeed;

        animator.SetFloat("X", Mathf.Lerp(oldX, newX, Time.deltaTime / expectedTimeX));

        oldY = animator.GetFloat("Y");

        newY = 0f;
        if (animator.GetBool("goingForward")) newY += 1f;
        if (animator.GetBool("goingBackward")) newY += -1f;

        diffY = Mathf.Abs(oldY - newY);
        maxDiffY = 2f;

        expectedTimeY = (diffY / maxDiffY) * lerpSpeed;

        animator.SetFloat("Y", Mathf.Lerp(oldY, newY, Time.deltaTime / expectedTimeY));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
