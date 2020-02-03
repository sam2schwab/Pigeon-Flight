using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;


public class Plume : NetworkedBehaviour
{
    [SerializeField] float RotationSpeed = 10.0f;
    // Start is called before the first frame update

    [SerializeField] AudioClip plumeClip;
    GameObject UIManager;

    void Start()
    {
        UIManager = GameObject.FindGameObjectWithTag("UiManager");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0); //rotates X degrees per second around z axis
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(plumeClip);
            Debug.Log("player interacts");
            Debug.Log("player is" + (other.GetComponent<NetworkedBehaviour>().IsLocalPlayer ? " local" : "remote"));
            InvokeServerRpc(CollectPlume);
        }       
    }

    [ServerRPC(RequireOwnership = false)]
    private void CollectPlume()
    {
        GameManager.Instance.GainFeather();
        Destroy(gameObject);
    }
}
