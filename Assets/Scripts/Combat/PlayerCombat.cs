using UnityEngine;

public class PlayerCombat : CharacterCombat
{
	[SerializeField] public CombatController CombatController;

	public override void Attack(CharacterStats targesStats)
	{
		switch (CombatController.CombatState)
		{
			case CombatState.Fists:
				targesStats.TakeDamage(5);
				break;
			case CombatState.Sword:
				targesStats.TakeDamage(myStats.AttackDamage.GetValue());
				break;
			case CombatState.Bow:
				targesStats.TakeDamage(myStats.ArrowDamage.GetValue());
				break;
			case CombatState.Magic:
				targesStats.TakeDamage(myStats.MagicDamage.GetValue());
				break;
		}
	}
}
