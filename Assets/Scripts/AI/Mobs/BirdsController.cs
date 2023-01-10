using System.Collections;
using UnityEngine;

public class BirdsController : MonoBehaviour
{
	[SerializeField] private GameObject StealthSphere;
	[SerializeField] private Transform thisTransform;
	[SerializeField] private float birdSpeed =4.5f;
	public bool isInAir;

	private void OnTriggerEnter(Collider collision)
	{
		if(collision.gameObject == StealthSphere && isInAir == false)
		{
			StartCoroutine(StartFly());
			isInAir = true;
		}
	}
	public IEnumerator StartFly()
	{
		// Play animation

		var slealthtransform = StealthSphere.GetComponent<Transform>();
		var randNum = Random.Range(0, 0.3f);
		yield return new WaitForSeconds(randNum);
		var b = slealthtransform.position.x + gameObject.transform.position.x;
		var toPos = new Vector3(thisTransform.position.x + b * 2.5f, +thisTransform.position.y + b * 2.5f, thisTransform.position.z);
		while (thisTransform.position != toPos)
		{
			thisTransform.position = Vector3.MoveTowards(thisTransform.position, toPos, birdSpeed * Time.deltaTime);
			yield return null;
		}
		Destroy(gameObject);
	}
}
