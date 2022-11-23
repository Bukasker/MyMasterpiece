using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
	public combatState CombatState;
	EquipmentMenager equipmentMenager;

	[Header("Keybinds")]
	[SerializeField] private KeyCode normalAttackButton = KeyCode.Mouse0;
	[SerializeField] private KeyCode strongAttackButton = KeyCode.Mouse1;
	[SerializeField] private KeyCode drawSword = KeyCode.Keypad1;
	[SerializeField] private KeyCode drawBow = KeyCode.Keypad2;
	[SerializeField] private KeyCode drawSpell = KeyCode.Keypad3;

	private bool isSwordDrawed = false;
	private bool isBowDrawed = false;
	private bool isMagicDrawed =false;
	private void Awake()
	{
		equipmentMenager = EquipmentMenager.instance;
		CombatState = combatState.Idle;
	}
	private void Update()
	{
		if(Input.GetKeyDown(drawSword) && equipmentMenager._currentEquipment[0] != null)
		{
			isSwordDrawed = !isSwordDrawed;
			CombatState = combatState.Sword;
		}
		if (Input.GetKeyDown(drawBow) && equipmentMenager._currentEquipment[1] != null)
		{
			isBowDrawed = !isBowDrawed;
			CombatState = combatState.Bow;
		}
		if(Input.GetKeyDown(drawSword)  && equipmentMenager._currentEquipment[0] == null)
		{
			CombatState= combatState.Fists;
		}
		if (Input.GetKeyDown(drawBow) && equipmentMenager._currentEquipment[1] == null)
		{
			CombatState = combatState.Fists;
		}
		if (Input.GetKeyDown(drawSpell))
		{
			//TODO
			CombatState = combatState.Magic;
		}
		if (Input.GetKeyDown(normalAttackButton))
		{
			if(CombatState == combatState.Fists)
			{
				//TODO NAPIERDLANIE PROSTEGO (wywo³uje funkcie która robi animacje która towrzy sfere która zadaje obra¿enia)
			}
			if(CombatState == combatState.Sword)
			{
				//TODO NAPIERDLANIE MIECZEM
			}
			if(CombatState == combatState.Bow)
			{
				//TODO NAPIERDALNIE Z £UKU
			}
		}
		if (Input.GetKeyDown(strongAttackButton))
		{
			if (CombatState == combatState.Fists)
			{
				//TODO MODZNE NAPIERDLANIE SIERPA (wywo³uje funkcie która robi animacje która towrzy sfere która zadaje obra¿enia)
			}
			if (CombatState == combatState.Sword)
			{
				//TODO MODZNE NAPIERDLANIE MIECZEM
			}
			if (CombatState == combatState.Bow)
			{
				//TODO CHUJ WIE 
			}
		}
	}
	public enum combatState
	{
		Idle,
		Fists,
		Sword,
		Bow,
		Magic
	}
}
