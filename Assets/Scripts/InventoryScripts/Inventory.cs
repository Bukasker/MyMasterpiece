using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> weaponItems = new List<Item>();
    public List<Item> apperanceItems = new List<Item>();
    public List<Item> potionItems = new List<Item>();
    public List<Item> foodItems = new List<Item>();
    public List<Item> bookItems = new List<Item>();
    public List<Item> ingridiensItems = new List<Item>();
	public List<Item> keyItems = new List<Item>();

	public delegate void OnItemChange();
    public OnItemChange onItemChangedCallback;

    public static Inventory Instance;

    public List<Item> listOfItems = new List<Item>();
    public Item currentItemType;

	#region Singeton
	void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Error : More than one instance of Inventory found");
            return;
        }
        Instance = this;
    }
    #endregion

    public void Add(Item item)
    {
        currentItemType = item;
		ChooseItemList(item);
		bool itemAlredyInInvetory = false;
        foreach (Item inventoryItem in listOfItems)
        {
            var allItemAmount = inventoryItem.itemAmount + item.itemAmount;
            if (inventoryItem.ItemName == item.ItemName && (allItemAmount) <= inventoryItem.maxStack)
            {
                inventoryItem.itemAmount += item.itemAmount;
                itemAlredyInInvetory = true;
            }
        }
        if (!itemAlredyInInvetory)
        {
            if (item != null)
            {
                Item copyItem = Instantiate(item);
                listOfItems.Add(copyItem);
            }
        }
	    onItemChangedCallback.Invoke();
    }
    public void Remove(Item item)
    {
		currentItemType = item;
		ChooseItemList(item);
        //ThrowItem(item, item.itemAmount);
        listOfItems.Remove(item);
        onItemChangedCallback.Invoke();
    }
	public void RemoveSlotItem(Item item)
	{
		currentItemType = item;
		ChooseItemList(item);
		//ThrowItem(item, item.itemAmount);
		listOfItems.Remove(item);
		onItemChangedCallback.Invoke();
	}
	public void ChooseItemList(Item item)
    {
        if (item != null)
        {
            switch (item.ItemType)
            {
                case ItemTypes.Weapon:
                    listOfItems = weaponItems;
                    break;
                case ItemTypes.Apperance:
                    listOfItems = apperanceItems;
                    break;
                case ItemTypes.Potion:
                    listOfItems = potionItems;
                    break;
                case ItemTypes.Food:
                    listOfItems = foodItems;
                    break;
                case ItemTypes.Book:
                    listOfItems = bookItems;
                    break;
                case ItemTypes.Ingridiens:
                    listOfItems = ingridiensItems;
                    break;
                case ItemTypes.Key:
					listOfItems = keyItems;
					break;
			}
        }
    }
}
