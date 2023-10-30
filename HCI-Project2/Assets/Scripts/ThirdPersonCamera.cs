using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public float distance; // The fixed distance from the target
    public float rotationSpeed; // Speed of mouse X rotation
    public float maxDistance = 5.0f; // Maximum distance from the target
    public LayerMask obstacleLayer; // Layer mask for obstacles

    private Transform playerTransform;
    private float currentRotationAngle = 0.0f;

    void Start()
    {
        playerTransform = target.transform;
    }

    void FixedUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerTransform = target.transform;

        // Calculate the desired camera position based on the target's position and rotation
        Vector3 offset = Quaternion.Euler(0, currentRotationAngle, 0) * (locationOffset + Vector3.back * distance);
        Vector3 desiredPosition = playerTransform.position + offset;

        // Check for obstacles using a raycast
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, desiredPosition - playerTransform.position, out hit, maxDistance, obstacleLayer))
        {
            Debug.Log("hit");
            // If an obstacle is hit, adjust the desired position to be just in front of the obstacle
            desiredPosition = hit.point - (desiredPosition - playerTransform.position).normalized * 0.5f;
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Apply mouse X rotation to the camera
        currentRotationAngle += rotationSpeed * Input.GetAxis("Mouse X");

        // Loop the rotation angle from -180 to 180 degrees
        currentRotationAngle = (currentRotationAngle + 180.0f) % 360.0f - 180.0f;

        transform.LookAt(playerTransform.position);
    }
}