using System.Collections;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{
	[SerializeField] private Transform doorTransform;
	[SerializeField] private float moveDistance;
	[SerializeField] private float moveSpeed;
	private float progress;
	public bool IsOpen { get; private set; } = false;
	public void Open()
	{
		IsOpen = !IsOpen;
		StartCoroutine(MoveDoor());
	}
	public void Close() 
	{
		IsOpen = !IsOpen;
		StartCoroutine(MoveDoor());
	}
	public IEnumerator MoveDoor()
	{
		var toPositon = new Vector3(
			doorTransform.position.x,
			doorTransform.position.y,
			doorTransform.position.z + (IsOpen ? moveDistance : -moveDistance));

		while (doorTransform.position != toPositon)
		{
			transform.position = Vector3.MoveTowards(transform.position, toPositon, moveSpeed * Time.deltaTime);
			yield return null;
		}
	}

}
