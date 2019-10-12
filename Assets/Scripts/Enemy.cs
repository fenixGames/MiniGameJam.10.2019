using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int hp = 200;
    public int power = 10;

    NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = GameObject.Find("Goal").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = GameObject.FindObjectOfType<Camera>().transform.rotation;

        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    Destroy(gameObject);
                    GameObject.Find("PlayerState").GetComponent<PlayerState>().Damage(power);
                }
            }
        }
    }
}
