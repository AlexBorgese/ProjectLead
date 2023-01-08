using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteThrower : MonoBehaviour
{
    // Start is called before the first frame update
    public float throwForce = 20f;
    public GameObject dynamitePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        GameObject dynamite = Instantiate(dynamitePrefab, transform.position, transform.rotation);
        Rigidbody rb = dynamite.GetComponent<Rigidbody>();
        
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
