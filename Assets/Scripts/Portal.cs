using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float cooldown = 0f;
    [SerializeField] float maxCooldown = 5.0f;
    [SerializeField] GameObject otherPortal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (cooldown < 0.1f)
        {
            otherPortal.GetComponent<Portal>().cooldown = maxCooldown;
            other.gameObject.transform.position = otherPortal.transform.position;
            //other.gameObject.transform.rotation = otherPortal.transform.rotation;
        }
    }
}
