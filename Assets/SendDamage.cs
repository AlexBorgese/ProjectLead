using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamage : MonoBehaviour
{
    void OnCollisionStay(Collision other){
        if(other.transform.CompareTag("Player")) {
            other.transform.SendMessage("ApplyDamage", 1);
        }
    }
}
