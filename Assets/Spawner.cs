using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float timeAccumulator = 0.0f;
    public float interval = 0.5f;
    public int capacity = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeAccumulator += Time.deltaTime;
        while(timeAccumulator > interval)
        {
            Instantiate(enemyPrefab, GameObject.Find("SpawnPoint").transform.position, Quaternion.identity);
            timeAccumulator -= interval;
        }
    }
}
