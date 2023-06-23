using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private float walkSpeed;
    private Vector3 velocity;
    private Vector3 moveDirection;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private bool playerHaveControll;

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
		playerHaveControll = true;
		walkSpeed = moveSpeed;
    }
    private void Update()
    {
	    Jump();
        Crouch();
    }
	private void FixedUpdate()
    {
        if (playerHaveControll)
        {
			MovePlayer();
		}
    }
	private void Jump()
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

	private void MovePlayer()
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
	private void Crouch()
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
	public void MovePlayerToPosition(Vector3 position, float offset)
	{
		position.y = transform.position.y;
        playerHaveControll = false;
        Vector3 targetPosition = position - (position - transform.position).normalized;
		StartCoroutine(MovePlayerCoroutine(position, offset));
	}

	private IEnumerator MovePlayerCoroutine(Vector3 targetPosition ,float offset)
	{
		float distance = Vector3.Distance(transform.position, targetPosition);
		while (distance > offset)
		{
			float progress = Mathf.Clamp01(walkSpeed * Time.deltaTime / distance);
			transform.position = Vector3.Lerp(transform.position, targetPosition, progress);
			distance = Vector3.Distance(transform.position, targetPosition);
			yield return null;
		}
        playerHaveControll = true;
		// Gracz dotar³ do docelowej pozycji
		Debug.Log("Gracz dotar³ do docelowej pozycji.");
	}
}