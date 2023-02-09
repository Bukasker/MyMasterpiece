using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class TraderUI : MonoBehaviour
{
	public GameObject TradeUIObject;
	public GameObject InventoryObject;
	public GameObject[] InvetoryUIObject;
	public GameObject SlotsParent;
	public GameObject SlotPrefab;
	public Item ItemToTrade;
	private bool isActive;
	public PlayerStats playerStats;
	public TraderScale traderScale;

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape) && isActive)
		{
			Close();
		}
	}
	public void AcceptTranaction()
	{
		if(playerStats.Gold >= ItemToTrade.Value)
		{
			playerStats.Gold -= ItemToTrade.Value;
			Inventory.Instance.Add(ItemToTrade);
			traderScale.ResetUI();
		}
		else
		{
			traderScale.ResetUI();
		}
		ItemToTrade = null;
	}
	public void CancelTransaction() 
	{
		traderScale.ResetUI();
		ItemToTrade = null;
	}
	public void Open(List<Item> TraderItems)
	{
		Close();
		isActive = true;
		TradeUIObject.SetActive(true);
		InventoryObject.SetActive(true);
		for(int i = 0;i< InvetoryUIObject.Length; i++)
		{
			InvetoryUIObject[i].SetActive(false);
		}
		for(int i = 0;i<TraderItems.Count;i++)
		{
			var prefab = Instantiate(SlotPrefab);
			var SlotScript = prefab.GetComponent<TraderSlot>();
			SlotScript.AddItemToSlot(TraderItems[i]);
			prefab.transform.SetParent(SlotsParent.gameObject.transform);
			prefab.transform.SetAsLastSibling();
		}
	}
	public void Close()
	{
		for (var i = SlotsParent.transform.childCount - 1; i >= 0; i--)
		{
			Object.Destroy(SlotsParent.transform.GetChild(i).gameObject);
		}

		isActive = false;
		TradeUIObject.SetActive(false);
		InventoryObject.SetActive(false);
		for (int i = 0; i < InvetoryUIObject.Length; i++)
		{
			InvetoryUIObject[i].SetActive(true);
		}
	}
	public void AddItemFromPlayer(Item item)
	{
		var prefab = Instantiate(SlotPrefab);
		var SlotScript = prefab.GetComponent<TraderSlot>();
		SlotScript.itemIsFromPlayer = true;
		prefab.transform.SetParent(SlotsParent.gameObject.transform);
		prefab.transform.SetAsLastSibling();
		SlotScript.AddItemToSlot(item);
		
	}
}
