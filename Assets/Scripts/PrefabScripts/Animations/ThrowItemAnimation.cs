using UnityEngine;

public class ThrowItemAnimation : MonoBehaviour
{
	public float forceMagnitude = 10.0f; // Si�a wyrzutu obiektu.
	public float fixedY = 2.0f; // Warto�� Y na sta�e przypisana.

	private Rigidbody rb;
	private Collider col;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			rb = GetComponent<Rigidbody>();
			col = GetComponent<Collider>();

			// Losowy kierunek wyrzutu w p�aszczy�nie XY.
			Vector2 randomDirectionXY = Random.insideUnitCircle.normalized;
			Vector3 randomDirection = new Vector3(randomDirectionXY.x, fixedY, randomDirectionXY.y);

			// Wyrzucenie obiektu.
			rb.AddForce(randomDirection * forceMagnitude, ForceMode.Impulse);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		// Sprawdzenie, czy obiekt nie dotkn�� gracza lub przedmiotu.
		if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Item")
		{
			// Wy��czenie grawitacji.
			rb.useGravity = false;
			rb.isKinematic = true;
			// Wy��czenie collidera.
			col.enabled = false;
		}
	}
}
