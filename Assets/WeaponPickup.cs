using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public GameObject weaponType;
    public bool respawn;
    public float delaySpawn = 30;


    void OnCollisionEnter(Collision other) {
        if (other.transform.CompareTag("Player")) {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            other.transform.BroadcastMessage("SetWeapon", weaponType);

            if(respawn){
                Invoke("Respawn", delaySpawn);
            }
        }    
    }

    void Respawn() {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
