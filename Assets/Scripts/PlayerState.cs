using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    int hp = 100;
    int resources;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int value)
    {
        hp -= value;
        if(hp == 0)
        {
            Debug.Log("Game over!");
        }
    }
}
