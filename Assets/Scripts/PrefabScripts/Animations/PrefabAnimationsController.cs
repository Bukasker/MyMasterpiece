using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabAnimationsController : MonoBehaviour
{

	[Header("Shake Animation")]
	public float shakeAmount = 2;    // Amplituda potrz¹sania
	public float shakeSpeed = 5;     // Szybkoœæ potrz¹sania
	public float shakeDuration = 2.0f;  // Czas trwania potrz¹sania

	private Transform objectToShake;
	private Quaternion originalRotation; // Pocz¹tkowa rotacja obiektu

	[Header("Falling Tree")]
	public float targetRotation = 190.0f; // Docelowa liczba stopni do obrotu
	public float rotationSpeed = 90.0f; // Prêdkoœæ obrotu w stopniach na sekundê

	private GameObject TopOfTree;
	private Quaternion initialRotation;
	private Quaternion targetQuaternion;

	#region Singeton
	public static PrefabAnimationsController Instance;

	private void Start()
	{
		if (Instance == null)
		Instance = this;
	}
	#endregion

	public void StartShakeGameObject(GameObject objectShake)
	{
		var objectA = objectShake;
		objectToShake = objectA.GetComponent<Transform>();
		originalRotation = objectToShake.rotation;
		//StartCoroutine(ShakeCoroutine());
	}

	public void TreeStartToFall(GameObject TopTree)
	{
		TopOfTree = TopTree;
		var transformTree = TopOfTree.GetComponent<Transform>();
		initialRotation = transformTree.rotation;
		targetQuaternion = initialRotation * Quaternion.Euler(0, 0, targetRotation);
		StartCoroutine(TreeFallCourotine(targetRotation));
	}



	IEnumerator TreeFallCourotine(float degrees)
	{
		var transformTree = TopOfTree.GetComponent<Transform>();
		Quaternion startRotation = transformTree.rotation;
		Quaternion endRotation = initialRotation * Quaternion.Euler(0, 0, degrees);

		float startTime = Time.time;
		float journeyLength = Quaternion.Angle(startRotation, endRotation);

		while (Quaternion.Angle(transformTree.rotation, endRotation) > 0.01f)
		{
			float distanceCovered = (Time.time - startTime) * rotationSpeed;
			float fractionOfJourney = distanceCovered / journeyLength;

			transformTree.rotation = Quaternion.Lerp(startRotation, endRotation, fractionOfJourney);
			yield return null;
		}

		Destroy(TopOfTree);
		transformTree.rotation = endRotation;
	}

	
	private IEnumerator ShakeCoroutine()
	{
		float elapsedTime = 0f;
		Quaternion startRotation = objectToShake.rotation;

		while (elapsedTime < shakeDuration)
		{
			float shakeOffset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
			Vector3 newRotationEuler = originalRotation.eulerAngles + new Vector3(0f, 0f, shakeOffset);
			Quaternion targetRotation = Quaternion.Euler(newRotationEuler);
			objectToShake.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / shakeDuration);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// Animacja powrotu do pocz¹tkowej rotacji
		float returnTime = 0f;
		while (returnTime < 1f)
		{
			objectToShake.rotation = Quaternion.Lerp(objectToShake.rotation, originalRotation, returnTime);
			returnTime += Time.deltaTime * 2f; // Szybkoœæ animacji powrotu jest podwójna
			yield return null;
		}

		objectToShake.rotation = originalRotation; // Upewniamy siê, ¿e obiekt jest dok³adnie w pocz¹tkowej rotacji
	}
	
}

