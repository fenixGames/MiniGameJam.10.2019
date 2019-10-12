using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Stationary = 0,
    Forward = 1,
    Backward = 2,
    Left = 4,
    ForwardLeft = 5,
    BackwardLeft = 6,
    Right = 8,
    ForwardRight = 9,
    BackwardRight = 10
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float turningSpeed = 1.0f;

    [SerializeField]
    private GameObject mouseTracker;

    private Direction direction = Direction.Stationary;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Awake()
    {
        direction = Direction.Stationary;
    }

    private void CheckKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.W)) // Forward
        {
            direction |= Direction.Forward;
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Backwards
        {
            direction |= Direction.Backward;
        }
        else if (Input.GetKeyDown(KeyCode.A)) // Left
        {
            direction |= Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Right
        {
            direction |= Direction.Right;
        }
    }


    private void CheckKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.W)) // Forward
        {
            direction &= ~ Direction.Forward;
        }
        else if (Input.GetKeyUp(KeyCode.S)) // Backwards
        {
            direction &= ~ Direction.Backward;
        }
        else if (Input.GetKeyUp(KeyCode.A)) // Left
        {
            direction &= ~ Direction.Left;
        }
        else if (Input.GetKeyUp(KeyCode.D)) // Right
        {
            direction &= ~ Direction.Right;
        }
    }



    private void MovePlayer()
    {
        Vector3 lookingDirection = transform.forward;
        float xSpeed, ySpeed;

        xSpeed = ((direction & Direction.Left) > 0 ? -speed : 0) + 
            ((direction & Direction.Right) > 0 ? speed : 0);
        ySpeed = ((direction & Direction.Backward) > 0 ? -speed : 0) + 
            ((direction & Direction.Forward) > 0 ? speed : 0);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(xSpeed, 0.0f, ySpeed);
    }

    private void RotateTorwardsPoint()
    {
        Vector3 direction = (mouseTracker.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeyDown();
        CheckKeyUp();

        MovePlayer();
        RotateTorwardsPoint();
    }
}
