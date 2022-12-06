using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    [Header("Item")]
    [Space]
    public string Name = "New Item";
    public ItemTypes ItemType;
    public Sprite Icon = null;
    public float Value = 0f;
    public float Weight = 0f;
    public int maxStack = 999;
    public int itemAmount = 1;

    public Mesh Mesh;
    public List<Material> Materials;

    [TextArea]
	public string Description = "Description placeholder";

	public virtual void Use()
    {
        Debug.Log("Using");
    }
	public void RemoveFromInventory()
	{
		Inventory.Instance.RemoveSlotItem(this);
	}
}
public enum ItemTypes
{
    Weapon,
    Apperance,
    Potion,
    Food,
    Book,
    Ingridiens,
    Key
}