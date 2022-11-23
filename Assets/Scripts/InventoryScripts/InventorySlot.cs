using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    public Item item;

    public TextMeshProUGUI itemAmoutText;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.Icon;
        icon.enabled = true;
        itemAmoutText.enabled = true;
        itemAmoutText.text = Convert.ToString(item.itemAmount);
    }
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        itemAmoutText.enabled = false;
    }
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
    public void OnRemoveItem()
    {
        Inventory.Instance.Remove(item);
    }
}