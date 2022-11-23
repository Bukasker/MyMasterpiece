using UnityEngine;

public class InventoryController : MonoBehaviour
{
	[SerializeField] private GameObject[] _inventoriesUI;
	[SerializeField] private GameObject[] _ScrollbarUI;
	[SerializeField] private GameObject Inventory;
	[SerializeField] KeyCode invetoryKey = KeyCode.I;
	private bool inventoryActive;

	void Start()
	{
		inventoryActive = true;
		for (int i = 0; i < _inventoriesUI.Length; i++)
		{
			_inventoriesUI[i].SetActive(false);
			_ScrollbarUI[i].SetActive(false);
		}
		_inventoriesUI[0].SetActive(true);
		_ScrollbarUI[0].SetActive(true);
	}
	private void Update()
	{
		if (Input.GetKeyDown(invetoryKey))
		{
			Inventory.SetActive(inventoryActive);
			inventoryActive = !inventoryActive;
		}
	}
	public void OpenInventory(int inventoryNum)
	{
		for (int i = 0; i < _inventoriesUI.Length; i++)
		{
			_inventoriesUI[i].SetActive(false);
			_ScrollbarUI[i].SetActive(false);
		}
		_inventoriesUI[inventoryNum].SetActive(true);
		_ScrollbarUI[inventoryNum].SetActive(true);
	}
}
