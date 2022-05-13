using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class StateMachine : MonoBehaviour
{
    //comma separated list of identifiers 
    public enum State
    {
        Chase,
        Patrol,
        Attack,
    }


    public State currentState;
    public AIMovement aiMovement;
    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        NextState();
    }

    private void NextState()
    {
        //runs one of the cases that matches the value (in this example the value is currentState)
        switch (currentState)
        {
            case State.Chase:
                StartCoroutine(ChaseState());
                break;
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
            case State.Attack:
                StartCoroutine(AttackState());
                break;
        }
    }


    //Coroutine is a special method that can be paused and returned to later
    private IEnumerator ChaseState()
    {
        Debug.Log("Chasing: Enter");

        while (currentState == State.Chase)
        {
            aiMovement.AIMoveTowards(aiMovement.Player);

            //grabs the set Attack Distance from AIMovement
            if (Vector2.Distance(transform.position, aiMovement.Player.position)
                < aiMovement.AttackDistance)
            {
                currentState = State.Attack;
            }
            else if (Vector2.Distance(transform.position, aiMovement.Player.position)
                >= aiMovement.chaseDistance)
            {
                currentState = State.Patrol;
            }
            yield return null;
        }
        Debug.Log("Chasing: Exit");
        NextState();
    }

    private IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");

        while (currentState == State.Attack)
        {
            GetComponent<Health>().DealDamage(0.5f);
            //not close enough range to attack results in putting the ai back into chase state 
            if (Vector2.Distance(transform.position, aiMovement.Player.position)
                >= aiMovement.AttackDistance)
            {
                currentState = State.Chase;
            }
            yield return null;
        }
        Debug.Log("Attack: Exit");
        NextState();
    }
    private IEnumerator PatrolState()
    {
        Debug.Log("Patrolling: Enter");

        aiMovement.LowestDistance();

        while (currentState == State.Patrol)
        {
            //update
            aiMovement.WaypointUpdate();
            aiMovement.AIMoveTowards(aiMovement.waypoints[aiMovement.waypointIndex]);

            if (Vector2.Distance(transform.position, aiMovement.Player.position)
               < aiMovement.chaseDistance)
            {
                currentState = State.Chase;
            }

            yield return null;
        }
        Debug.Log("Patrolling: Exit");
        NextState();
    }


}