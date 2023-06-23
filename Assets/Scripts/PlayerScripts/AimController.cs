using System.Collections;
using UnityEngine;

public class AimController : MonoBehaviour
{
	[SerializeField] private GameObject Bow;
	[SerializeField] private Transform aimPivot;
	[SerializeField] private Transform arrowSpawnPoint;
	[SerializeField] private Transform mouseWorldPos;
	[SerializeField] private GameObject bulletPrefeb;
	[SerializeField] private float force = 1.3f;
	private Vector3 direction;

	public IEnumerator startAim()
	{
		bool isAiming = true;
		while (isAiming)
		{
			Bow.SetActive(true);
			var mousePos = Input.mousePosition;
			mousePos.z = Camera.main.nearClipPlane + 8;
			mouseWorldPos.position = Camera.main.ScreenToWorldPoint(mousePos);


			direction = new Vector3(
				mouseWorldPos.position.x,
				mouseWorldPos.position.y,
				aimPivot.position.z) - aimPivot.position;

			aimPivot.right = direction;
			yield return null;
		}
		Bow.SetActive(false);
		isAiming = false;
	}
	public void Shoot()
	{
		var newbullet = Instantiate(bulletPrefeb, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
		var newbulletRb = newbullet.GetComponent<Rigidbody>();
		newbulletRb.AddForce(direction * force, ForceMode.Impulse);
	}
}
