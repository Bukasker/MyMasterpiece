using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tool", menuName = "Inventory/Tool")]
public class Tools : Item
{
	public ToolType toolType;
	public ToolBarSlot toolSlot;

	public override void Use()
	{
		base.Use();
		ToolMenager.Instance.UseTool();
	}

	public enum ToolType
	{
		Axe,
		Pickaxe,
		Hoe,
		Scyle,
		FishingRod,
	}
}
