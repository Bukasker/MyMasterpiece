using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	private Inventory _inventory;
	public static InventoryUI instanceUI;

	public GameObject WeaponSlotsParent;
	public GameObject ApperanceSlotsParent;
	public GameObject PotionSlotsParent;
	public GameObject FoodSlotsParent;
	public GameObject BookSlotsParent;
	public GameObject IngridiensSlotsParent;
	public GameObject KeySlotsParent;

	private InventorySlot[] WeaponSlots;
	private InventorySlot[] ApperanceSlots;
	private InventorySlot[] PotionSlots;
	private InventorySlot[] FoodSlots;
	private InventorySlot[] BookSlots;
	private InventorySlot[] IngridiensSlots;
	private InventorySlot[] KeySlots;

	private TextMeshProUGUI[] WeaponAmountTexts;
	private TextMeshProUGUI[] ItemApperanceAmountTexts;
	private TextMeshProUGUI[] ItemPotionAmountTexts;
	private TextMeshProUGUI[] ItemFoodAmountTexts;
	private TextMeshProUGUI[] ItemBookAmountTexts;
	private TextMeshProUGUI[] ItemIngridienAmountTexts;
	private TextMeshProUGUI[] KeyAmountTexts;

	public TextMeshProUGUI[] CurrentAmountTexts;
	public InventorySlot[] CurrentSlots;
	public int CurrentInvetoryCount;

	void Start()
	{
		if (instanceUI != null)
		{
			Debug.Log("Error: More than one instance of Inventory found");
			return;
		}
		instanceUI = this;

		_inventory = Inventory.Instance;
		_inventory.onItemChangedCallback += UpdateUI;

		WeaponSlots = WeaponSlotsParent.GetComponentsInChildren<InventorySlot>();
		ApperanceSlots = ApperanceSlotsParent.GetComponentsInChildren<InventorySlot>();
		PotionSlots = PotionSlotsParent.GetComponentsInChildren<InventorySlot>();
		FoodSlots = FoodSlotsParent.GetComponentsInChildren<InventorySlot>();
		BookSlots = BookSlotsParent.GetComponentsInChildren<InventorySlot>();
		IngridiensSlots = IngridiensSlotsParent.GetComponentsInChildren<InventorySlot>();
		KeySlots = KeySlotsParent.GetComponentsInChildren<InventorySlot>();

		WeaponAmountTexts = WeaponSlotsParent.GetComponentsInChildren<TextMeshProUGUI>();
		ItemApperanceAmountTexts = ApperanceSlotsParent.GetComponentsInChildren<TextMeshProUGUI>();
		ItemPotionAmountTexts = PotionSlotsParent.GetComponentsInChildren<TextMeshProUGUI>();
		ItemFoodAmountTexts = FoodSlotsParent.GetComponentsInChildren<TextMeshProUGUI>();
		ItemBookAmountTexts = BookSlotsParent.GetComponentsInChildren<TextMeshProUGUI>();
		ItemIngridienAmountTexts = IngridiensSlotsParent.GetComponentsInChildren<TextMeshProUGUI>();
		KeyAmountTexts = KeySlotsParent.GetComponentsInChildren<TextMeshProUGUI>();
	}

	void UpdateUI()
	{
		switch (_inventory.currentItemType.ItemType)
		{
			case ItemTypes.Weapon:
				CurrentSlots = WeaponSlots;
				CurrentAmountTexts = WeaponAmountTexts;
				CurrentInvetoryCount = _inventory.weaponItems.Count;
				break;
			case ItemTypes.Apperance:
				CurrentSlots = ApperanceSlots;
				CurrentAmountTexts = ItemApperanceAmountTexts;
				CurrentInvetoryCount = _inventory.apperanceItems.Count;
				break;
			case ItemTypes.Potion:
				CurrentSlots = PotionSlots;
				CurrentAmountTexts = ItemPotionAmountTexts;
				CurrentInvetoryCount = _inventory.potionItems.Count;
				break;
			case ItemTypes.Food:
				CurrentSlots = FoodSlots;
				CurrentAmountTexts = ItemFoodAmountTexts;
				CurrentInvetoryCount = _inventory.foodItems.Count;
				break;
			case ItemTypes.Book:
				CurrentSlots = BookSlots;
				CurrentAmountTexts = ItemBookAmountTexts;
				CurrentInvetoryCount = _inventory.bookItems.Count;
				break;
			case ItemTypes.Ingridiens:
				CurrentSlots = IngridiensSlots;
				CurrentAmountTexts = ItemIngridienAmountTexts;
				CurrentInvetoryCount = _inventory.ingridiensItems.Count;
				break;
			case ItemTypes.Key:
				CurrentSlots = KeySlots;
				CurrentAmountTexts = KeyAmountTexts;
				CurrentInvetoryCount = _inventory.keyItems.Count;
				break;
		}
		for (var i = 0; i < CurrentSlots.Length; i++)
		{
			if (i < CurrentInvetoryCount)
			{
				CurrentSlots[i].AddItem(_inventory.listOfItems[i]);
				if (_inventory.listOfItems[i].itemAmount > 1)
				{
					CurrentAmountTexts[i].enabled = true;
				}
				else
				{
					CurrentAmountTexts[i].enabled = false;
				}
			}	
			else
			{
				CurrentSlots[i].ClearSlot();
				CurrentAmountTexts[i].enabled = false;
			}
		}
	}
}
