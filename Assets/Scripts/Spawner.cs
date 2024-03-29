﻿using System.Collections;
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
        while(capacity > 0 && timeAccumulator > interval)
        {
            Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            timeAccumulator -= interval;
            capacity--;

            interval = interval * 0.95f;
            if (interval < 0.1f)
                interval = 0.1f;
        }
    }
}
