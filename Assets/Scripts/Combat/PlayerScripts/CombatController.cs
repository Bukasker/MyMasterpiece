using UnityEngine;
using UnityEngine.Events;

public class CombatController : MonoBehaviour
{
	public CombatState CombatState { get; private set; }

	public Equipment equipedWeapon;
	[SerializeField] private EquipmentMenager equipmentMenager;

	[Header("Keybinds")]
	[SerializeField] private KeyCode leftMouseButton = KeyCode.Mouse0;
	[SerializeField] private KeyCode rightMouseButton = KeyCode.Mouse1;
	[SerializeField] private KeyCode drawSword = KeyCode.Keypad1;
	[SerializeField] private KeyCode drawBow = KeyCode.Keypad2;
	[SerializeField] private KeyCode drawSpell = KeyCode.Keypad3;

	[SerializeField] private Animator combatAnimatior;
	[SerializeField] private AimController aimController;

	[SerializeField] private UnityEvent aimEventStart;
	[SerializeField] private UnityEvent aimEventExit;


	private bool isSwordDrawed = false;
	private bool isBowDrawed = false;
	private bool isMagicDrawed = false;

	private void Awake()
	{
		CombatState = CombatState.Fists;
	}
	private void Update()
	{
		if (ToolBarMenager.Instance.CurrentSlot != null)
		{
			if (ToolBarMenager.Instance.CurrentSlot.item is Equipment)
			{
				equipedWeapon = (Equipment)ToolBarMenager.Instance.CurrentSlot.item;

				switch (equipedWeapon.equipType)
				{
					case EquipmentType.Sword:
						CombatState = CombatState.Sword;
						break;
					case EquipmentType.Bow:
						CombatState = CombatState.Bow;
						break;
				}

				if(ToolBarMenager.Instance.CurrentSlot.item == null)
				{
					CombatState = CombatState.Fists;
				}
			}
			else
			{
				CombatState = CombatState.Fists;
			}
		}
		else
		{
			CombatState = CombatState.Fists;
		}
	


		if (CombatState == CombatState.Fists)
		{
			isBowDrawed = false;
			isSwordDrawed = false;
			isMagicDrawed = false;
			aimEventExit.Invoke();
		}
		if (CombatState == CombatState.Sword)
		{
			isBowDrawed = false;
			isMagicDrawed = false;
			aimEventExit.Invoke();
		}
		if (CombatState == CombatState.Bow)
		{
			isSwordDrawed = false;
			isMagicDrawed = false;
			aimEventStart.Invoke();
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
