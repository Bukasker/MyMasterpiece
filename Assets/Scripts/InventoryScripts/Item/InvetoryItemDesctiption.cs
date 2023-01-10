using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InvetoryItemDesctiption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private InventorySlot inventorySlot;
	[SerializeField] private EquipmentSlotUI equipmentSlot;
	[SerializeField] private InventorySlot chestSlot;
	[SerializeField] private GameObject descriptionPanelUI;
	[SerializeField] private GameObject canvas;
	[SerializeField] private float waitTime = 0.8f;
	[SerializeField] private float BorderWidth = 337.92f;
	[SerializeField] private float BorderHeight = 353.28f;
	private GameObject test;

	public Item slotItem;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (test != null)
				test.SetActive(false);
		}
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (inventorySlot?.item != null)
		{
			StartCoroutine("ExampleCoroutine");
		}
		if (equipmentSlot?.item != null)
		{
			StartCoroutine("ExampleCoroutine");
		}
		if (chestSlot?.item != null)
		{
			StartCoroutine("ExampleCoroutine");
		}
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		StopCoroutine("ExampleCoroutine");
		if(test != null)
		{
			Destroy(test);
		}
	}

	IEnumerator ExampleCoroutine()
	{
		yield return new WaitForSeconds(waitTime);
		canvas = GameObject.FindGameObjectWithTag("MainCanvas");
		test = Instantiate(descriptionPanelUI);
		test.transform.SetParent(canvas.transform);
		var description = test.GetComponent<ItemDescription>();
		if (inventorySlot != null)
		{
			slotItem = inventorySlot.item;
		}
		else if (equipmentSlot != null)
		{
			slotItem = equipmentSlot.item;
		}
		else if (chestSlot != null)
		{
			slotItem = chestSlot.item;
		}

		if (slotItem != null)
		{
			descriptionPanelUI.SetActive(true);
		}
		description.OnDescriptionDisplay(slotItem);
		Vector3 mousePos = Input.mousePosition;
		if (mousePos.y >= (Screen.height / 2f) && mousePos.x <= (Screen.width * 2 / 3f))
		{
			test.transform.position = new Vector3(mousePos.x + BorderWidth / 2, mousePos.y - BorderHeight / 2, mousePos.z);
		}
		if (mousePos.y <= (Screen.height / 2f) && mousePos.x >= (Screen.width * 2 / 3f))
		{
			test.transform.position = new Vector3(mousePos.x - BorderWidth / 2, mousePos.y + BorderHeight / 2, mousePos.z);
		}
		if (mousePos.y >= (Screen.height / 2f) && mousePos.x >= (Screen.width * 2 / 3f))
		{
			test.transform.position = new Vector3(mousePos.x - BorderWidth / 2, mousePos.y - BorderHeight / 2, mousePos.z);
		}
		if (mousePos.y <= (Screen.height / 2f) && mousePos.x <= (Screen.width * 2 / 3f))
		{
			test.transform.position = new Vector3(mousePos.x + BorderWidth / 2, mousePos.y + BorderHeight / 2, mousePos.z);
		}
	}

}

