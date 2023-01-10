using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
	public Equipment equipment;
	private string itemName;
	private string description;

	private float Value = 0f;
	private float Weight = 0f;

	private int armor;
	private int damage;

	[SerializeField] private TextMeshProUGUI itemNameText;
	[SerializeField] private TextMeshProUGUI itemValueText;
	[SerializeField] private TextMeshProUGUI itemWeightText;
	[SerializeField] private TextMeshProUGUI Text1;
	[SerializeField] private TextMeshProUGUI Text2;
	public void OnDescriptionDisplay(Item item)
	{
		if (item != null)
		{
			equipment = item as Equipment;
			if (equipment)
				switch (equipment.ItemType)
				{
					case ItemTypes.Weapon:
						if (equipment.attackDamageModifier != 0)
						{
							Text1.text = "Damage : " + Convert.ToString(equipment.attackDamageModifier);
						}
						if (equipment.arrowDamageModifier != 0)
						{
							Text1.text = "Damage : " + Convert.ToString(equipment.arrowDamageModifier);
						}
						Text2.text = equipment.Description;
						break;
					case ItemTypes.Apperance:
						armor = equipment.armorModifier;
						Text1.text = "Damage : " + Convert.ToString(equipment.armorModifier);
						Text2.text = equipment.Description;
						break;
					case ItemTypes.Potion:
						//	listOfItems = potionItems;
						break;
					case ItemTypes.Food:
						//	listOfItems = foodItems;
						break;
					case ItemTypes.Book:
						//	listOfItems = bookItems;
						break;
					case ItemTypes.Ingridiens:
						//	listOfItems = ingridiensItems;
						break;
					case ItemTypes.Key:
						//	listOfItems = keyItems;
						break;
				}
			itemName = equipment.ItemName;
			Value = equipment.Value;
			Weight = equipment.Weight;

			itemNameText.text = itemName;
			itemValueText.text = Convert.ToString(Value);
			itemWeightText.text = Convert.ToString(Weight);

		}
	}
}
