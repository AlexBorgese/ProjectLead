using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    public GameObject particle;

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.contacts[0];
         //Set the exact position and rotation we hit the collider at. 
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point; 
        Instantiate(particle, pos, rot);
        gameObject.SetActive(false);
    }
}
