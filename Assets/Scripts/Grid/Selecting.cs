using System.Collections.Generic;
using UnityEngine;
using static TileMapData;
using static UnityEngine.GraphicsBuffer;

public class Selecting : MonoBehaviour
{
	public Material highlightMaterial;
	public Material selectionMaterial;
	public Material originalMaterial;

	public PlayerController controller;
	public GameObject SelectedObject;
	public TileMapData.TileType tileType;
	public float distance = 1.7f;

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
			if(SelectedObject == null)
			{
				SelectedObject = hit.collider.gameObject;
			}
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
						//controller.MovePlayerToPosition(posistion, 4f);
					}
					else
					{
						SelectedObject = hit.collider.gameObject;
						var posistion = SelectedObject.GetComponent<Transform>().position;
						//controller.MovePlayerToPosition(posistion, distance);
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
			//other.gameObject.GetComponent<Renderer>().material = highlightMaterial;
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

	public void CheckTypeOfSelectedObject()
	{
		var selectedObjectVector3 = GetComponent<Transform>().position;
		var vector2Object = new Vector2(selectedObjectVector3.x, selectedObjectVector3.z);

		foreach (KeyValuePair<Vector2, TileType> kvp in GridDic)

		{
			if (kvp.Key.Equals(vector2Object))
			{
				var foundTileType = kvp.Value;
				break;
			}
		}
	}
}
