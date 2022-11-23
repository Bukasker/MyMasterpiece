using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [Space]
    [SerializeField] int Gold;
    void Start()
    {
        EquipmentMenager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }
    private void Awake()
    {
        currentHealth = MaxHealth;
        if(_slider != null)
        {
			_slider.maxValue = MaxHealth;
			_slider.minValue = MinHealth;
			_slider.value = MaxHealth;
		}
    }
    public override void TakeDamage(int damage)
    {
        damage = damage - armor.GetValue() - basicArmorPenetraiton;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        _slider.value = currentHealth;


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
            damage.AddMofifier(newItem.damageModifier);
        }
        if (oldItem != null)
        {
            armor.RemoveMofifier(oldItem.armorModifier);
            damage.RemoveMofifier(oldItem.damageModifier);
        }
    }
}
