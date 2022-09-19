using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndShoot : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public float knockbackTime = 1;
    public float kick = 1.8f;
    private Transform goal;
    private bool hit;
    private ContactPoint contact;
    private float timer; 
	public GameObject projectile;

    public float shootSpeed = 300;
    private bool playerInRange = true;
    private float lastAttackTime = 0f;
    private float fireRate = 0.5f; //how many bullets are fired/second
    private Transform player = null;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //Set timer to the same a knockback in first instance.
        timer = knockbackTime; 
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            //Allow physics to be
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Camera.main.transform.forward * kick, contact.point, ForceMode.Impulse);
            hit = false;
            timer = 0;
            } else {
            timer += Time.deltaTime;
            //After being knocked back, restart movement after X seconds.
            if (knockbackTime < timer)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false; 
                // agent.SetDestination(goal.position);
            }
        }

        if (playerInRange)
        {
            //Rotate the enemy towards the player
            transform.localRotation = Quaternion.LookRotation(player.position - transform.position, transform.up);
            
            if (Time.time - lastAttackTime >= 1f/fireRate)
            {
                shootBullet();
                lastAttackTime = Time.time;
            }
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.tag == "PlayerCapsule")
    //     {
    //         playerInRange = true;
    //         player = other.transform;
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if(other.tag == "player")
    //     {
    //         playerInRange = false;
    //         player = null;
    //     }
    // }

    void shootBullet()
	{
		
		var clone = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
            //Destroy after 2 seconds to stop clutter
        Destroy(clone, 5.0f);
	}
      

    void OnCollisionEnter(Collision other)
    {
 //We compare the tag in the other object to the tag name we set earlier.
        if (other.transform.CompareTag("bullet"))
        {
            contact = other.contacts[0];
            hit = true;
        }
    }
}
