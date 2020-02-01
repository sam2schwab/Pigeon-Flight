using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class NetworkedPlayerController : NetworkedBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!IsLocalPlayer)
        {
            GetComponent<MyCharacterController>().enabled = false;
        }
        else
        {
            var camera = GameObject.FindObjectOfType<Camera>().gameObject.AddComponent<MyThirdPersonCamera>() as MyThirdPersonCamera;
            camera.target = transform;
        }
    }
}
