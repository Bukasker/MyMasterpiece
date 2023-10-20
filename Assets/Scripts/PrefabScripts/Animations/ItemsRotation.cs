using System.Collections;
using UnityEngine;

public class ItemsRotation : MonoBehaviour
{
	public AnimationCurve verticalMovementCurve; // Krzywa animacji poruszania siê góra-dó³
	public float verticalMovementDuration = 2f; // Zmieniona krótsza d³ugoœæ animacji poruszania siê
	public float rotationSpeed = 90.0f; // Prêdkoœæ obrotu w stopniach na sekundê

	private Vector3 initialPosition; // Pozycja pocz¹tkowa
	private bool movingUp = true; // Flaga okreœlaj¹ca, czy przedmiot porusza siê w górê


	private void Start()
	{
		initialPosition = transform.position;
		StartCoroutine(ItemRotationCoroutine());
	}

	private IEnumerator ItemRotationCoroutine()
	{
		while (true)
		{
			float elapsedTime = 0f;
			Vector3 startPos = transform.position;
			Vector3 endPos;

			if (movingUp)
			{
				endPos = initialPosition + Vector3.up - new Vector3(0,0.5f,0);
			}
			else
			{
				endPos = initialPosition;
			}

			while (elapsedTime < verticalMovementDuration)
			{
				float t = elapsedTime / verticalMovementDuration;
				transform.position = Vector3.Lerp(startPos, endPos, t);
				transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
				elapsedTime += Time.deltaTime;
				yield return null;
			}

			// Odwróæ kierunek ruchu
			movingUp = !movingUp;
			yield return null; // Czekaj na nastêpny klatkê
		}
	}
}
