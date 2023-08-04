using UnityEngine;
using UnityEngine.UI;

public class ToolBarMenager : MonoBehaviour
{
	[Header("Slots")]
	public EquipmentSlotUI CurrentSlot;
	[SerializeField] private EquipmentSlotUI[] Slots;
	private int ScrollIndex = 0;
	private int oldIndex;

	[Header("Keybinds")]
	[SerializeField] private KeyCode key0 = KeyCode.Alpha0;
	[SerializeField] private KeyCode key1 = KeyCode.Alpha1;
	[SerializeField] private KeyCode key2 = KeyCode.Alpha2;
	[SerializeField] private KeyCode key3 = KeyCode.Alpha3;
	[SerializeField] private KeyCode key4 = KeyCode.Alpha4;
	[SerializeField] private KeyCode key5 = KeyCode.Alpha5;
	[SerializeField] private KeyCode key6 = KeyCode.Alpha6;
	[SerializeField] private KeyCode key7 = KeyCode.Alpha7;
	[SerializeField] private KeyCode key8 = KeyCode.Alpha8;
	[SerializeField] private KeyCode key9 = KeyCode.Alpha9;
	[SerializeField] private KeyCode keyMinus = KeyCode.Exclaim;
	[SerializeField] private KeyCode keyEquals = KeyCode.DoubleQuote;

	#region Singleton

	public static ToolBarMenager Instance { get; private set; }
	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
	}
	#endregion

	public void Update()
	{
		if (Input.GetKeyDown(key1))
			changeColor(0);
		if (Input.GetKeyDown(key2))
			changeColor(1);
		if (Input.GetKeyDown(key3))
			changeColor(2);
		if (Input.GetKeyDown(key4))
			changeColor(3);
		if (Input.GetKeyDown(key5))
			changeColor(4);
		if (Input.GetKeyDown(key6))
			changeColor(5);
		if (Input.GetKeyDown(key7))
			changeColor(6);
		if (Input.GetKeyDown(key8))
			changeColor(7);
		if (Input.GetKeyDown(key9))
			changeColor(8);
		if (Input.GetKeyDown(key0))
			changeColor(9);
		if (Input.GetKeyDown(keyMinus))
			changeColor(10);
		if (Input.GetKeyDown(keyEquals))
			changeColor(11);


		float scrollInput = Input.GetAxis("Mouse ScrollWheel");

		if (scrollInput > 0f)
		{
			ScrollIndex++;
			if(ScrollIndex > 11)
			{
				ScrollIndex = 0;
			}
		}
		else if (scrollInput < 0f)
		{
			ScrollIndex--;
			if (ScrollIndex < 0)
			{
				ScrollIndex = 11;
			}
		}

		if (ScrollIndex != oldIndex)
		{
			changeColor(ScrollIndex);
		}
	}
	private void changeColor(int index)
	{
		var imageOld = Slots[oldIndex].GetComponent<Image>();
		imageOld.color = Color.white;

		oldIndex = index;
		ScrollIndex = index;

		CurrentSlot = Slots[index];
		var image = CurrentSlot.GetComponent<Image>();
		image.color = Color.red;
	}
}
