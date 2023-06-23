using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLogic : MonoBehaviour
{
	[SerializeField] private List<GameObject> buildGridLvls;
	public List<GameObject> buildGrid;
	private void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Selectable"))
		{
			buildGrid.Add(col.gameObject);
		}
	}
	private void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Selectable"))
		{
			buildGrid.Remove(col.gameObject);
		}

	}
	public void UseSelected(GameObject SelectedObject)
	{
		Vector3 gameObjectTranform = SelectedObject.transform.position;


	}
}
