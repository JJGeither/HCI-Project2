using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMovement : MonoBehaviour
{
    // Public Variables
    [SerializeField] public float shellSpeed;
    public GameObject playerObject;

    // Private Variables
    Rigidbody _rb;
    Vector3 lastVelocity;

    private void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {

        // Check if 'J' key is pressed and transform the object
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Transform this object to match the targetObject
            playerObject.SetActive(true);
            playerObject.transform.position = this.transform.position;
            _rb.velocity = Vector3.zero;
            this.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        lastVelocity = _rb.velocity;
        _rb.velocity = (Vector3.Normalize(_rb.velocity) * 50);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {

            float speed = lastVelocity.magnitude;
            Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            // Ensure that the object continues moving horizontally by applying a minimum velocity in the x-direction
            Vector3 reflectedVelocity = direction * Mathf.Max(speed, 2f);
            if (Mathf.Abs(reflectedVelocity.x) < 0.1f)
            {
                reflectedVelocity.x = 0.1f * Mathf.Sign(reflectedVelocity.x);
            }

            _rb.velocity = reflectedVelocity;
            Debug.Log(collision.gameObject + " " + reflectedVelocity + " " + lastVelocity);
        }
    }

}
