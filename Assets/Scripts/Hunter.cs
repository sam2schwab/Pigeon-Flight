using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]

public class Hunter : MonoBehaviour
{
    public float speed;
    private bool isMoving;
    private bool isListening;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
