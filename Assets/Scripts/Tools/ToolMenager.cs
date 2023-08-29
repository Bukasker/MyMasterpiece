using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolMenager : MonoBehaviour
{

	[SerializeField] Animator animator;
	private void Update()
	{
		if(ToolBarMenager.Instance.CurrentSlot != null && ToolBarMenager.Instance.CurrentSlot.item is Equipment)
		{
			var equipment = (Equipment)ToolBarMenager.Instance.CurrentSlot.item;
			if (equipment.toolType != ToolType.None)
			{
				if (Input.GetMouseButtonDown(0))
				{
					animator.SetTrigger("BasicUseAnim");
					switch (equipment.toolType)
					{
						case ToolType.Axe:
							break;
						case ToolType.Pickaxe:
							break;
						case ToolType.Hoe:
							break;
						case ToolType.Scyle:
							break;
						case ToolType.FishingRod:
							break;
					}

				}
			}
		}
	}
}
