using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player, mouseTracker;

    [SerializeField]
    private float offset;

    // Update is called once per frame
    void Update()
    {
        SetCameraPosition();
        SetCameraRotation();
    }

    private void SetCameraRotation()
    {
        Vector3 direction = (GetMiddlePoint() - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void SetCameraPosition()
    {
        Vector3 lookDirection = mouseTracker.transform.position - player.transform.position;
        Vector3 cameraVector = Vector3.Cross(lookDirection, Vector3.up).normalized;
        Vector3 cameraPosition = GetMiddlePoint() + offset * cameraVector;
        cameraPosition.y = transform.position.y;

        transform.position = cameraPosition;
    }

    private Vector3 GetMiddlePoint()
    {
        return (mouseTracker.transform.position + player.transform.position) / 2.0f;
    }
}
