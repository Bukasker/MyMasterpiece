public class EnemyCombat : CharacterCombat
{
	public override void Attack(CharacterStats targesStats)
	{
		if (attackCooldown <= 0f)
		{
			targesStats.TakeDamage(myStats.AttackDamage.GetValue());
			targesStats.TakeDamage(myStats.ArrowDamage.GetValue());
			targesStats.TakeDamage(myStats.MagicDamage.GetValue());
			attackCooldown = 1f / attackSpeed;
		}
	}
}
