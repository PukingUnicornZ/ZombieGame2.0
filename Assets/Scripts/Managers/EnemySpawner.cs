using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EnemySpawner : NetworkBehaviour
{
    public Transform[] spawnpoints;

    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnTimer;
    [SerializeField] private int enemyLimit;

    [SerializeField] private Wave[] waves;
    [SerializeField] private Wave currentWave;
    public NetworkVariable<int> waveCount;
    private int currentEnemyWaveCount;



    public NetworkVariable<int> enemyAmount;
    public NetworkVariable<int> enemyKillCount;



    private UIManager uIManager;
    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        setWave();
    }
    // Update is called once per frame
    void Update()
    {

        if (IsServer)
        {
            if (IsSpawned)
            {
                // If no enemies Spawn new Wave
                if (waveCount.Value < waves.Length && waves != null && waves.Length > 0 && currentWave != null) {

                    if (currentEnemyWaveCount <= 0 && enemyAmount.Value <= 0)
                    {
                        setWave();
                    }
                    else if (currentEnemyWaveCount > 0)
                    {

                        //Delay between spawns in wave
                        if (spawnTimer > spawnCooldown)
                        {
                            if (enemyAmount.Value < enemyLimit)
                            {
                                spawnTimer = 0;
                                EnemyServerRpc(currentEnemyWaveCount - 1);
                                currentEnemyWaveCount--;
                            }
                        }
                        else
                        {
                            spawnTimer += Time.deltaTime;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    EnemyServerRpc(0);
                }
                if(waveCount.Value > waves.Length - 1)
                {
                    print("You win Rhubarb!");
                }

            }
        }

    }
    void setWave()
    {

        currentWave = waves[waveCount.Value];
        waveCount.Value++;
        print(currentWave.enemies);
        currentEnemyWaveCount = currentWave.enemies.Length;
        if (currentWave.spawnCooldown > 0)
        {
            spawnCooldown = currentWave.spawnCooldown;
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void EnemyServerRpc(int i)
    {
        int r = Random.Range(0, spawnpoints.Length);
        GameObject g = Instantiate(currentWave.enemies[i], spawnpoints[r]);
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
