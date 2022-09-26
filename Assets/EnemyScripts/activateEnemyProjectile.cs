using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateEnemyProjectile : MonoBehaviour
{

     public GameObject projectile; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var clone = Instantiate(projectile, gameObject.transform.position, gameObject. transform.rotation);
        //Destroy after 2 seconds to stop
        Destroy(clone, 5.0f);
    }
}
