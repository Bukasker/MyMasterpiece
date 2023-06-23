using System.Collections.Generic;
using UnityEngine;

public class Selecting : MonoBehaviour
{
	public Material highlightMaterial;
	public Material selectionMaterial;
	public Material originalMaterial;

	public PlayerController controller;
	public GameObject SelectedObject;

	public List<GameObject> TilesInRange;
	public bool containsGameObject;

	private void Update()
	{

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		int mask = 1 << 9;
		mask = mask;

		if (SelectedObject != null)
		{
			if (TilesInRange.Contains(SelectedObject))
			{
				containsGameObject = true;
			}
			else
			{
				containsGameObject = false;
			}
		}


		if (Physics.Raycast(ray, out RaycastHit hit, 20.0f, mask))
		{
			if (SelectedObject.CompareTag("Selectable"))
			{
				if (Input.GetMouseButtonDown(0))
				{

					SelectedObject = hit.collider.gameObject;
					if (TilesInRange.Contains(SelectedObject))
					{
						containsGameObject = true;
						SelectedObject = hit.collider.gameObject;
						var posistion = SelectedObject.GetComponent<Transform>().position;
						controller.MovePlayerToPosition(posistion, 4f);
					}
					else
					{
						SelectedObject = hit.collider.gameObject;
						var posistion = SelectedObject.GetComponent<Transform>().position;
						controller.MovePlayerToPosition(posistion, 4f);
					}

					if (containsGameObject && SelectedObject != null)
					{
						SelectedObject = hit.collider.gameObject;
					}
				}
			}
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Selectable"))
		{
			other.gameObject.GetComponent<Renderer>().material = highlightMaterial;
			TilesInRange.Add(other.gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Selectable"))
		{
			if (other.gameObject == SelectedObject)
			{
				SelectedObject = null;
				containsGameObject = false;
			}
			TilesInRange.Remove(other.gameObject);
		}
	}
}
