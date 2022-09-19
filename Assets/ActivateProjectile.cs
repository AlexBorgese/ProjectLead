using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateProjectile : MonoBehaviour
{

    public GameObject projectile; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            var clone = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
            //Destroy after 2 seconds to stop clutter
            Destroy(clone, 5.0f);
        }
    }
}
