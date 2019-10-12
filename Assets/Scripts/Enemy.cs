using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Health health;
    public int power = 10;
    bool targetingFork = false;

    NavMeshAgent navMeshAgent;
    static int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        GameObject[] forks = GameObject.FindGameObjectsWithTag("Fork");
        for(int i = 0; i < forks.Length; ++i)
        {
            if (counter % forks.Length == i)
            {
                navMeshAgent.destination = forks[i].transform.position;
                targetingFork = true;
                break;
            }
        }
        counter++;

        if (!targetingFork)
        {
            navMeshAgent.destination = GameObject.Find("Goal").transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    if (targetingFork)
                    {
                        navMeshAgent.destination = GameObject.Find("Goal").transform.position;
                        targetingFork = false;
                    }
                    else
                    {
                        Destroy(gameObject);
                        GameObject.Find("PlayerState").GetComponent<PlayerState>().Damage(power);
                    }
                }
            }
        }
    }
}
