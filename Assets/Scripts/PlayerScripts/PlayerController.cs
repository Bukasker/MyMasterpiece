using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private float walkSpeed;
    private Vector3 velocity;
    private Vector3 moveDirection;
	[SerializeField] private CharacterController characterController;

	[Header("Jumping")]
    [SerializeField] private float jumpForce;
    private float ySpeed;


    [Header("Crouching")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private bool isCrouching;

    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    void Start()
    {
        walkSpeed = moveSpeed;
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
        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(jumpKey))
            {
                isCrouching = false;
                ySpeed = jumpForce;
            }
        }
        if (!characterController.isGrounded)
        {
            ySpeed += Physics.gravity.y * Time.deltaTime * 2;
        }
    }

    void MovePlayer()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        var magnitude = Mathf.Clamp01(moveDirection.magnitude) * (isCrouching ? crouchSpeed : walkSpeed);
        moveDirection.Normalize();


        velocity = moveDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);
    }
    void Crouch()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;

        }
        if (isCrouching)
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

}