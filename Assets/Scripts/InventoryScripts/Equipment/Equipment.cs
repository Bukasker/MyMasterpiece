using UnityEngine;

[CreateAssetMenu(fileName = "New Equpment", menuName = "Inventory/Equpment")]

public class Equipment : Item
{
    [Header("Equipment Item")]
    [Space]
    public EquipmentType equipType;

	public ToolType toolType;
	public float digingValue;

	public int armorModifier;

    public int attackDamageModifier;
	public int arrowDamageModifier;
	public int magicDamageModifier;

}
public enum EquipmentType 
{ 
    Sword,
    Bow,
    Arrow,
    Ring,
    Armor,
    Helmet,
    Belt,
    Greaves,
    Boots,
    Potion
}


public enum ToolType
{
    None,
	Axe,
	Pickaxe,
	Hoe,
	Scyle,
	FishingRod,
}