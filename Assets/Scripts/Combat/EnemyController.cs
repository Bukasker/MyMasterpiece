using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float PatrolTime = 15f;
    public float PotrolRange = 5f;
    public float AggroRange = 10f;
    [SerializeField] Vector3 _waypointPosition;
    int _index;
    float _currentSpeed;
    float _agentSpeed;
    Transform _player;
    public NavMeshAgent _agent;

    public Vector3 _defaultPosition;
    private void Awake()
    {
        _defaultPosition = this.GetComponent<Transform>().position;
        //TODO : Animation
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if(_agent != null) { _agentSpeed = _agent.speed; }

        InvokeRepeating("Tick", 0, 0.1f);

        InvokeRepeating("Patrol", 0, PatrolTime);
        
    }

    private void Patrol()
    {
        var radnomPostionX = Random.Range(-PotrolRange, PotrolRange);
        var radnomPostionZ = Random.Range(-PotrolRange, PotrolRange);
        _waypointPosition = new Vector3(_defaultPosition.x + radnomPostionX,
            gameObject.transform.position.y, _defaultPosition .z + radnomPostionZ).normalized;
    }

    private void Tick()
    {
        _agent.destination = _waypointPosition;
    }
    private void OnDrawGizmosSelected()
    {

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_defaultPosition, PotrolRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AggroRange);
    }
}
