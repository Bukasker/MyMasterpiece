using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _runSpeed;
    public float _jumpForce;
    float _ySpeed;
    CharacterController _characterController;
    private Vector3 movementDirection;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }
    void Move()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        var magnitude = Mathf.Clamp01(movementDirection.magnitude) * _runSpeed;
        movementDirection.Normalize();

        _ySpeed += Physics.gravity.y * Time.deltaTime * 2;

        if (_characterController.isGrounded)
        {
            _ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                _ySpeed = _jumpForce;
            }
        }
        else
        {
            _characterController.stepOffset = 0;
        }

        var velocity = movementDirection * magnitude;
        velocity.y = _ySpeed;

        _characterController.Move(velocity * Time.deltaTime);
    }
}
