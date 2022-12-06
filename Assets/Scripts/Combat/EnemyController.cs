using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MonoBehaviour
{
	public float PatrolTime = 15f;
	public float PotrolRange = 5f;
	public float AggroRange = 10f;
	[SerializeField] private int minimalDisnance = 1;
	[SerializeField] private Vector3 waypointPosition;
	[SerializeField] private Vector3 defaultPosition;
	[SerializeField] private Transform player;
	[SerializeField] private EnemyCombat enemyCombat;
	[SerializeField] private PlayerStats playerStats;
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private Transform enemyTransform;
	[SerializeField] private bool isAggressiv;
	private int index;
	private float currentSpeed;
	private float agentSpeed;
	private bool canAttack;
	private bool wasAttaked;
	private void Awake()
	{
		//TODO : Animation
		defaultPosition = transform.position;
		if (agent != null) { agentSpeed = agent.speed; }

		InvokeRepeating("Patrol", 0, PatrolTime);

		InvokeRepeating("Tick", 0, 0.1f);
	}


	private void Patrol()
	{
		var radnomPostionX = Random.Range(-PotrolRange, PotrolRange);
		var radnomPostionZ = Random.Range(-PotrolRange, PotrolRange);
		if (radnomPostionX < minimalDisnance && radnomPostionX > -minimalDisnance)
		{
			int randomINT = Random.Range(-minimalDisnance, minimalDisnance);
			radnomPostionX = randomINT;
		}
		if (radnomPostionZ < minimalDisnance && radnomPostionZ > -minimalDisnance)
		{
			int randomINT = Random.Range(-minimalDisnance, minimalDisnance);
			radnomPostionZ = randomINT;
		}
		waypointPosition = new Vector3(
				defaultPosition.x + radnomPostionX,
				transform.position.y,
				defaultPosition.z + radnomPostionZ);
	}

	private void Tick()
	{
		var distance = Vector3.Distance(player.transform.position, enemyTransform.position);

		if (distance <= AggroRange)
			canAttack = true;
		else
			canAttack = false;

		if ((wasAttaked || isAggressiv) && canAttack)
		{
			agent.destination = player.position;
			enemyCombat.Attack(playerStats);
		}
		else
		{
			agent.destination = waypointPosition;
		}
	}

	private void OnDrawGizmosSelected()
	{

		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, PotrolRange);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, AggroRange);
	}

}
