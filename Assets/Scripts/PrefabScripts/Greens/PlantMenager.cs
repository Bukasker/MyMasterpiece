using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMenager : MonoBehaviour
{
	public static List<GameObject>  Plants = null;

	public void OnDayEnd()
	{
		for (int i = 0; i < Plants.Count; i++)
		{
			GameObject plantGameObject = Plants[i];
			Plant plant = plantGameObject.GetComponent<Plant>();
			plantGameObject.GetComponent<SpriteRenderer>().sprite = null;
			
		}
	}
}
