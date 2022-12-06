using UnityEngine;

public class RotationController : MonoBehaviour
{
	private Vector3 moveDirection;
	private bool isFacingRight;
	void Update()
	{
		var horizontalInput = Input.GetAxis("Horizontal");
		var verticalInput = Input.GetAxis("Vertical");
		moveDirection = new Vector3(horizontalInput, 0, verticalInput);
		var mousePos = Input.mousePosition;
		if (moveDirection.x < 0 && isFacingRight)
		{
			FlipX();
		}

		if (moveDirection.x > 0 && !isFacingRight)
		{
			FlipX();
		}
		if (Input.GetMouseButtonDown(0) && (mousePos.x > Screen.width/2) && !isFacingRight)
		{
			FlipX();
		}
		if (Input.GetMouseButtonDown(0) && (mousePos.x < Screen.width/2) && isFacingRight)
		{
			FlipX();
		}

	}
	void FlipX()
	{
		Vector3 mousePos = Input.mousePosition;
		transform.eulerAngles -= new Vector3(0, 180, 0);

		isFacingRight = !isFacingRight;
	}
}
