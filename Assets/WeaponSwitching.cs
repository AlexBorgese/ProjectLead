using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public List<WeaponType> weaponsList = new List<WeaponType>();
    public WeaponType selectedWeaponType;
    public int selectedWeapon = 0;

    public WeaponSwitching(WeaponType startingWeapon)
    {
        selectedWeaponType = startingWeapon;
    }

    private void Awake()
    {
        weaponsList.Add(selectedWeaponType);
    }

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
                selectedWeaponType = weaponsList[0];
            }
            else
            {
                selectedWeapon++;
                Debug.Log("is this the right weapon" + weaponsList[selectedWeapon]);
                selectedWeaponType = weaponsList[selectedWeapon];
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
                selectedWeaponType = weaponsList[selectedWeapon];
            }
            else
            {
                selectedWeapon--;
                selectedWeaponType = weaponsList[selectedWeapon];
            }
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void SetWeapon(int id) {
        Debug.Log("setting weapon to " + id);
        selectedWeapon = id;
        selectedWeaponType = weaponsList[id];
        SelectWeapon();
    }
    
    public void AddWeaponToList(WeaponType weapon)
    {
        weaponsList.Add(weapon);
    }
}
