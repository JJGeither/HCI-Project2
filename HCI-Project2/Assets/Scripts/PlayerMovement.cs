using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public Variables
    public float playerMaxSpeed;
    public float playerMinSpeed;
    public float rotationSpeed;
    public float acceleration;
    public float deceleration;
    public GameObject shellObject;

    // Private Variables
    Rigidbody _rb;
    float _playerSpeed = 0.0f;

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

            shellObject.SetActive(true);
            shellObject.transform.position = this.transform.position;
            shellObject.GetComponent<Rigidbody>().velocity = _rb.velocity;
            this.gameObject.SetActive(false);

        }
    }

    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate acceleration based on input
        float targetSpeed = _playerSpeed;

        if (verticalInput != 0.0 || horizontalInput != 0.0)
            targetSpeed += acceleration * Time.fixedDeltaTime;
        else
            targetSpeed -= deceleration * Time.fixedDeltaTime;

        // Clamp the speed to the specified range
        targetSpeed = Mathf.Clamp(targetSpeed, playerMinSpeed, playerMaxSpeed);

        // Smoothly adjust the playerSpeed towards the targetSpeed
        _playerSpeed = Mathf.MoveTowards(_playerSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);

        // Clamping input values
        float clampedVerticalInput = Mathf.Clamp(verticalInput, -1f, 1f);
        float clampedHorizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);

        float verticalMovement = clampedVerticalInput * _playerSpeed * Time.fixedDeltaTime;
        float horizontalMovement = clampedHorizontalInput * _playerSpeed * Time.fixedDeltaTime;
        float rotation = clampedHorizontalInput * rotationSpeed * Time.fixedDeltaTime;

        _rb.velocity = new Vector3(horizontalMovement, 0, verticalMovement);

        // Rotate the object
        //transform.Rotate(Vector3.up * rotation);


    }
}
    