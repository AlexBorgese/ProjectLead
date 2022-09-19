using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public float knockbackTime = 1;
    public float kick = 1.8f;
    private Transform goal;
    private bool hit;
    private ContactPoint contact;
    private float timer; 

    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Player").transform;
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
                agent.SetDestination(goal.position);
            }
        }
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
