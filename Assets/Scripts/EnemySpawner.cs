using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EnemySpawner : NetworkBehaviour
{
    public Transform[] spawnpoints;
    public GameObject Enemy;

    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnTimer;
    [SerializeField] private int enemyLimit;

    public NetworkVariable<int> enemyAmount;
    public NetworkVariable<int> enemyKillCount;

    [SerializeField] private int enemyDEBUG;
    [SerializeField] private int enemyDEBUG2;


    private UIManager uIManager;
    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
    }
    // Update is called once per frame
    void Update()
    {
        enemyDEBUG = enemyAmount.Value;
        enemyDEBUG2 = enemyKillCount.Value;
        if (IsSpawned)
        {

            if (spawnTimer > spawnCooldown)
            {
                if (enemyAmount.Value < enemyLimit)
                {
                    spawnTimer = 0;
                    EnemyServerRpc();
                }
            }
            else
            {
                spawnTimer += Time.deltaTime;
            }



            if (Input.GetKeyDown(KeyCode.P))
            {
                EnemyServerRpc();
            }
        }

    }
    [ServerRpc(RequireOwnership = false)]
    public void EnemyServerRpc()
    {
        int r = Random.Range(0, spawnpoints.Length);
        GameObject g = Instantiate(Enemy, spawnpoints[r]);
        g.GetComponent<NetworkObject>().Spawn(true);


        enemyAmount.Value++;


        EnemyClientRpc();

    }
    [ClientRpc(RequireOwnership = false)]
    public void EnemyClientRpc()
    {
        uIManager.updateKillText();
    }
}
