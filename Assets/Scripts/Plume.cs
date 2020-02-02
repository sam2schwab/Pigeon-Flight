using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plume : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (0,RotationSpeed*Time.deltaTime,0); //rotates X degrees per second around z axis
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            UpdateScore();
            Destroy(gameObject);
        }       
    }
    
    void UpdateScore()
    {
        // INSERT LAN STUFF HERE!
    }
}
