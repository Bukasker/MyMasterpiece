using System.Collections;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{	public bool IsOpen { get; private set; } = false;

	[Header("Bindings")]
	[SerializeField] private Transform target;
	[Header("Settings")]
	[SerializeField] private float moveSpeed = 1f;
	[SerializeField] private Vector3 openOffset = new(-2, 0, 0);

	private Coroutine moveCoroutine = null;
	private Vector3 startPos;
	private float progress = 0f;
	private void Awake()
	{
		startPos = target.position;
	}

	public void Open()
	{
		if (moveCoroutine != null)
			StopCoroutine(moveCoroutine);

		moveCoroutine = StartCoroutine(MovementCoroutine(true));
	}
	public void Close()
	{
		if (moveCoroutine != null)
			StopCoroutine(moveCoroutine);

		moveCoroutine = StartCoroutine(MovementCoroutine(false));
	}

	public IEnumerator MovementCoroutine(bool doOpen)
	{
		while (doOpen ? (progress < 1f) : (progress > 0f))
		{
			target.position = Vector3.Lerp(
				startPos, startPos + openOffset, progress
			);
			progress += Time.deltaTime * (doOpen ? moveSpeed : -moveSpeed);
			yield return null;
		}
		if (doOpen)
			progress = 1f;
		else
			progress = 0f;

		IsOpen = doOpen;
		target.position = Vector3.Lerp(
			startPos, startPos + openOffset, progress);
	}
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		var st = startPos;
		if (st == Vector3.zero)
			st = target.position;
		Gizmos.DrawLine(st, st + openOffset);
		Gizmos.DrawSphere(st + openOffset, 0.1f);
	}
}