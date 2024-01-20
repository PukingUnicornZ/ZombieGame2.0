using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun", order = 1)]
public class Gun : ScriptableObject
{
    public string gunName;
    public bool auto;
    public int dmg;
    public float firerate;
    public int maxAmmo;
    public bool shotgun;
    public int shotgunBullets;
    public float spreadFactor;
}
