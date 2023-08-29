using UnityEngine;
using System.Collections;

public class ShakeAnimation : MonoBehaviour
{
	public Transform objectToShake;    // Obiekt do wstrz¹sania
	public float shakeAmount = 0.1f;    // Amplituda potrz¹sania
	public float shakeSpeed = 1.0f;     // Szybkoœæ potrz¹sania
	public float shakeDuration = 2.0f;  // Czas trwania potrz¹sania

	private Quaternion originalRotation; // Pocz¹tkowa rotacja obiektu
	private bool isShaking = false;      // Flaga wskazuj¹ca, czy trwa potrz¹sanie

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

		// Animacja powrotu do pocz¹tkowej rotacji
		float returnTime = 0f;
		while (returnTime < 1f)
		{
			objectToShake.rotation = Quaternion.Lerp(objectToShake.rotation, originalRotation, returnTime);
			returnTime += Time.deltaTime * 2f; // Szybkoœæ animacji powrotu jest podwójna
			yield return null;
		}

		objectToShake.rotation = originalRotation; // Upewniamy siê, ¿e obiekt jest dok³adnie w pocz¹tkowej rotacji
		isShaking = false;
	}
}
