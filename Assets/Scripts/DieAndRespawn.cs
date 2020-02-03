using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class DieAndRespawn : NetworkedBehaviour
{
    GameObject[] spawns;

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!IsLocalPlayer || !collision.gameObject.CompareTag("Cat")) return;
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
        InvokeServerRpc(LoseLife);
    }

    [ServerRPC]
    public void LoseLife()
    {
        GameManager.Instance.LoseLife();
    }
}
