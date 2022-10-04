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

    public delegate void OnItemChange();
    public OnItemChange onItemChangedCallback;

    public static Inventory Instance;

    [SerializeField] private GameObject itemToSpawn;
    [SerializeField] private ItemPickUp itemPickUp;
    [SerializeField] private Transform PlayerPostion;

    [Range(1f, 10f)]
    [SerializeField] private float itemThrowForce = 2;

    private List<Item> listOfItems = new List<Item>();

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
        ChooseItemList(item);
        bool itemAlredyInInvetory = false;
        foreach (Item inventoryItem in listOfItems)
        {
            var allItemAmount = inventoryItem.itemAmount + item.itemAmount;
            if (inventoryItem.Name == item.Name && (allItemAmount) <= inventoryItem.maxStack)
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
       // onItemChangedCallback.Invoke();
    }
    public void Remove(Item item)
    {
        ChooseItemList(item);
        ThrowItem(item, item.itemAmount);
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
            }
        }
    }
    private void ThrowItem(Item item, int numberToDrop)
    {
        itemPickUp = itemToSpawn.GetComponent<ItemPickUp>();
        itemPickUp.Item = item;

        for (var i = 0; i < numberToDrop; ++i)
        {
            var itemInScene = Instantiate(itemToSpawn, new Vector3(PlayerPostion.position.x, PlayerPostion.position.y, PlayerPostion.position.z + 1), Quaternion.identity);
            var itemInSceneRb = itemInScene.GetComponent<Rigidbody>();
            var randomPostion = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 3f), Random.Range(0f, 1.5f));
            var positonToThrow = (randomPostion - PlayerPostion.position).normalized;
            itemInSceneRb.AddForce(positonToThrow * itemThrowForce, ForceMode.Impulse);
        }
    }
}
