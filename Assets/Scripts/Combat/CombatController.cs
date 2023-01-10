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
		CombatState = CombatState.Fists;
	}
	private void Update()
	{
		if(Input.GetKeyDown(drawSword) && equipmentMenager.currentEquipment[0] != null)
		{
			isSwordDrawed = !isSwordDrawed;
			if (isSwordDrawed)
			{
				CombatState = CombatState.Sword;
			}
		}
		if (Input.GetKeyDown(drawBow) && equipmentMenager.currentEquipment[1] != null)
		{
			isBowDrawed = !isBowDrawed;
			if (isBowDrawed)
			{
				CombatState = CombatState.Bow;
			}
		}

		if(Input.GetKeyDown(drawSword)  && (equipmentMenager.currentEquipment[0] == null || !isSwordDrawed))
		{
			CombatState = CombatState.Fists;
		}
		if (Input.GetKeyDown(drawBow) && equipmentMenager.currentEquipment[1] == null || !isBowDrawed)
		{
			CombatState = CombatState.Fists;
		}


		if (Input.GetKeyDown(drawSpell))
		{
			//TODO
			CombatState = CombatState.Magic;
		}



		if (CombatState == CombatState.Fists)
		{
			isBowDrawed = false;
			isSwordDrawed = false;
			isMagicDrawed = false;
			aimController.StopCoroutine("startAim");
		}
		if (CombatState == CombatState.Sword)
		{
			isBowDrawed = false;
			isMagicDrawed = false;
			aimController.StopCoroutine("startAim");
		}
		if (CombatState == CombatState.Bow)
		{
			isSwordDrawed = false;
			isMagicDrawed = false;
			aimController.StartCoroutine("startAim");
		}
		if (CombatState == CombatState.Magic)
		{

		}


		if (Input.GetMouseButtonDown(0))
		{
			if(CombatState == CombatState.Fists)
			{
				combatAnimatior.SetTrigger("OnLeftMouseClick");
			}
			if(CombatState == CombatState.Sword)
			{
				combatAnimatior.SetTrigger("OnLeftMouseClick");
			}
			if(CombatState == CombatState.Bow)
			{
				aimController.Shoot();
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
