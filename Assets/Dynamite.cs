using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public GameObject explosionEffect;
    public float delay = 3f;
    private float countdown;
    private bool hasExploded = false;
    public float blastRadius = 5f;
    public float force = 700f;

    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded) 
        {
            Explode();
        }
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        hasExploded = true;
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, blastRadius);
            }

            Destructible destructible = nearbyObject.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.Destroy();
            }
        }
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, blastRadius);
            }
        }
        Destroy(gameObject);
    }
}
