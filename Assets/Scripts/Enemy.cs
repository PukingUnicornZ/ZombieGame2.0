using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Enemy : NetworkBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Material damagedColour;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Die()
    {
        DestroyEnemyServerRpc();
    }
    [ServerRpc(RequireOwnership = false)]
    public void DestroyEnemyServerRpc()
    {
        Destroy(gameObject);
    }
    [ServerRpc(RequireOwnership = false)]
    public void DamageServerRpc(int value)
    {
        DamageClientRpc(value);
    }
    [ClientRpc]
    public void DamageClientRpc(int value)
    {
        health -= value;
        if (health <= 0)
        {
            DestroyEnemyServerRpc();
        }
        if (health <= 50)
        {
            gameObject.GetComponent<Renderer>().material = damagedColour;
        }
    }
}
