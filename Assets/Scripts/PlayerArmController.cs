using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmController : MonoBehaviour
{
    [SerializeField] private Transform head;
    void Update()
    {
        if(head != null)
        {
            float hX = head.transform.rotation.eulerAngles.x;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, (-90 + (-hX)));
        }
    }
}
