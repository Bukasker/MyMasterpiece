using System.Collections;
using UnityEngine;

public class BasicTreeLogic : MonoBehaviour
{
	public int pointsToDestroy = 10;
	public Item item; /*<-- Here should be the whole item drop manager*/
	public GameObject TopOfTree;
	public GameObject BottomOfTree;




	public void UseTree()
	{

	}
	public void PokeTree(Equipment equipment)
	{
		pointsToDestroy -= equipment.toolLvl;

		if (pointsToDestroy <= 0)
		{
			//var itemGameObject = Instantiate(item, transform.position, Quaternion.identity);

		}

		if (pointsToDestroy <= 0)
		{
			PrefabAnimationsController.Instance.TreeStartToFall(TopOfTree);
		}
	}

}

