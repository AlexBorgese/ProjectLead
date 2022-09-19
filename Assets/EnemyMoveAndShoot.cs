using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveAndShoot : MonoBehaviour
{
    public bool isMelee = false;
    private NavMeshAgent agent;
    public float knockbackTime = 1;
    public float kick = 1.8f;
    private Transform goal;
    private bool hit;
    private ContactPoint contact;
    private float timer; 
	public GameObject weapon;
    public int Health = 5;

    public float shootSpeed = 10;
    private bool playerInAttackRange = false;
    private bool playerInSightRange = false;
    private Transform player = null;
    public LayerMask whatIsGround, whatIsPlayer;

    // patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // attacking
    private float fireRate = 1f; //how many bullets are fired/second
    private bool alreadyAttacked = false;
    public float sightRange, attackRange;


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
        if (hit) {
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

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patrolling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
        


        // if (playerInAttackRange){
        //     playerInSightRange = false; 
        //     //Rotate the enemy towards the player
        //     transform.localRotation = Quaternion.LookRotation(player.position - transform.position, transform.up);
            
        //     if (Time.time - lastAttackTime >= 1f/fireRate)
        //     {
        //         shootBullet();
        //         lastAttackTime = Time.time;
        //     }
        // }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInSightRange = true; 
            player = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInAttackRange = false;
            playerInSightRange = false;
            player = null;
        }
    }

    void shootBullet()
	{
		
		var clone = Instantiate(weapon, gameObject.transform.position, gameObject. transform.rotation);

	}
      

    void OnCollisionEnter(Collision other)
    {
 //We compare the tag in the other object to the tag name we set earlier.
        if (other.transform.CompareTag("bullet"))
        {
            contact = other.contacts[0];
            hit = true;
            Health = Health - 1;
    Debug.Log(Health);
            if(Health == 0) {
                Destroy(gameObject);
            }

        }
    }

    private void Patrolling() {
        Debug.Log("Patrolling");
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet) {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // check if on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void ChasePlayer() {
        Debug.Log("Chase");
        agent.SetDestination(player.position);
    }

    private void AttackPlayer() {
        Debug.Log("AttackS");
        if(isMelee) {
            agent.SetDestination(player.position);
        } else {
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            if(!alreadyAttacked) {
                shootBullet();
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), fireRate);
            } 
        }
    }

    private void ResetAttack() {
        alreadyAttacked = false;
    }
}
