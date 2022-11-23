using UnityEngine;
using UnityEngine.UI;
public class EquipmentSlotUI : MonoBehaviour
{
    [SerializeField] private Sprite basicIcon;
    [SerializeField] private Image icon;
    public Item item;

    public void EquipItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.Icon;
    }
    public void Unequip()
    {
        item = null;

        icon.sprite = basicIcon;
    }
}
