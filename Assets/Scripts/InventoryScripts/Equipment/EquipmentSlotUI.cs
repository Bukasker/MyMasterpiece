 using UnityEngine;
using UnityEngine.UI;
public class EquipmentSlotUI : MonoBehaviour
{
    [SerializeField] private Sprite basicIcon;
    [SerializeField] private Image icon;
    public Item item;
    public int slotIndex;

	public void EquipItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.Icon;
        icon.enabled = true;
    }
    public void Unequip()
    {
        item = null;
        icon.sprite = basicIcon;
        if(basicIcon == null)
        {
			icon.enabled = false;
		}
    }

    public void CursorEquip()
    {   
        Item itemC = InventoryCursor.Instance.CursorItem;
        if (InventoryCursor.Instance.CursorItem != null) 
        {
			itemC.Use(slotIndex);
		}
        else
        {
            InventoryCursor.Instance.AddToCursor(item, slotIndex);
        }
    }
}
