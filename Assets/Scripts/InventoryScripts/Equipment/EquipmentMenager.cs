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

	public Equipment[] _currentEquipment;
	private Equipment[] _currentEquipmentPotion;
	[SerializeField] private EquipmentSlotUI[] _equipmentSlotUI;
	[SerializeField] private EquipmentSlotUI[] _potionSlotUI;
	private Inventory _inventory;
	public static EquipmentMenager instance;
	public int potionIndex = 0;

	private void Start()
	{
		_inventory = Inventory.Instance;

		int numArmorSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		_currentEquipment = new Equipment[numArmorSlots];
		_currentEquipmentPotion = new Equipment[_potionSlotUI.Length];
	}

	public void Equip(Equipment newItem)
	{
		int slotIndex = (int)newItem.equipSlot;
		Equipment oldItem = null;

		if (_currentEquipment[slotIndex] != null)
		{
			oldItem = _currentEquipment[slotIndex];
			_inventory.Add(oldItem);
		}
		if (_currentEquipmentPotion[potionIndex] != null)
		{
			oldItem = _currentEquipmentPotion[potionIndex];
			_inventory.Add(oldItem);
		}
		if (onEquipmentChanged != null)
		{
			onEquipmentChanged.Invoke(newItem, oldItem);
		}
		if (newItem.ItemType != ItemTypes.Potion)
		{
			_equipmentSlotUI[slotIndex].EquipItem(newItem);
			_currentEquipment[slotIndex] = newItem;
		}
		if (newItem.ItemType == ItemTypes.Potion)
		{
			_potionSlotUI[potionIndex].EquipItem(newItem);
			_currentEquipmentPotion[potionIndex] = newItem;
			potionIndex++;
			if(potionIndex >= _potionSlotUI.Length)
			{
				potionIndex = 0;
			}
		}
	}
	public void Unequip(int slotIndex)
	{
		if (_currentEquipment[slotIndex] != null)
		{
			Equipment oldItem = _currentEquipment[slotIndex];
			_equipmentSlotUI[slotIndex].Unequip();
			_inventory.Add(oldItem);

			_currentEquipment[slotIndex] = null;

			if (onEquipmentChanged != null)
			{
				onEquipmentChanged.Invoke(null, oldItem);
			}
		}
	}
}
