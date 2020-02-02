using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlumesSpawner : MonoBehaviour
{
    List<Transform> spawnPoints = new List<Transform>();
    public int plumesToSpawn = 10;
    [SerializeField] GameObject plumeTemplate;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }
        plumesToSpawn = Mathf.Min(plumesToSpawn, spawnPoints.Count);

        for (int i = 0; i < plumesToSpawn; i++)
        {
            int index = Random.Range(0, spawnPoints.Count);
            Instantiate(plumeTemplate, spawnPoints[index].transform.position, Quaternion.identity);
            spawnPoints.Remove(spawnPoints[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
