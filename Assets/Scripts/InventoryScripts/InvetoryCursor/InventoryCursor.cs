using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryCursor : MonoBehaviour
{
	public Item CursorItem;
	public Image CursorIcon;

	public RectTransform cursorRectTransform;

	public int fromIndex;
	public int slotIndex;
	public static InventoryCursor Instance { get; private set; }

	private int iteration = 0;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	private void Update()
	{
		Vector2 cursorPosition = Input.mousePosition;

		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			cursorRectTransform.parent as RectTransform,
			cursorPosition,
			null,
			out Vector2 localCursorPosition);
		cursorRectTransform.localPosition = localCursorPosition;
	}

	public void AddToCursor(Item item, int index)
	{
		if(item != null)
		{
			if (iteration > 0)
			{
				fromIndex = index;
			}
			else
			{
				fromIndex = slotIndex;
			}
			slotIndex = index;	
			CursorItem = item;
			CursorIcon.sprite = item.Icon;
			CursorIcon.enabled = true;
			iteration++;
		}
	}
	public void RemoveFromCursor()
	{
		if(CursorItem.isToThrowAway)
		{
			CursorItem = null;
			CursorIcon.sprite = null;
			CursorIcon.enabled = false;
		}
	}


}
