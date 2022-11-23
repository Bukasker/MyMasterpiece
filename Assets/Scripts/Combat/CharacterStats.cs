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
    public Stat damage;
    public Stat armor;

    [Header("Healh bar slider")]
    [SerializeField] public GameObject _sliderGameObject;
    [SerializeField] public Slider _slider;

    public virtual void TakeDamage(int damage)
    {

    }
    public virtual void Die()
    {
        //Character is dead
    }

}
