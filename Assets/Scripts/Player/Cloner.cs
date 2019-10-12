using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private GameObject towerPrefab2;

    [SerializeField]
    private float cloningTimeout = 10.0f;

    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timer > cloningTimeout)
        {
            GameObject tower = Instantiate(towerPrefab);
            tower.transform.position = transform.position;
            timer = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.Return) && timer > cloningTimeout)
        {
            GameObject tower = Instantiate(towerPrefab2);
            tower.transform.position = transform.position;
            timer = 0.0f;
        }

        if (timer < cloningTimeout)
            timer += Time.deltaTime;
    }
}
