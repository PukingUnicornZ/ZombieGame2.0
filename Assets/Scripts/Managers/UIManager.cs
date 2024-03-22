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

    [SerializeField] private TextMeshProUGUI WaveTxt;
    [SerializeField] private TextMeshProUGUI AmmoTxt;


    [SerializeField] private TextMeshProUGUI EnemyCountTxt;

    [SerializeField] private EnemySpawner spawner;



    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        updateKillText();
        //select mode
        switch (SceneManager.gameType)
        {
            case 0:
                setIP("83.83.116.240");
                startHost(true);
                break;
            case 1:
                setIP("83.83.116.240");
                startClient(true);
                break;
            case 2:
                setIP("127.0.0.1");
                startHost(false);
                break;
            case 3:
                setIP("127.0.0.1");
                startClient(false);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        FPSTxt.text = "FPS: " + (int)(1f / Time.deltaTime);
    }
    public void updateKillText()
    {
        StartCoroutine(updateTxt());
    }
    public void updateAmmoUI(int cur,int max)
    {
        if (cur == 0)
        {
            AmmoTxt.text =  "<color=\"red\">" + cur + "</color>" + "/" + max;
            AmmoTxt.GetComponent<UIPop>().Pop();
        }
        else
        {
            AmmoTxt.text = cur + "/" + max;
        }
    }
    IEnumerator updateTxt()
    {
        yield return new WaitForSeconds(0.1f);
        if (KillTxt.text != "Kills: " + spawner.enemyKillCount.Value)
        {
            KillTxt.text = "Kills: " + spawner.enemyKillCount.Value;
            KillTxt.GetComponent<UIPop>().Pop();
        }
        EnemyCountTxt.text = "Enemies: " + spawner.enemyAmount.Value;
        if (WaveTxt.text != "Wave: " + spawner.waveCount.Value)
        {
            WaveTxt.text = "Wave: " + spawner.waveCount.Value;
            WaveTxt.GetComponent<UIPop>().Pop();
        }
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
