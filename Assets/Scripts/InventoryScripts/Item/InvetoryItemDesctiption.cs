using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InvetoryItemDesctiption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private InventorySlot inventorySlot;
	[SerializeField] private EquipmentSlotUI equipmentSlot;
	[SerializeField] private GameObject itemBorder;
	[SerializeField] private float waitTime = 0.8f;
	[SerializeField] private float BorderWidth = 337.92f;
	[SerializeField] private float BorderHeight = 353.28f;

	private Item slotItem;
	void Start()
	{
		itemBorder = GameObject.Find("DescriptionPanel");
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			itemBorder.SetActive(false);
		}
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (inventorySlot?.item != null )
		{
			StartCoroutine("ExampleCoroutine");
		}

		if (equipmentSlot?.item != null)
		{
			StartCoroutine("ExampleCoroutine");
		}
	}
	public void OnPointerExit(PointerEventData eventData)
	{

		StopCoroutine("ExampleCoroutine"); 
		itemBorder.SetActive(false);
	}

	IEnumerator ExampleCoroutine()
	{
		yield return new WaitForSeconds(waitTime);
		var description = itemBorder.GetComponent<ItemDescription>();
		if (equipmentSlot == null)
		{
			slotItem = inventorySlot.item;
		}
		else if(equipmentSlot != null)
		{
			slotItem = equipmentSlot.item;
		}

		if (slotItem != null)
		{
			itemBorder.SetActive(true);
		}
		description.OnDescriptionDisplay(slotItem);
		Vector3 mousePos = Input.mousePosition;
		if (mousePos.y >= (Screen.height / 2f) && mousePos.x <= (Screen.width*2 / 3f))
		{
			itemBorder.transform.position = new Vector3(mousePos.x + BorderWidth / 2, mousePos.y - BorderHeight / 2, mousePos.z);
		}
		if (mousePos.y <= (Screen.height / 2f) && mousePos.x >= (Screen.width * 2 / 3f))
		{
			itemBorder.transform.position = new Vector3(mousePos.x - BorderWidth / 2, mousePos.y + BorderHeight / 2, mousePos.z);
		}
		if (mousePos.y >= (Screen.height / 2f) && mousePos.x >= (Screen.width * 2 / 3f))
		{
			itemBorder.transform.position = new Vector3(mousePos.x - BorderWidth / 2, mousePos.y - BorderHeight / 2, mousePos.z);
		}
		if (mousePos.y <= (Screen.height / 2f) && mousePos.x <= (Screen.width * 2 / 3f))
		{
			itemBorder.transform.position = new Vector3(mousePos.x + BorderWidth / 2, mousePos.y + BorderHeight / 2, mousePos.z);
		}
	}

}

