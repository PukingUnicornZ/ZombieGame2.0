using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Enemy : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 60;
    [SerializeField] private int health = 100;
    [SerializeField] float Redness;
    [SerializeField] private GameObject DeathEffect;
    Color c = Color.red;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0)
        {
            DestroyEnemyServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void DestroyEnemyServerRpc()
    {
        GameObject deathEffectInstance = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        UIManager u = FindObjectOfType<UIManager>();
        u.killCount++;
        u.updateKillText();
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
/*
        Redness = 1f / (float)maxHealth * (float)health;
        c.r = Redness;
        gameObject.GetComponent<Renderer>().material.color = c;*/
        if (health <= 0)
        {
            DestroyEnemyServerRpc();
        }
    }
}
