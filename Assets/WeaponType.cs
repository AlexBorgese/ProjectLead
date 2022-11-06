using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponType")]
public class WeaponType : ScriptableObject
{
    public string id;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    public int damage;
    public GameObject prefab;
    public GameObject gunModel;
}
