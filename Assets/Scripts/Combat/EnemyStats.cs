using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats
{
	[Space]
	[SerializeField] private EnemyController enemyController;
	[SerializeField] private PlayerStats playerStats;
	[SerializeField] private GameObject player;
	private void Awake()
	{
		currentHealth = MaxHealth;
	//	sliderGameObject.SetActive(false);
		//slider.maxValue = MaxHealth;
		//slider.minValue = MinHealth;
		//slider.value = MaxHealth;
	}
	public override void TakeDamage(int damage)
	{
		var LvlDiff = Lvl -playerStats.Lvl;
		damage = damage - (playerStats.basicArmorPenetraiton * ((2 * LvlDiff) / 3)) - armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		currentHealth -= damage;
		//slider.value = currentHealth;

		Debug.Log(transform.name + " takes " + damage + " damage.");
		/*
		if (currentHealth < MaxHealth)
		{
			sliderGameObject.SetActive(true);
		}
		else
		{
			sliderGameObject.SetActive(false);
		}
		*/
		if (currentHealth <= 0)
		{
			Die();
		}
		base.TakeDamage(damage);
	}
	public override void Die()
	{
		base.Die();

		enemyController = GetComponent<EnemyController>();
		// _enemyController.isDead = true;
	}
}
