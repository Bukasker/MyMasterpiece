using UnityEngine;


[CreateAssetMenu(fileName = "New Mini Quest KillEnemy", menuName = "Quest/MiniQuestKillEnemy")]
public class MissionKillEnemy : QuestSettings
{
	public Enemy.EnemyTypes EnemyTypes;
	public int AmountEnemyToKill;
	public int AmountOfKilledEnemies;
}
