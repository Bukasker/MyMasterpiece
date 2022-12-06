using UnityEngine;

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

	public Equipment[] currentEquipment;
	public Equipment[] currentEquipmentPotion;
	[SerializeField] private EquipmentSlotUI[] equipmentSlotUI;
	[SerializeField] private EquipmentSlotUI[] potionSlotUI;
	private Inventory inventory;
	public static EquipmentMenager instance;
	public int potionIndex = 0;

	private void Start()
	{
		inventory = Inventory.Instance;

		int numArmorSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numArmorSlots];
		currentEquipmentPotion = new Equipment[potionSlotUI.Length];
	}

	public void Equip(Equipment newItem)
	{
		int slotIndex = (int)newItem.equipSlot;
		Equipment oldItem = null;

		if (currentEquipment[slotIndex] != null)
		{
			oldItem = currentEquipment[slotIndex];
			inventory.Add(oldItem);
		}
		if (currentEquipmentPotion[potionIndex] != null)
		{
			oldItem = currentEquipmentPotion[potionIndex];
			inventory.Add(oldItem);
		}
		if (onEquipmentChanged != null)
		{
			onEquipmentChanged.Invoke(newItem, oldItem);
		}
		if (newItem.ItemType != ItemTypes.Potion)
		{
			equipmentSlotUI[slotIndex].EquipItem(newItem);
			currentEquipment[slotIndex] = newItem;
		}
		if (newItem.ItemType == ItemTypes.Potion)
		{
			potionSlotUI[potionIndex].EquipItem(newItem);
			currentEquipmentPotion[potionIndex] = newItem;
			potionIndex++;
			if (potionIndex >= potionSlotUI.Length)
			{
				potionIndex = 0;
			}
		}
	}
	public void Unequip(int slotIndex)
	{
		if (currentEquipment[slotIndex] != null)
		{
			Equipment oldItem = currentEquipment[slotIndex];
			equipmentSlotUI[slotIndex].Unequip();
			inventory.Add(oldItem);

			currentEquipment[slotIndex] = null;

			if (onEquipmentChanged != null)
			{
				onEquipmentChanged.Invoke(null, oldItem);
			}
		}
	}
}