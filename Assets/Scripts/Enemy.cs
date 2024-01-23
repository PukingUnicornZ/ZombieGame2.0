using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Enemy : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 60;
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject DeathEffect;


    private EnemySpawner spawner;


    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5)
        {
            DestroyEnemyServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void DestroyEnemyServerRpc()
    {
        spawner.enemyAmount.Value--;
        spawner.enemyKillCount.Value++;

        DieClientRpc();
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
    }
    [ClientRpc]
    public void DieClientRpc()
    {
        print("kill");
        GameObject deathEffectInstance = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        UIManager u = FindObjectOfType<UIManager>();
        u.updateKillText();
    }
    
}
