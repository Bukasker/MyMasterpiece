using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [Header("Basic stats")]
    public int MinHealth = 0;
    public int MaxHealth = 100;
    public int Lvl = 1;
    public int basicArmorPenetraiton = 1;
    public float currentHealth;

    [Header("Dmg calculations")]
    public Stat AttackDamage;
	public Stat ArrowDamage;
	public Stat MagicDamage;
	public Stat armor;

    [Header("Healh bar slider")]
    [SerializeField] public GameObject sliderGameObject;
    [SerializeField] public Slider slider;

    public virtual void TakeDamage(int damage)
    {

    }
    public virtual void Die()
    {
        //Character is dead
    }

}
