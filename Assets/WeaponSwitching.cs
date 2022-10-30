using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public List<WeaponType> weaponsList;
    public WeaponType selectedWeaponType;

    public WeaponSwitching(WeaponType startingWeapon)
    {
        selectedWeaponType = startingWeapon;
    }

    private void Awake()
    {
        weaponsList.Add(selectedWeaponType);
    }

    void Update()
    {
        int index = weaponsList.IndexOf(selectedWeaponType);
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            try
            {
                selectedWeaponType = weaponsList[index + 1];
            }
            catch (Exception e)
            {
                selectedWeaponType = weaponsList[0];
            }
        }
    }

    public void AddWeaponToList(WeaponType weapon)
    {
        if (!weaponsList.Contains(weapon))
        {
            weaponsList.Add(weapon);
        }
    }
}
