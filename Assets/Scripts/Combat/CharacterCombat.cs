using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public CharacterStats myStats;
    public float attackSpeed = 1f;
    public float attackCooldown = 0f;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
    public void Attack(CharacterStats targesStats)
    {
		targesStats.TakeDamage(myStats.damage.GetValue());
	}

}
