using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EnemySpawner : NetworkBehaviour
{
    public Transform[] spawnpoints;
    public GameObject Enemy;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EnemyServerRpc();
        }

    }
    [ServerRpc(RequireOwnership = false)]
    public void EnemyServerRpc()
    {
        int r = Random.Range(0, spawnpoints.Length);
        GameObject g = Instantiate(Enemy, spawnpoints[r]);
        g.GetComponent<NetworkObject>().Spawn(true);
    }
    [ClientRpc(RequireOwnership = false)]
    public void EnemyClientRpc()
    {

    }
}
