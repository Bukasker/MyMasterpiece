using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarSlot : MonoBehaviour
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
	public virtual void UseTool()
	{
		Debug.Log("Tool Used !");
	}
}
