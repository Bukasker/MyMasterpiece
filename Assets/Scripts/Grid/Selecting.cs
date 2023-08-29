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
		if(enableSelectGizmos != false)
		{
			if (hoveredTile != null)
			{
				Gizmos.color = highlightColor;
				Vector3 cubeScale = Vector3.one * 1.3f; // scaleFactor to wartoœæ, przez któr¹ chcesz zwiêkszyæ szeœcian
				Gizmos.DrawCube(hoveredTile.transform.position, cubeScale);
			}

			if (selectedTile != null)
			{
				Gizmos.color = selectionColor;
				Vector3 cubeScale = Vector3.one * 1.3f; // scaleFactor to wartoœæ, przez któr¹ chcesz zwiêkszyæ szeœcian
				Gizmos.DrawCube(selectedTile.transform.position, cubeScale);
			}
		}
	}
}