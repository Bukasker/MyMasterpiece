using System.Collections.Generic;
using UnityEngine;

public class TraderScript : MonoBehaviour
{
	public static TraderScript lastTrader = null;
	public TraderScale TraderScale;
	public List<Item> TraderItems = new List<Item>();
	public static Item itemToTrade;
	private bool isBuying;
	public TraderUI traderUI;
	public PlayerStats playerStats;
	private bool itemIsIn;
	public void StartTrade()
	{
		traderUI.Open(TraderItems);
		lastTrader = this;
	}
	public void StopTrade()
	{
		lastTrader = null;
	}

	public void Buy(Item item)
	{
		isBuying = true;
		itemToTrade = item;
		TraderScale.AddItemToScles(item,true);
		traderUI.ItemToTrade = item;
	}
	public void Sell(Item item)
	{
		isBuying = false;
		itemToTrade = item;
		Inventory.Instance.Remove(item);
		playerStats.Gold += (item.Value * item.itemAmount);
		for(int i = 0;i< TraderItems.Count;i++)
		{
			if(item.ItemName == TraderItems[i].ItemName)
			{
				itemIsIn = true;
			}
		}
		if (!itemIsIn)
		{
			TraderItems.Add(item);
			traderUI.AddItemFromPlayer(item);
		}
		itemIsIn = false;
	}
}
