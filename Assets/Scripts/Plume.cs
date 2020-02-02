using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;


public class Plume : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 10.0f;
    // Start is called before the first frame update

    GameObject UIManager;

    void Start()
    {
        UIManager = GameObject.FindGameObjectWithTag("UiManager");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (0,RotationSpeed*Time.deltaTime,0); //rotates X degrees per second around z axis
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<NetworkedObject>().IsLocalPlayer)
        {
            UpdateScore();
            Destroy(gameObject);
        }       
    }
    
    void UpdateScore()
    {
        UIManager.GetComponent<UIManagement>().UpdatePlumeText(1);
        // INSERT LAN STUFF HERE!
    }
}
