using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float _moveSpeed;
    float _walkSpeed;
    Vector3 _velocity;
    Vector3 _moveDirection;
    CharacterController _characterController;

    [Header("Jumping")]
    [SerializeField] float _jumpForce;
    float _ySpeed;

    [Header("Crouching")]
    [SerializeField] float _crouchSpeed;
    [SerializeField] bool _isCrouching;

    [Header("Keybinds")]
    [SerializeField] KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] KeyCode _crouchKey = KeyCode.LeftControl;

    void Start()
    {
        _walkSpeed = _moveSpeed;
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Jump();
        Crouch();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    void Jump()
    {
        if (_characterController.isGrounded)
        {
            if (Input.GetKeyDown(_jumpKey))
            {
                _isCrouching = false;
                _ySpeed = _jumpForce;
            }
        }
        if (!_characterController.isGrounded)
        {
            _ySpeed += Physics.gravity.y * Time.deltaTime * 2;
        }
    }

    void MovePlayer()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        _moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        var magnitude = Mathf.Clamp01(_moveDirection.magnitude) * (_isCrouching ? _crouchSpeed : _walkSpeed);
        _moveDirection.Normalize();


        _velocity = _moveDirection * magnitude;
        _velocity.y = _ySpeed;

        _characterController.Move(_velocity * Time.deltaTime);



        /*
            if (_velocity.x != 0 || _velocity.z != 0 )
            {
                if (_runSpeed < _maxRunSpeed)
                {
                    _speed = _runSpeed + Mathf.Pow(_minRunSpeed, 2f)/15;
                }
                else
                {
                    _speed = _maxRunSpeed;
                }
            }
            else
            {
                _speed -= _minRunSpeed;
            }

        }
        */
    }
    void Crouch()
    {
        if (Input.GetKeyDown(_crouchKey))
        {
            _isCrouching = !_isCrouching;

        }
        if (_isCrouching)
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}