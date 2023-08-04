using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ToolMenager : MonoBehaviour
{
	public static ToolMenager Instance;
	//public Tools tool;
	private void Awake()
	{
		Instance = this;
	}

	public void UseTool()
	{
		//if (tool.toolType == Tools.ToolType.Axe && Selecting.SelectedObject )
		//{

		//}
		/* if (tool.toolType == Tools.ToolType.Pickaxe)
		{

		}
		else if (tool.toolType == Tools.ToolType.Hoe)
		{

		}
		else if (tool.toolType == Tools.ToolType.Scyle)
		{

		}
		else if (tool.toolType == Tools.ToolType.FishingRod)
		{

		}
		*/
		//animation
		//throw item /with chanse
	}
}
