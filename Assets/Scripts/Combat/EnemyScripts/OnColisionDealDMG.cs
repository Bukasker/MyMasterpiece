using UnityEngine;

public class OnColisionDealDMG : MonoBehaviour
{
	public Enemy enemy;
	private void OnTriggerEnter(Collider col)
	{
		{
			if (col.CompareTag("Enemy") == false) return;

			enemy = col.GetComponent<Enemy>();
			enemy.Interact();

			Debug.Log($"{gameObject.name} collided with {col.name}");
		}
	}
}
