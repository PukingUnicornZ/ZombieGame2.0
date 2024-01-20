using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class UIManager : MonoBehaviour
{
    public GameObject Selection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
