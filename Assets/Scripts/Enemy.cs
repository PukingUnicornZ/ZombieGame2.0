using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Enemy : NetworkBehaviour
{
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
        Destroy(gameObject);
    }
}
