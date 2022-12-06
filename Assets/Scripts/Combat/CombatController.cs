using UnityEngine;

public class CombatController : MonoBehaviour
{
	public CombatState CombatState { get; private set; }
	[SerializeField] private EquipmentMenager equipmentMenager;

	[Header("Keybinds")]
	[SerializeField] private KeyCode leftMouseButton = KeyCode.Mouse0;
	[SerializeField] private KeyCode rightMouseButton = KeyCode.Mouse1;
	[SerializeField] private KeyCode drawSword = KeyCode.Keypad1;
	[SerializeField] private KeyCode drawBow = KeyCode.Keypad2;
	[SerializeField] private KeyCode drawSpell = KeyCode.Keypad3;

	[SerializeField] private Animator combatAnimatior;
	[SerializeField] private AimController aimController;

	private bool isSwordDrawed = false;
	private bool isBowDrawed = false;
	private bool isMagicDrawed =false;
	private void Awake()
	{
		CombatState = CombatState.Idle;
	}
	private void Update()
	{
		if(Input.GetKeyDown(drawSword) && equipmentMenager.currentEquipment[0] != null)
		{
			isSwordDrawed = !isSwordDrawed;
			isBowDrawed = false;
			if (isSwordDrawed)
			{
				CombatState = CombatState.Sword;
			}
		}
		if (Input.GetKeyDown(drawBow) && equipmentMenager.currentEquipment[1] != null)
		{
			isBowDrawed = !isBowDrawed;
			isSwordDrawed = false;
			CombatState = CombatState.Bow;
		}
		if(Input.GetKeyDown(drawSword)  && (equipmentMenager.currentEquipment[0] == null || !isSwordDrawed))
		{

			CombatState = CombatState.Fists;
			Debug.Log(CombatState.Fists);
		}
		if (Input.GetKeyDown(drawBow) && equipmentMenager.currentEquipment[1] == null)
		{
			isBowDrawed = false;
			isSwordDrawed = false;
			CombatState = CombatState.Fists;
			Debug.Log(CombatState.Fists);
		}
		if (Input.GetKeyDown(drawSpell))
		{
			//TODO
			CombatState = CombatState.Magic;
		}
		if (Input.GetMouseButtonDown(0))
		{
			if(CombatState == CombatState.Fists || CombatState == CombatState.Idle)
			{
				combatAnimatior.SetTrigger("OnLeftMouseClick");
			}
			if(CombatState == CombatState.Sword)
			{
				combatAnimatior.SetTrigger("OnLeftMouseClick");
			}
			if(CombatState == CombatState.Bow)
			{
			//	aimController.StartCoroutine(aimController.StartAim());
			}
		}
		if (Input.GetKeyDown(rightMouseButton))
		{
			if (CombatState == CombatState.Fists)
			{
				//TODO MODZNE NAPIERDLANIE SIERPA (wywo³uje funkcie która robi animacje która towrzy sfere która zadaje obra¿enia)
			}
			if (CombatState == CombatState.Sword)
			{
				//TODO MODZNE NAPIERDLANIE MIECZEM
			}
			if (CombatState == CombatState.Bow)
			{
				//TODO CHUJ WIE 
			}
		}
	}
}
