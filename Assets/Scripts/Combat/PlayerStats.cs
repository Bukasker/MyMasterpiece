using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [Space]
    public int Gold;
    void Start()
    {
        EquipmentMenager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }
    private void Awake()
    {
        currentHealth = MaxHealth;
        if(slider != null)
        {
			slider.maxValue = MaxHealth;
			slider.minValue = MinHealth;
			slider.value = MaxHealth;
		}
    }
    public override void TakeDamage(int damage)
    {
        damage = (armor.GetValue() - basicArmorPenetraiton) + damage;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        //slider.value = currentHealth;


        if (currentHealth <= 0)
        {
            Die();
        }
        base.TakeDamage(damage);
    }
	void OnEquipmentChanged(Item newItem, Item oldItem)
	{
		if (newItem is Equipment)
		{
			Equipment newEquipment = newItem as Equipment;
			armor.AddMofifier(newEquipment.armorModifier);
			AttackDamage.AddMofifier(newEquipment.attackDamageModifier);
			ArrowDamage.AddMofifier(newEquipment.arrowDamageModifier);
			MagicDamage.AddMofifier(newEquipment.magicDamageModifier);
		}

		if (oldItem is Equipment)
		{
			Equipment oldEquipment = oldItem as Equipment;
			armor.RemoveMofifier(oldEquipment.armorModifier);
			AttackDamage.RemoveMofifier(oldEquipment.attackDamageModifier);
			ArrowDamage.RemoveMofifier(oldEquipment.arrowDamageModifier);
			MagicDamage.RemoveMofifier(oldEquipment.magicDamageModifier);
		}
	}
}
