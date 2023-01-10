using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using static UnityEditor.Progress;

public class StorageController : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private GameObject storagePanelGameObject;
	[SerializeField] private GameObject Slots;
	[SerializeField] private GameObject inventory;
	[SerializeField] private GameObject[] gameObjectToDisable;
	public InventorySlot[] ChestSlots;
	private TextMeshProUGUI[] ItemAmountTexts;
	private bool IsOpen;
	

	public List<Item> StorageItems = new List<Item>();

	private void Start()
	{
		ItemAmountTexts = Slots.GetComponentsInChildren<TextMeshProUGUI>();
		Add();
		ChangeUI();
		storagePanelGameObject.SetActive(false);
	}
	public void Move()
	{
		IsOpen = !IsOpen;
		animator.SetBool("IsOpen", IsOpen);
		storagePanelGameObject.SetActive(IsOpen);
		inventory.SetActive(IsOpen);
		for(int i = 0; i < gameObjectToDisable.Length; i++)
		{
			gameObjectToDisable[i].SetActive(!IsOpen);
		}
	}
	public void Add()
	{
		for (int i = 0; i < StorageItems.Count; i++)
		{
			for (int z = 0; z < StorageItems.Count; z++)
			{
				var item = StorageItems[i];
				var checkItem = StorageItems[z];
				if (item.ItemName == checkItem.ItemName && item.itemAmount <= item.maxStack && i!=z)
				{
					var cloneItem = Instantiate(StorageItems[z]);
					cloneItem.itemAmount += 1;
					StorageItems[i] = cloneItem;
					StorageItems.Remove(checkItem);
					cloneItem = null;
				}
				checkItem = null;
				item = null;
			}
		}
	}

	private void ChangeUI()
	{
		for (var i = 0; i < ChestSlots.Length; i++)
		{
			var CurrentInvetoryCount = StorageItems.Count;
			var CurrentAmountTexts = StorageItems.Count;
			if (i < CurrentInvetoryCount)
			{
				ChestSlots[i].AddItem(StorageItems[i]);
				if (StorageItems[i].itemAmount > 1)
				{
					ItemAmountTexts[i].enabled = true;
				}
				else
				{
					ItemAmountTexts[i].enabled = false;
				}
			}
			else
			{
				ChestSlots[i].ClearSlot();
				ItemAmountTexts[i].enabled = false;
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape) && IsOpen)
		{
			CloseStorage();
		}
	}
	public void CloseStorage()
	{
		storagePanelGameObject.SetActive(false);
		Move();
	}
}