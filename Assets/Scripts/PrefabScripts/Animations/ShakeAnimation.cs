using UnityEngine;
using System.Collections;

public class ShakeAnimation : MonoBehaviour
{
	public Transform objectToShake;    // Obiekt do wstrz�sania
	public float shakeAmount = 0.1f;    // Amplituda potrz�sania
	public float shakeSpeed = 1.0f;     // Szybko�� potrz�sania
	public float shakeDuration = 2.0f;  // Czas trwania potrz�sania

	private Quaternion originalRotation; // Pocz�tkowa rotacja obiektu
	private bool isShaking = false;      // Flaga wskazuj�ca, czy trwa potrz�sanie

	private void Start()
	{
		originalRotation = objectToShake.rotation;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L) && !isShaking)
		{
			StartCoroutine(ShakeCoroutine());
		}
	}

	private IEnumerator ShakeCoroutine()
	{
		isShaking = true;
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

		// Animacja powrotu do pocz�tkowej rotacji
		float returnTime = 0f;
		while (returnTime < 1f)
		{
			objectToShake.rotation = Quaternion.Lerp(objectToShake.rotation, originalRotation, returnTime);
			returnTime += Time.deltaTime * 2f; // Szybko�� animacji powrotu jest podw�jna
			yield return null;
		}

		objectToShake.rotation = originalRotation; // Upewniamy si�, �e obiekt jest dok�adnie w pocz�tkowej rotacji
		isShaking = false;
	}
}
