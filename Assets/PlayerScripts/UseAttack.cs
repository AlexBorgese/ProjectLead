using System;
using System.Collections;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UseAttack : MonoBehaviour
{
    private bool isReloading = false;
    public float meleeRepeatDelay = 0.25f;
    private WeaponType weaponType;
    public GameObject punchMesh;
    public Text ammoPanel;
    private bool punchActive;
    private int weaponId;
    private WeaponSwitching weaponSwitching;
    public Animator animator;
    public ParticleSystem muzzelFlash;

    private void Awake()
    {
        weaponSwitching = gameObject.GetComponent<WeaponSwitching>();
    }

    void Start()
    {
        weaponType = weaponSwitching.selectedWeaponType;

        if (weaponType.currentAmmo == -1)
        {
            weaponType.currentAmmo = weaponType.maxAmmo;
        }
        UpdateText();
        punchMesh.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        weaponType = weaponSwitching.selectedWeaponType;
        if (isReloading) return;
        if (weaponType.currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButtonDown("Fire1")) {
            muzzelFlash.Play();
            if (weaponType.currentAmmo > 0) {
                weaponType.currentAmmo--;
                UpdateText();
                // change the below to a generic projectile?
                var clone = Instantiate(weaponType.prefab, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(clone, 5.0f);
            } else {
                if (!punchActive) {
                    punchActive = true;
                    StartCoroutine(MeleeAttack());
                }
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(weaponType.reloadTime);
        animator.SetBool("Reloading", false);
        weaponType.currentAmmo = weaponType.maxAmmo;
        isReloading = false;
        UpdateText();
    }

    void ApplyAmmo(int ammo)
    {
        weaponType.currentAmmo += ammo;
        UpdateText();
    }

    void SetWeapon(WeaponType weapon) {
        weaponSwitching.AddWeaponToList(weapon);
   }

    void UpdateText() {
        if (ammoPanel != null) {
            ammoPanel.text = weaponType.currentAmmo.ToString();
        }
    }

    IEnumerator MeleeAttack() {
        punchMesh.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        punchMesh.SetActive(false);
        yield return new WaitForSeconds(meleeRepeatDelay);
        punchActive = false;
        yield return null;
    }
}
