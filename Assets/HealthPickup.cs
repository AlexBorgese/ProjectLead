using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healthAmount = 10;
    public bool respawn;
    public float delaySpawn = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Player")){
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            other.transform.SendMessage("ApplyHeal", healthAmount);

            if(respawn) {
                Invoke("Respawn", delaySpawn);
            }
        }
    }

    // void Respawn(){
    //     gameObject.GetComponent<MeshRenderer>().enabled = true;
    //     gameObject.GetComponent<Collider>().enabled = true;
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
