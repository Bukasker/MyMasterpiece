using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : CharacterStats
{
    [Space]
    EnemyController _enemyController;
    PlayerStats _playerStats;
    GameObject _player;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerStats = _player.GetComponent<PlayerStats>();
        _slider = _sliderGameObject.GetComponent<Slider>();
        _sliderGameObject.SetActive(false);
        currentHealth = MaxHealth;
        _slider.maxValue = MaxHealth;
        _slider.minValue = MinHealth;
        _slider.value = MaxHealth;
    }
    public override void TakeDamage(int damage)
    {
        var LvlDiff = _playerStats.Lvl - this.Lvl;
        damage = damage - (armor.GetValue() * ((2 * LvlDiff) / 3)) - basicArmorPenetraiton;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        _slider.value = currentHealth;

        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth < MaxHealth)
        {
            _sliderGameObject.SetActive(true);
        }
        else
        {
            _sliderGameObject.SetActive(false);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();

        _enemyController = GetComponent<EnemyController>();
       // _enemyController.isDead = true;
    }
}
