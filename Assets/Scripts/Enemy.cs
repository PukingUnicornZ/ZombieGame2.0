using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Enemy : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 60;
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject DeathEffect;
    [SerializeField] private GameObject DmgPopup;
    [SerializeField] private Transform effectSpawnLocation;

    private bool dead;

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
        Transform t = effectSpawnLocation;
        DmgPopup popup = Instantiate(DmgPopup, new Vector3(t.position.x, t.position.y,t.position.z), Quaternion.identity).GetComponent<DmgPopup>();
        popup.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180,transform.eulerAngles.z);
        popup.setValue(value);
        if (health <= 0 && dead == false)
        {

            dead = true;
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
