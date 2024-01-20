using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //input
        //if (!IsOwner) { return; }
        Vector3 move = new Vector3(0, 0, 0);
        if (Input.GetKey("w")) { move.z += 1f; }
        if (Input.GetKey("a")) { move.x += -1f; }
        if (Input.GetKey("s")) { move.z += -1f; }
        if (Input.GetKey("d")) { move.x += 1f; };
        transform.position += move * speed * Time.deltaTime;      
        
        if(transform.position.y < 1)
        {
            transform.position = new Vector3(0, 1, 0);
        }
    }
}
