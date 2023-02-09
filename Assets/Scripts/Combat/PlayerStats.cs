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
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddMofifier(newItem.armorModifier);
            AttackDamage.AddMofifier(newItem.attackDamageModifier);
			ArrowDamage.AddMofifier(newItem.arrowDamageModifier);
			MagicDamage.AddMofifier(newItem.magicDamageModifier);
		}
        if (oldItem != null)
        {
            armor.RemoveMofifier(oldItem.armorModifier);
			AttackDamage.RemoveMofifier(oldItem.attackDamageModifier);
			ArrowDamage.RemoveMofifier(oldItem.arrowDamageModifier);
			MagicDamage.RemoveMofifier(oldItem.magicDamageModifier);
		}
    }
}
