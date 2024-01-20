using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject Selection;
    public TextMeshProUGUI KillTxt;
    public TextMeshProUGUI FPSTxt;
    public int killCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FPSTxt.text = "FPS: " + (1f / Time.deltaTime);
    }
    public void updateKillText()
    {
        KillTxt.text = "Kills: " + killCount;
    }
    public void startClient()
    {
        Selection.SetActive(false);
        NetworkManager.Singleton.StartClient();
    }
    public void startHost()
    {
        Selection.SetActive(false);
        NetworkManager.Singleton.StartHost();
    }
}
