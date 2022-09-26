using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UseAttack : MonoBehaviour
{
    public int ammoAmount = 10;
    public float meleeRepeatDelay = 0.25f;
    private GameObject weaponType;
    public GameObject punchMesh;
    public Text ammoPanel;
    private bool punchActive;
    private int weaponId;

    public GameObject revolver;
    public GameObject rifle;

    void Start()
    {
        weaponType = revolver;
        UpdateText();
        punchMesh.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(weaponType);
        if (Input.GetButtonDown("Fire1")) {
            if (ammoAmount > 0) {
                ammoAmount--;
                UpdateText();
                var clone = Instantiate(weaponType, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(clone, 5.0f);
            } else {
                if (!punchActive) {
                    punchActive = true;
                    StartCoroutine(MeleeAttack());
                }
            }
        }
    }

    void ApplyAmmo(int ammo)
    {
        ammoAmount += ammo;
        UpdateText();
    }

    void SetWeapon(GameObject weapon) {
        weaponType = weapon;
    }

    void UpdateText() {
        if (ammoPanel != null) {
            ammoPanel.text = ammoAmount.ToString();
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
