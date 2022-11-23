using UnityEngine;

[CreateAssetMenu(fileName = "New Equpment", menuName = "Inventory/Equpment")]

public class Equipment : Item
{
    [Header("Equipment Item")]
    [Space]
    public EquipmentSlot equipSlot;

    public int armorModifier;

    public int damageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentMenager.Instance.Equip(this);
        RemoveFromInventory();
    }
}
public enum EquipmentSlot { Sword, Bow, Arrow, Ring, Armor, Helmet, Belt, Greaves, Boots, Potion}