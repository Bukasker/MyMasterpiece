using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public class DirtDig : MonoBehaviour
{

	#region materials
	[Header("Materials")]
	[SerializeField] private Material Left;
	[SerializeField] private Material Up;
	[SerializeField] private Material Right;
	[SerializeField] private Material Down;

	[SerializeField] private Material LeftDownRightFull;
	[SerializeField] private Material LeftUpRightFull;
	[SerializeField] private Material LeftDownUpFull;
	[SerializeField] private Material DownUpRightFull;

	[SerializeField] private Material RightUpFullTrimed;
	[SerializeField] private Material LeftDownFullTrimed;
	[SerializeField] private Material RightDownFullTrimed;
	[SerializeField] private Material LeftUpFullTrimed;

	[SerializeField] private Material LeftUpFull;
	[SerializeField] private Material RightUpFull;
	[SerializeField] private Material RightDownFull;
	[SerializeField] private Material LeftDownFull;

	[SerializeField] private Material LeftUpTrim;
	[SerializeField] private Material RightUpTrim;
	[SerializeField] private Material RightDownTrim;
	[SerializeField] private Material LeftDownTrim;

	[SerializeField] private Material DownUpTrim;
	[SerializeField] private Material LeftRightTrim;
	[SerializeField] private Material CenterTrim;
	[SerializeField] private Material CenterFull;

	[SerializeField] private Material DefaultDirt;
	#endregion

	[Header("Script Objects")]
	[SerializeField] private Material ChoosenMaterial;
	[SerializeField] private GameObject targetObject;
	[SerializeField] private GameObject targetObjectAround;

	[SerializeField] private TileMapData tileMapData;

	public static Dictionary<Vector3, bool> DigedDic = new Dictionary<Vector3, bool>();
	public static Dictionary<Vector3, bool> WaterdDic = new Dictionary<Vector3, bool>();


	public bool hasLeft = false;
	public bool hasRight = false;
	public bool hasUp = false;
	public bool hasDown = false;

	public bool hasLeftUpCorner = false;
	public bool hasRightUpCorner = false;
	public bool hasLeftDownCorner = false;
	public bool hasRightDownCorner = false;

	[SerializeField] private float blockSize = 1.6f;


	public void AddDigedTile(Vector3 position, GameObject selectedTile)
	{
		targetObject = selectedTile;

		// Zaokr¹glij liczby w position do 2 miejsc po przecinku
		position.x = (float)Math.Round(position.x, 2);
		position.y = (float)Math.Round(position.y, 2);
		position.z = (float)Math.Round(position.z, 2);

		if (!DigedDic.ContainsKey(position))
		{
			if (!CheckDictionaryForKeyWithTrue(position))
				DigedDic.Add(position, true);
		}

		CheckNeightbours(position);
		UpdateTilesAroud(position);

	}
	public void RemoveDigedTile(Vector3 position, GameObject selectedTile)
	{
		targetObject = selectedTile;

		// Zaokr¹glij liczby w position do 2 miejsc po przecinku
		position.x = (float)Math.Round(position.x, 2);
		position.y = (float)Math.Round(position.y, 2);
		position.z = (float)Math.Round(position.z, 2);

		if (DigedDic.ContainsKey(position))
		{
			if (CheckDictionaryForKeyWithTrue(position))
			{
				bool value = true;
				DigedDic.Remove(position, out value);
			}
		}
		ChoosenMaterial = DefaultDirt;
		CheckNeightbours(position);
		UpdateTilesAroud(position);

	}
	public void AddWateredTile(Vector3 position, GameObject selectedTile)
	{
		/*
		targetObject = selectedTile;

		// Zaokr¹glij liczby w position do 2 miejsc po przecinku
		position.x = (float)Math.Round(position.x, 2);
		position.y = (float)Math.Round(position.y, 2);
		position.z = (float)Math.Round(position.z, 2);

		if (!DigedDic.ContainsKey(position))
		{
			if (!CheckDictionaryForKeyWithTrue(position))
				DigedDic.Add(position, true);
		}

		CheckNeightbours(position);
		UpdateTilesAroud(position);
		*/
	}

	public void UpdateTilesAroud(Vector3 position)
	{
		Vector3 leftVec = position + new Vector3(-blockSize, 0, 0);
		Vector3 rightVec = position + new Vector3(blockSize, 0, 0);
		Vector3 upVec = position + new Vector3(0, 0, blockSize);
		Vector3 downVec = position + new Vector3(0, 0, -blockSize);

		Vector3 leftUpVec = position + new Vector3(-blockSize, 0, blockSize);
		Vector3 rightUpVec = position + new Vector3(blockSize, 0, blockSize);
		Vector3 leftDownVec = position + new Vector3(-blockSize, 0, -blockSize);
		Vector3 rightDownVec = position + new Vector3(blockSize, 0, -blockSize);

		var leftPos = roundPosition(leftVec);
		var rightPos = roundPosition(rightVec);
		var upPos = roundPosition(upVec);
		var downPos = roundPosition(downVec);

		var leftUpPos = roundPosition(leftUpVec);
		var rightUpPos = roundPosition(rightUpVec);
		var leftDownPos = roundPosition(leftDownVec);
		var rightDownPos = roundPosition(rightDownVec);

		if (CheckDictionaryForKeyWithTrue(leftPos))
		{
			if (TileMapData.GridDicGameObject.TryGetValue(leftPos, out targetObjectAround))
			{
				targetObject = targetObjectAround;
				CheckNeightbours(leftPos);
			}
		}
		if (CheckDictionaryForKeyWithTrue(rightPos))
		{
			if (TileMapData.GridDicGameObject.TryGetValue(rightPos, out targetObjectAround))
			{
				targetObject = targetObjectAround;
				CheckNeightbours(rightPos);
			}
		}
		if (CheckDictionaryForKeyWithTrue(upPos))
		{
			if (TileMapData.GridDicGameObject.TryGetValue(upPos, out targetObjectAround))
			{
				targetObject = targetObjectAround;
				CheckNeightbours(upPos);
			}
		}
		if (CheckDictionaryForKeyWithTrue(downPos))
		{
			if (TileMapData.GridDicGameObject.TryGetValue(downPos, out targetObjectAround))
			{
				targetObject = targetObjectAround;
				CheckNeightbours(downPos);
			}
		}



		if (CheckDictionaryForKeyWithTrue(leftUpPos))
		{
			if (TileMapData.GridDicGameObject.TryGetValue(leftUpPos, out targetObjectAround))
			{
				targetObject = targetObjectAround;
				CheckNeightbours(leftUpPos);
			}
		}
		if (CheckDictionaryForKeyWithTrue(rightUpPos))
		{
			if (TileMapData.GridDicGameObject.TryGetValue(rightUpPos, out targetObjectAround))
			{
				targetObject = targetObjectAround;
				CheckNeightbours(rightUpPos);
			}
		}
		if (CheckDictionaryForKeyWithTrue(leftDownPos))
		{
			if (TileMapData.GridDicGameObject.TryGetValue(leftDownPos, out targetObjectAround))
			{
				targetObject = targetObjectAround;
				CheckNeightbours(leftDownPos);
			}
		}
		if (CheckDictionaryForKeyWithTrue(rightDownPos))
		{
			if (TileMapData.GridDicGameObject.TryGetValue(rightDownPos, out targetObjectAround))
			{
				targetObject = targetObjectAround;
				CheckNeightbours(rightDownPos);
			}
		}
	}



	public void CheckNeightbours(Vector3 postion)
	{
		hasLeft = false;
		hasRight = false;
		hasUp = false;
		hasDown = false;

		hasLeftUpCorner = false;
		hasRightUpCorner = false;
		hasLeftDownCorner = false;
		hasRightDownCorner = false;

		Vector3 leftVec = postion + new Vector3(-blockSize, 0, 0);
		Vector3 rightVec = postion + new Vector3(blockSize, 0, 0);
		Vector3 upVec = postion + new Vector3(0, 0, blockSize);
		Vector3 downVec = postion + new Vector3(0, 0, -blockSize);

		Vector3 leftUpVec = postion + new Vector3(-blockSize, 0, blockSize);
		Vector3 rightUpVec = postion + new Vector3(blockSize, 0, blockSize);
		Vector3 leftDownVec = postion + new Vector3(-blockSize, 0, -blockSize);
		Vector3 rightDownVec = postion + new Vector3(blockSize, 0, -blockSize);

		var leftPos = roundPosition(leftVec);
		var rightPos = roundPosition(rightVec);
		var upPos = roundPosition(upVec);
		var downPos = roundPosition(downVec);

		var leftUpPos = roundPosition(leftUpVec);
		var rightUpPos = roundPosition(rightUpVec);
		var leftDownPos = roundPosition(leftDownVec);
		var rightDownPos = roundPosition(rightDownVec);


		hasLeft = DigedDic.ContainsKey(leftPos);
		hasRight = DigedDic.ContainsKey(rightPos);
		hasUp = DigedDic.ContainsKey(upPos);
		hasDown = DigedDic.ContainsKey(downPos);

		hasLeftUpCorner = DigedDic.ContainsKey(leftUpPos);
		hasRightUpCorner = DigedDic.ContainsKey(rightUpPos);
		hasLeftDownCorner = DigedDic.ContainsKey(leftDownPos);
		hasRightDownCorner = DigedDic.ContainsKey(rightDownPos);

		if (ChoosenMaterial != DefaultDirt)
		{
			if (hasLeft && !hasUp && !hasRight && !hasDown)
				ChoosenMaterial = Left;
			if (!hasLeft && hasUp && !hasRight && !hasDown)
				ChoosenMaterial = Up;
			if (!hasLeft && !hasUp && hasRight && !hasDown)
				ChoosenMaterial = Right;
			if (!hasLeft && !hasUp && !hasRight && hasDown)
				ChoosenMaterial = Down;

			if (hasLeft && !hasUp && hasRight && hasDown)
				ChoosenMaterial = LeftDownRightFull;
			if (hasLeft && hasUp && hasRight && !hasDown)
				ChoosenMaterial = LeftUpRightFull;
			if (hasLeft && hasUp && !hasRight && hasDown)
				ChoosenMaterial = LeftDownUpFull;
			if (!hasLeft && hasUp && hasRight && hasDown)
				ChoosenMaterial = DownUpRightFull;

			if (hasLeft && hasUp && hasRight && hasDown && !hasRightUpCorner)
				ChoosenMaterial = RightUpFullTrimed;
			if (hasLeft && hasUp && hasRight && hasDown && !hasLeftDownCorner)
				ChoosenMaterial = LeftDownFullTrimed;
			if (hasLeft && hasUp && hasRight && hasDown && !hasRightDownCorner)
				ChoosenMaterial = RightDownFullTrimed;
			if (hasLeft && hasUp && hasRight && hasDown && !hasLeftUpCorner)
				ChoosenMaterial = LeftUpFullTrimed;

			if (hasLeft && hasUp && !hasRight && !hasDown && hasLeftUpCorner)
				ChoosenMaterial = LeftUpFull;
			if (!hasLeft && hasUp && hasRight && !hasDown && hasRightUpCorner)
				ChoosenMaterial = RightUpFull;
			if (!hasLeft && !hasUp && hasRight && hasDown && hasRightDownCorner)
				ChoosenMaterial = RightDownFull;
			if (hasLeft && !hasUp && !hasRight && hasDown && hasLeftDownCorner)
				ChoosenMaterial = LeftDownFull;

			if (hasLeft && hasUp && !hasRight && !hasDown && !hasLeftUpCorner)
				ChoosenMaterial = LeftUpTrim;
			if (!hasLeft && hasUp && hasRight && !hasDown && !hasRightUpCorner)
				ChoosenMaterial = RightUpTrim;
			if (!hasLeft && !hasUp && hasRight && hasDown && !hasRightDownCorner)
				ChoosenMaterial = RightDownTrim;
			if (hasLeft && !hasUp && !hasRight && hasDown && !hasLeftDownCorner)
				ChoosenMaterial = LeftDownTrim;

			if (!hasLeft && hasUp && !hasRight && hasDown)
				ChoosenMaterial = DownUpTrim;
			if (hasLeft && !hasUp && hasRight && !hasDown)
				ChoosenMaterial = LeftRightTrim;
			if (!hasLeft && !hasUp && !hasRight && !hasDown)
				ChoosenMaterial = CenterTrim;
			if (hasLeft && hasUp && hasRight && hasDown)
				ChoosenMaterial = CenterFull;
		}

		targetObject.GetComponent<Renderer>().material = ChoosenMaterial;
		//Instantiate(ChoosenGameObject, postion, new Quaternion(0,0,0,0));
		//Destroy(targetObject);
		ChoosenMaterial = null;
	}

	private Vector3 roundPosition(Vector3 position)
	{
		Vector3 roundedPosition = new Vector3(
		(float)Math.Round(position.x, 2),
		(float)Math.Round(position.y, 2),
		(float)Math.Round(position.z, 2)
		);
		return roundedPosition;
	}
	bool CheckDictionaryForKeyWithTrue(Vector3 position)
	{
		bool value;
		if (DigedDic.TryGetValue(position, out value))
		{
			return value;
		}
		return false;
	}

}
