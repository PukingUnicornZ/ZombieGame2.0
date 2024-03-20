using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 2)]
public class Wave : ScriptableObject
{
    public GameObject[] enemies;
    public float spawnCooldown = -1;
    public string waveType;
}
