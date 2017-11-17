using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{

    public float inReachDistance;

    public float distanceBetween;

    private Transform theZombie;
    private Transform targetToReach;

    private NavMeshAgent navComponent;

    public float walkingSpeedZombie;
    public float runningSpeedZombie;

    private Rigidbody theRigidbodyZombie;

    void Start()
    {
        theRigidbodyZombie = GetComponent<Rigidbody>();

        targetToReach = GameObject.Find("Player").transform;
        theZombie = this.transform;

        navComponent = this.gameObject.GetComponent<NavMeshAgent>();

        navComponent.speed = walkingSpeedZombie;
    }

    void Update()
    {
        theRigidbodyZombie.angularVelocity = new Vector3(0, 0, 0);
        float theDistance = Vector3.Distance(targetToReach.position, transform.position);

        if (targetToReach)
        {
            navComponent.SetDestination(targetToReach.position);
        }
        else
        {
            if(targetToReach == null)
            {
                targetToReach = this.gameObject.GetComponent<Transform>();
            }
            else
            {
                targetToReach = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        if(theDistance <= inReachDistance)
        {
            navComponent.speed = runningSpeedZombie;
        }
        else if(theDistance >= inReachDistance)
        {
            navComponent.speed = walkingSpeedZombie;
        }
        /*transform.LookAt(targetToReach);*/
    }

    void FixedUpdate()
    {
        /*theRigidbodyZombie.velocity = (transform.forward * movementSpeedZombie);*/
    }

}

