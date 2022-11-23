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
        //_slider = _sliderGameObject.GetComponent<Slider>();
        currentHealth = MaxHealth;
        //_slider.maxValue = MaxHealth;
       // _slider.minValue = MinHealth;
       // _slider.value = MaxHealth;
    }
    public override void TakeDamage(int damage)
    {
        damage = damage - armor.GetValue() - basicArmorPenetraiton;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        _slider.value = currentHealth;

        if (gameObject.name != "Player" && currentHealth < MaxHealth)
        {
            _sliderGameObject.SetActive(true);
        }
        else if (gameObject.name != "Player")
        {
            _sliderGameObject.SetActive(false);
        }

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
            armor.AddMofifier(oldItem.armorModifier);
            damage.AddMofifier(oldItem.damageModifier);
        }
    }
}
