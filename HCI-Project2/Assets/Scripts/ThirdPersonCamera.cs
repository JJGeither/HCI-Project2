using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public float distance = 5.0f; // The fixed distance from the target
    public float rotationSpeed = 2.0f; // Speed of mouse X rotation

    private float currentRotationAngle = 0.0f;
    private Transform playerTransform;

    void FixedUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerTransform = target.transform;

        // Calculate the desired camera position based on the target's position and rotation
        Vector3 offset = Quaternion.Euler(0, currentRotationAngle, 0) * (locationOffset + Vector3.back * distance);
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Apply mouse X rotation to the camera
        currentRotationAngle += rotationSpeed * Input.GetAxis("Mouse X");
        transform.LookAt(target.transform.position);
    }
}
