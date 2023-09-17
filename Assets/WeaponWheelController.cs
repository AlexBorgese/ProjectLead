using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    public static bool weaponWheelSelected = false;
    public Image SelectedItem;
    public Sprite noImage;
    public static int weaponID;

    public GameObject playerWeapons;

    private WeaponSwitching weaponSwitcher; 

    void Awake() {
        weaponSwitcher = playerWeapons.GetComponent<WeaponSwitching>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            weaponWheelSelected = !weaponWheelSelected;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (weaponWheelSelected) {
            anim.SetBool("OpenWeaponWheel", true);
        } else {
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch(weaponID) {
            case 0:
                weaponSwitcher.SetWeapon(0);
                Debug.Log("Revolver");
                break;
            case 1:
                weaponSwitcher.SetWeapon(1);
                Debug.Log("Rifle");
                break;
        }

    }
}
