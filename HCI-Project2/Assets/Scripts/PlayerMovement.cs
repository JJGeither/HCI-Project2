using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Public Variables")]
    [SerializeField]
    public float playerMaxSpeed;

    [SerializeField]
    public float playerMinSpeed;

    [SerializeField]
    public float acceleration;

    [SerializeField]
    public float deceleration;

    [SerializeField]
    public GameObject shellObject;

    [SerializeField]
    public float gravityScale;

    [SerializeField]
    public float gravityScaleIdle;

    [SerializeField]
    public float jumpForce;

    private bool isJumping = false; // Flag to indicate jump input

    private float _playerSpeed;
    private float _jumpForce = 0; // The current jump force

    // Private Variables
    Rigidbody _rb;
    PlayerController _playerController;

    private void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _playerController = this.transform.parent.GetComponent<PlayerController>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _playerController.GetIsGrounded())
        {
            isJumping = true;
            _jumpForce = jumpForce; // Initialize jump force
            Debug.Log("Space key pressed");
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

        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);

        movement = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movement;

        // Apply both jump force and movement velocity
        Vector3 totalVelocity = movement + Vector3.up * _jumpForce;
        _rb.velocity = totalVelocity;

        if (isJumping)
        {
            // Gradually decrease jumpForce to make it smoother
            _jumpForce -= Time.fixedDeltaTime * 50;

            if (_jumpForce <= 0)
            {
                isJumping = false;
                _jumpForce = 0;
            }
        }

        if (!_playerController.GetIsGrounded())
            _rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        else
            _rb.AddForce(Physics.gravity * gravityScaleIdle, ForceMode.Acceleration);
    }
}
