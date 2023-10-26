using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    GameObject player;
    public Vector3 offset;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Start()
    {
        
    }

    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Get player object
        if (player != null)
        {
            Vector3 pos = player.transform.position;  // Get player position

            Camera.main.transform.position = new Vector3(pos.x, pos.y, pos.z) + offset;  // Translate main camera to players position (follow)
        }

        // Update camera rotation based on user input.
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}