using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Plant")]

public class Plant : ScriptableObject
{
	[Header("Basic Info")]
	[Space]
	public string PlantName = "NewPlantName";
	public int PlantQuality = 0;
	public int DaysToGrow = 5;
	public int PlantDays;
	public Image[] PlantTextures = null;
	public Item PlantItem = null;
	
	//public Tool PlantTool = null;
	public PlantMenager PlantMenager = null;


}
