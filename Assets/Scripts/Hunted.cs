using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]

public class Hunted : MonoBehaviour
{
    public float minSpeed;
    public float normalSpeed;
    public float maxSpeed;

    public float rotateSpeed;

    public float maxStamina;
    public float stamina;
    public int bpm;

    public bool isRunning;
    public bool isHidden;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * rotateSpeed, 0.0f);
    }

    private void UpdateStamina()
    {

    }
}
