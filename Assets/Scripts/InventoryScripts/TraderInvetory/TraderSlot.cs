using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraderSlot : MonoBehaviour
{
	[SerializeField] private Image icon;
	public Item item;
	public bool itemIsFromPlayer;
	public TextMeshProUGUI itemName;
	public TextMeshProUGUI itemType;
	public TextMeshProUGUI itemValue;
	public void AddItemToSlot(Item Item)
	{
		item = Item;
		icon.sprite = item.Icon;

		itemName.text = item.ItemName;
		itemType.text = Convert.ToString(item.ItemType);
		itemValue.text = Convert.ToString(item.Value);
	}

	public void BuyItem()
	{
		TraderScript.lastTrader?.Buy(item);
		if (itemIsFromPlayer)
		{
			Destroy(gameObject);
		}
	}
}
