using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _gizmosScale;


    public GameObject _player;
    public CharacterStats myStats;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        myStats = GetComponent<CharacterStats>();
    }

    public void Interact()
    {

        CharacterCombat playerCombat = _player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);//deal dmg to enemy
		}
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _gizmosScale);
    }
}
