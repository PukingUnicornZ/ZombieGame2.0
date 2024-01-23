using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UTP;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject Selection;
    [SerializeField] private TextMeshProUGUI KillTxt;
    [SerializeField] private TextMeshProUGUI FPSTxt;


    [SerializeField] private TextMeshProUGUI EnemyCountTxt;

    [SerializeField] private EnemySpawner spawner;



    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        updateKillText();
    }

    // Update is called once per frame
    void Update()
    {
        FPSTxt.text = "FPS: " + (int)(1f / Time.deltaTime);
    }
    public void updateKillText()
    {
        KillTxt.text = "Kills: " + spawner.enemyKillCount.Value;
        EnemyCountTxt.text = "Enemies: " + spawner.enemyAmount.Value;
    }



    //Server connections
    public void startClient(bool overrideIP)
    {
        Selection.SetActive(false);
        NetworkManager.Singleton.StartClient();

        if (overrideIP) NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = "192.168.178.15";
        else NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = "";
    }

    public void setIP(string ip)
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ip;
    }

    public void startHost(bool overrideIP)
    {
        Selection.SetActive(false);
        NetworkManager.Singleton.StartHost();

        if (overrideIP) NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = "192.168.178.15";
        else NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = "";
    }
}
