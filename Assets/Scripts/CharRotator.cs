using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRotator : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + speed,transform.eulerAngles.z);
    }
}
