using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMouseMovement : MonoBehaviour
{
    [SerializeField]
    private float trackSpeed = 1.0f;
    public Vector3 offset;
    Vector3 moveVector;
    public Transform reference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 moveVector = Vector3.zero;

        moveVector.x += Input.GetAxis("Mouse X");
        moveVector.z += Input.GetAxis("Mouse Y");

        Vector3 localPos = transform.localPosition;
        //localPos.y = 0;

        //transform.localPosition = localPos;
        transform.position =  reference.transform.position + trackSpeed * moveVector;


    }
}
