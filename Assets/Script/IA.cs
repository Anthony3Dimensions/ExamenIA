using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    
    enum State
    {
        Attacking,
        Patrolling,
        Chasing
      
    }

    private State currentState;
    private Transform player;
    private NavMeshAgent agent;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float detectionRange = 15;
    [SerializeField] private float attackRange = 5;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }
    void Start()
    {
        SetRandomPoint();
        currentState = State.Patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Attacking:
                Attack();
            break;

            case State.Patrolling:
                Patrol();
            break;  

            case State.Chasing:
                Chase();
            break;  
        }
    }

    void SetRandomPoint()
    {
        agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length - 1)].position;
    }
    bool IsInRange(float range)
    {
        if(Vector3.Distance(transform.position, player.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Attack()
    {
        Debug.Log("WakalaMeDio");
        currentState = State.Chasing;
    }

    void Patrol()
    {
        if(Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            currentState = State.Chasing;
        }
        if(agent.remainingDistance < 0.5f)
        {
            SetRandomPoint();
        }
    }

    void Chase()
    {
        if(IsInRange(detectionRange) == false)
        {
            SetRandomPoint();
            currentState = State.Patrolling;
        }

        if(IsInRange(detectionRange) == false)
        {
           
            currentState = State.Attacking;
        }

        agent.destination = player.position;
    }
}
