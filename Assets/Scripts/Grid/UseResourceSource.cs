using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static TileMapData;

public class UseResourceSource : MonoBehaviour
{
	public TileType FoundTileType;
	public Vector3 TileVector3;
	public Vector3 selectedObjectVector3;
	public GameObject selectedTile;
	[SerializeField] private ToolBarMenager toolBarMenager;
	[SerializeField] private DirtDig dirtDig;
	public void UseSource(GameObject resource)
	{
		var equipment = (Equipment)toolBarMenager.CurrentSlot.item;
		if (selectedTile == resource)
		{
			if(FoundTileType == TileType.Tree)
			{
				var treeLogic = resource.GetComponent<BasicTreeLogic>();
				if (equipment.toolType == ToolType.Axe)
				{
					treeLogic.PokeTree(equipment);
				}
			}
			if(FoundTileType == TileType.Ore)
			{
				if(equipment.toolType == ToolType.Pickaxe)
				{

				}
			}
			if(FoundTileType == TileType.Dirt)
			{
				if(equipment.toolType == ToolType.Hoe)
				{
					dirtDig.AddDigedTile(selectedObjectVector3, selectedTile);
				}
				else if(equipment.toolType == ToolType.Pickaxe)
				{
					dirtDig.RemoveDigedTile(selectedObjectVector3, selectedTile);
				}
				else if(equipment.toolType == ToolType.Bucket)
				{
					dirtDig.AddWateredTile(selectedObjectVector3, selectedTile);
				}
			}
			if(FoundTileType == TileType.Water)
			{
				if(equipment.toolType == ToolType.FishingRod)
				{

				}
			}
		}
	}
	public void CheckTypeOfSelectedObject()
	{
		selectedObjectVector3 = selectedTile.GetComponent<Transform>().position;
		var vector2Object = new Vector3(selectedObjectVector3.x,selectedObjectVector3.y, selectedObjectVector3.z);

		foreach (KeyValuePair<Vector3, TileType> kvp in GridDic)
		{
			if (kvp.Key.Equals(vector2Object))
			{
				FoundTileType = kvp.Value;
				TileVector3 = kvp.Key;
				break;
			}
		}
	}
}
