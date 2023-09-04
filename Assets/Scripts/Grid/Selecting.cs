using System.Collections.Generic;
using UnityEngine;

public class Selecting : MonoBehaviour
{
	[Header("Gizmos")]
	[SerializeField] private Color highlightColor = Color.green;
	[SerializeField] private Color selectionColor = Color.magenta;
	[SerializeField] private bool enableSelectGizmos = true;

	[Header("Player")]
	[SerializeField] private PlayerController controller;
	[SerializeField] private float distance = 1.7f;

	[Header("Tiles")]
	public UseResourceSource UseResourceSource;
	public List<GameObject> tilesInRange;
	private GameObject hoveredTile;
	private GameObject selectedTile;


	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		int mask = 1 << 9; // Layer mask for raycast

		if (Physics.Raycast(ray, out RaycastHit hit, 20.0f, mask))
		{
			if (hit.collider.CompareTag("Selectable"))
			{
				hoveredTile = hit.collider.gameObject;

				if (Input.GetMouseButtonDown(0))
				{
					selectedTile = hoveredTile;

					if (selectedTile != null)
					{
						if (tilesInRange.Contains(selectedTile))
						{
							UseResourceSource.selectedTile = selectedTile;
							UseResourceSource.CheckTypeOfSelectedObject();
						}
						var position = selectedTile.transform.position;
						//controller.MovePlayerToPosition(position, distance);
					}
				}
				else if (Input.GetMouseButtonDown(1))
				{
					selectedTile = hoveredTile;

					if (selectedTile != null)
					{
						if (tilesInRange.Contains(selectedTile))
						{
							PrefabAnimationsController.Instance.StartShakeGameObject(selectedTile);
							//UseResourceSource.selectedTile = selectedTile;
							//UseResourceSource.CheckTypeOfSelectedObject();
						}
						var position = selectedTile.transform.position;
						//controller.MovePlayerToPosition(position, distance);
					}
				}
			}
			else
			{
				hoveredTile = null;
			}
		}
		else
		{
			hoveredTile = null;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Selectable"))
		{
			tilesInRange.Add(other.gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Selectable"))
		{
			tilesInRange.Remove(other.gameObject);
		}
	}
	private void OnDrawGizmos()
	{
		if (enableSelectGizmos != false)
		{
			if (hoveredTile != null)
			{
				if (tilesInRange.Contains(hoveredTile))
				{
					var posLeft = new Vector3(hoveredTile.transform.position.x + 0.8f, hoveredTile.transform.position.y + 0.8f, hoveredTile.transform.position.z);
					var posRight = new Vector3(hoveredTile.transform.position.x - 0.8f, hoveredTile.transform.position.y + 0.8f, hoveredTile.transform.position.z);
					var posUp = new Vector3(hoveredTile.transform.position.x, hoveredTile.transform.position.y + 0.8f, hoveredTile.transform.position.z + 0.8f);
					var posDown = new Vector3(hoveredTile.transform.position.x, hoveredTile.transform.position.y + 0.8f, hoveredTile.transform.position.z - 0.8f);

					Gizmos.color = highlightColor;
					Vector3 cubeScale = new Vector3(0.4f, 0.2f, 4) * 0.5f; // scaleFactor to wartoœæ, przez któr¹ chcesz zwiêkszyæ szeœcian

					Gizmos.DrawCube(posLeft, cubeScale);
					Gizmos.DrawCube(posRight, cubeScale);

					cubeScale = new Vector3(4, 0.2f, 0.4f) * 0.5f;
					Gizmos.DrawCube(posUp, cubeScale);
					Gizmos.DrawCube(posDown, cubeScale);

				}
			}
			if (selectedTile != null)
			{
				if (tilesInRange.Contains(selectedTile))
				{

					var posLeft = new Vector3(selectedTile.transform.position.x + 0.8f, selectedTile.transform.position.y + 0.8f, selectedTile.transform.position.z);
					var posRight = new Vector3(selectedTile.transform.position.x - 0.8f, selectedTile.transform.position.y + 0.8f, selectedTile.transform.position.z);
					var posUp = new Vector3(selectedTile.transform.position.x, selectedTile.transform.position.y + 0.8f, selectedTile.transform.position.z + 0.8f);
					var posDown = new Vector3(selectedTile.transform.position.x, selectedTile.transform.position.y + 0.8f, selectedTile.transform.position.z - 0.8f);

					Gizmos.color = selectionColor;
					Vector3 cubeScale = new Vector3(0.4f, 0.2f, 4) * 0.5f; // scaleFactor to wartoœæ, przez któr¹ chcesz zwiêkszyæ szeœcian

					Gizmos.DrawCube(posLeft, cubeScale);
					Gizmos.DrawCube(posRight, cubeScale);

					cubeScale = new Vector3(4, 0.2f, 0.4f) * 0.5f;
					Gizmos.DrawCube(posUp, cubeScale);
					Gizmos.DrawCube(posDown, cubeScale);
				}
			}

		}
	}
}