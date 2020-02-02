using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour
{
    GameObject[] spawns;


    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cat")
        {
            Transform respawn = spawns[0].transform;
            float distance = 0f;
            foreach (var item in spawns)
            {
                float possibleDistance = Mathf.Abs((transform.position - item.transform.position).magnitude);
                if (possibleDistance > distance)
                {
                    distance = possibleDistance;
                    respawn = item.transform;
                }
            }
            transform.position = respawn.position;
        }
    }
}
