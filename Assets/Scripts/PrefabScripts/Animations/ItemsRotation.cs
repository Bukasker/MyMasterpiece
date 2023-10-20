using System.Collections;
using UnityEngine;

public class ItemsRotation : MonoBehaviour
{
	public AnimationCurve verticalMovementCurve; // Krzywa animacji poruszania si� g�ra-d�
	public float verticalMovementDuration = 2f; // Zmieniona kr�tsza d�ugo�� animacji poruszania si�
	public float rotationSpeed = 90.0f; // Pr�dko�� obrotu w stopniach na sekund�

	private Vector3 initialPosition; // Pozycja pocz�tkowa
	private bool movingUp = true; // Flaga okre�laj�ca, czy przedmiot porusza si� w g�r�


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

			// Odwr�� kierunek ruchu
			movingUp = !movingUp;
			yield return null; // Czekaj na nast�pny klatk�
		}
	}
}
