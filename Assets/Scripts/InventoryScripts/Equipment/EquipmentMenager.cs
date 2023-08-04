using System;
using UnityEngine;
using static UnityEditor.Progress;

public class EquipmentMenager : MonoBehaviour
{
	#region Singleton

	public static EquipmentMenager Instance;

	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public OnEquipmentChanged onEquipmentChanged;
	private void Awake()
	{
		Instance = this;
	}
	#endregion
	public Item[] currentEquipment;

	[SerializeField] private InventoryCursor cursor;
	[SerializeField] private EquipmentSlotUI[] equipmentSlotUI;
	public bool alredyEquiped;
	public Item items;
	public int Index;
	private void Start()
	{
		int numSlots = equipmentSlotUI.Length;
		currentEquipment = new Item[numSlots];
	}

	public void Equip(Item newItem, int slotIndex)
	{
		items = newItem;
		Index = slotIndex;
		Item oldItem = null;
		alredyEquiped = false;
		foreach (Equipment equipment in currentEquipment)
        {
            if (equipment != null && equipment == newItem)
            {
				alredyEquiped = true;
			}
        }

		if (slotIndex <= 5) //Armor equipment
		{
			if (newItem is Equipment && newItem.ItemType == ItemTypes.Apperance)
			{

				if (currentEquipment[slotIndex] != null)
				{
					oldItem = (Equipment)currentEquipment[slotIndex];
					Inventory.Instance.Add(oldItem);
				}
				equipmentSlotUI[slotIndex].EquipItem((Equipment)newItem);
				currentEquipment[slotIndex] = (Equipment)newItem;
				Inventory.Instance.RemoveSlotItem(newItem);
				InventoryCursor.Instance.RemoveFromCursor();
			}
			else
			{
				InventoryCursor.Instance.RemoveFromCursor();
			}
		}
		else if (slotIndex >= 6  && slotIndex <= 8) //Potion equipment
		{
			if (newItem.ItemType == ItemTypes.Potion)
			{
				if (!alredyEquiped)
				{
					if (currentEquipment[slotIndex] != null)
					{
						oldItem = (Equipment)currentEquipment[slotIndex];
						Inventory.Instance.Add(oldItem);
					}
					equipmentSlotUI[slotIndex].EquipItem((Equipment)newItem);
					currentEquipment[slotIndex] = (Equipment)newItem;
					Inventory.Instance.RemoveSlotItem(newItem);
					InventoryCursor.Instance.RemoveFromCursor();
				}
				else
				{
					if (currentEquipment[slotIndex] != null)
					{
						oldItem = currentEquipment[slotIndex];
						Inventory.Instance.Add(oldItem);
					}
					equipmentSlotUI[slotIndex].EquipItem((Equipment)newItem);
					currentEquipment[slotIndex] = (Equipment)newItem;
					InventoryCursor.Instance.RemoveFromCursor();
					equipmentSlotUI[InventoryCursor.Instance.fromIndex].Unequip();
					currentEquipment[InventoryCursor.Instance.fromIndex] = null;
				}
			}
			else
			{
				InventoryCursor.Instance.RemoveFromCursor();
			}

		}
		else if (slotIndex >= 9 && slotIndex <= 20) //ToolBar equipment
		{
			if (!alredyEquiped)
			{
				if (currentEquipment[slotIndex] != null)
				{
					oldItem = currentEquipment[slotIndex];
				    Inventory.Instance.Add(oldItem);
				}
				if(newItem is Equipment)
				{
					equipmentSlotUI[slotIndex].EquipItem((Equipment)newItem);
					currentEquipment[slotIndex] = (Equipment)newItem;
				}
				else
				{
					equipmentSlotUI[slotIndex].EquipItem(newItem);
					currentEquipment[slotIndex] = newItem;
				}
				Inventory.Instance.RemoveSlotItem(newItem);
				InventoryCursor.Instance.RemoveFromCursor();
			}
			else
			{
				if (currentEquipment[slotIndex] != null)
				{
					oldItem = currentEquipment[slotIndex];
					equipmentSlotUI[InventoryCursor.Instance.fromIndex].EquipItem((Equipment)oldItem);
					currentEquipment[InventoryCursor.Instance.fromIndex] = (Equipment)oldItem;
					equipmentSlotUI[slotIndex].EquipItem((Equipment)newItem);
					currentEquipment[slotIndex] = (Equipment)newItem;
					InventoryCursor.Instance.RemoveFromCursor();
				}
				else
				{
					Item oldItemPrev = currentEquipment[InventoryCursor.Instance.fromIndex];
					equipmentSlotUI[InventoryCursor.Instance.fromIndex].Unequip();

					currentEquipment[InventoryCursor.Instance.fromIndex] = null;

					if (onEquipmentChanged != null && (oldItemPrev is Equipment))
					{
						onEquipmentChanged.Invoke(null, (Equipment)oldItemPrev);
					}

					equipmentSlotUI[slotIndex].EquipItem((Equipment)newItem);
					currentEquipment[slotIndex] = (Equipment)newItem;


					InventoryCursor.Instance.RemoveFromCursor();

					//equipmentSlotUI[InventoryCursor.Instance.fromIndex].Unequip();
					//currentEquipment[InventoryCursor.Instance.fromIndex] = null;

				}
			}
		}


		if (onEquipmentChanged != null && newItem is Equipment)
		{
			onEquipmentChanged.Invoke((Equipment)newItem, (Equipment)oldItem);
		}
	}
	public void Unequip(int slotIndex)
	{
		if(currentEquipment[slotIndex] is Equipment)
		{
			if (currentEquipment[slotIndex] != null)
			{
				Item oldItem = currentEquipment[slotIndex];
				equipmentSlotUI[slotIndex].Unequip();
				Inventory.Instance.Add(oldItem);
				InventoryCursor.Instance.RemoveFromCursor();

				currentEquipment[slotIndex] = null;

				if (onEquipmentChanged != null)
				{
					onEquipmentChanged.Invoke(null, (Equipment)oldItem);
				}
			}
		}
		else
		{
			Item oldItem = currentEquipment[slotIndex];
			equipmentSlotUI[slotIndex].Unequip();
			Inventory.Instance.Add(oldItem);
			InventoryCursor.Instance.RemoveFromCursor();

			currentEquipment[slotIndex] = null;
		}
	}
}