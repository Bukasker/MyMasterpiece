using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public class DirtDig : MonoBehaviour
{

	[SerializeField] private GameObject Left;
	[SerializeField] private GameObject Up;
	[SerializeField] private GameObject Right;
	[SerializeField] private GameObject Down;

	[SerializeField] private GameObject LeftDownRightFull;
	[SerializeField] private GameObject LeftUpRightFull;
	[SerializeField] private GameObject LeftDownUpFull;
	[SerializeField] private GameObject DownUpRightFull;

	[SerializeField] private GameObject RightUpFullTrimed;
	[SerializeField] private GameObject LeftDownFullTrimed;
	[SerializeField] private GameObject RightDownFullTrimed;
	[SerializeField] private GameObject LeftUpFullTrimed;

	[SerializeField] private GameObject LeftUpFull;
	[SerializeField] private GameObject RightUpFull;
	[SerializeField] private GameObject RightDownFull;
	[SerializeField] private GameObject LeftDownFull;

	[SerializeField] private GameObject LeftUpTrim;
	[SerializeField] private GameObject RightUpTrim;
	[SerializeField] private GameObject RightDownTrim;
	[SerializeField] private GameObject LeftDownTrim;

	[SerializeField] private GameObject LeftRightUpTrim;
	[SerializeField] private GameObject RightUpDownTrim;
	[SerializeField] private GameObject LeftRightDownTrim;
	[SerializeField] private GameObject LeftUpDownTrim;

	[SerializeField] private GameObject DownUpTrim;
	[SerializeField] private GameObject LeftRightTrim;
	[SerializeField] private GameObject CenterTrim;
	[SerializeField] private GameObject CenterFull;
	[SerializeField] private GameObject FullTrim;

	[SerializeField] private GameObject ChoosenGameObject;
	[SerializeField] private GameObject targetObject;

	[SerializeField] private GameObject targetObjectAround2;
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

		DigedDic.Add(position, true);

		CheckNeightbours(position);
		targetObjectAround2 = targetObject;
		UpdateTilesAroud(position);
		Destroy(targetObjectAround2);
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
				//CheckNeightbours(leftPos);
				Destroy(targetObject);
			}
		}

		if (CheckDictionaryForKeyWithTrue(rightPos))
		{
			if (TileMapData.GridDicGameObject.TryGetValue(rightPos, out targetObjectAround))
			{
				targetObject = targetObjectAround;
				//CheckNeightbours(rightPos);
				Destroy(targetObject);
			}
		}
	/*
		if (CheckDictionaryForKeyWithTrue(upPos))
			CheckNeightbours(upPos);
		if (CheckDictionaryForKeyWithTrue(downPos))
			CheckNeightbours(downPos);

		if (CheckDictionaryForKeyWithTrue(leftUpPos))
			CheckNeightbours(leftUpPos);
		if (CheckDictionaryForKeyWithTrue(rightUpPos))
			CheckNeightbours(rightUpPos);
		if (CheckDictionaryForKeyWithTrue(leftDownPos))
			CheckNeightbours(leftDownPos);
		if (CheckDictionaryForKeyWithTrue(rightDownPos))
			CheckNeightbours(rightDownPos);
		*/
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


		if (hasLeft && !hasUp && !hasRight && !hasDown)
			ChoosenGameObject = Left;
		if (!hasLeft && hasUp && !hasRight && !hasDown)
			ChoosenGameObject = Up;
		if (!hasLeft && !hasUp && hasRight && !hasDown)
			ChoosenGameObject = Right;
		if (!hasLeft && !hasUp && !hasRight && hasDown)
			ChoosenGameObject = Down;

		if (hasLeft && !hasUp && hasRight && hasDown  && hasRightDownCorner && hasLeftDownCorner)
			ChoosenGameObject = LeftDownRightFull;
		if (hasLeft && hasUp && hasRight && !hasDown && hasLeftUpCorner && hasRightUpCorner)
			ChoosenGameObject = LeftUpRightFull;
		if (hasLeft && hasUp && !hasRight && hasDown && hasLeftUpCorner && hasLeftDownCorner)
			ChoosenGameObject = LeftDownUpFull;
		if (!hasLeft && hasUp && hasRight && hasDown  && hasRightUpCorner && hasRightDownCorner)
			ChoosenGameObject = DownUpRightFull;

		if (hasLeft && hasUp && hasRight && hasDown && hasLeftUpCorner && !hasRightUpCorner && hasRightDownCorner && hasLeftDownCorner)
			ChoosenGameObject = RightUpFullTrimed;
		if (hasLeft && hasUp && hasRight && hasDown && hasLeftUpCorner && hasRightUpCorner && hasRightDownCorner && !hasLeftDownCorner)
			ChoosenGameObject = LeftDownFullTrimed;
		if (hasLeft && hasUp && hasRight && hasDown && hasLeftUpCorner && hasRightUpCorner && !hasRightDownCorner && hasLeftDownCorner)
			ChoosenGameObject = RightDownFullTrimed;
		if (hasLeft && hasUp && hasRight && hasDown && !hasLeftUpCorner && hasRightUpCorner && hasRightDownCorner && hasLeftDownCorner)
			ChoosenGameObject = LeftUpFullTrimed;

		if (hasLeft && hasUp && !hasRight && !hasDown && hasLeftUpCorner)
			ChoosenGameObject = LeftUpFull;
		if (!hasLeft && hasUp && hasRight && !hasDown && hasRightUpCorner)
			ChoosenGameObject = RightUpFull;
		if (!hasLeft && !hasUp && hasRight && hasDown && hasRightDownCorner)
			ChoosenGameObject = RightDownFull;
		if (hasLeft && !hasUp && !hasRight && hasDown && hasLeftDownCorner)
			ChoosenGameObject = LeftDownFull;

		if (hasLeft && hasUp && !hasRight && !hasDown && !hasLeftUpCorner)
			ChoosenGameObject = LeftUpTrim;
		if (!hasLeft && hasUp && hasRight && !hasDown && !hasRightUpCorner)
			ChoosenGameObject = RightUpTrim;
		if (!hasLeft && !hasUp && hasRight && hasDown && !hasRightDownCorner)
			ChoosenGameObject = RightDownTrim;
		if (hasLeft && !hasUp && !hasRight && hasDown && !hasLeftDownCorner)
			ChoosenGameObject = LeftDownTrim;

		if (hasLeft && hasUp && hasRight && !hasDown && !hasLeftUpCorner && !hasRightUpCorner)
			ChoosenGameObject = LeftRightUpTrim;
		if (!hasLeft && hasUp && hasRight && hasDown && !hasRightUpCorner && !hasRightDownCorner)
			ChoosenGameObject = RightUpDownTrim;
		if (hasLeft && !hasUp && hasRight && hasDown && !hasRightDownCorner && !hasLeftDownCorner)
			ChoosenGameObject = LeftRightDownTrim;
		if (hasLeft && hasUp && !hasRight && hasDown && !hasLeftDownCorner && !hasLeftUpCorner)
			ChoosenGameObject = LeftUpDownTrim;

		if (!hasLeft && hasUp && !hasRight && hasDown)
			ChoosenGameObject = DownUpTrim;
		if (hasLeft && !hasUp && hasRight && !hasDown)
			ChoosenGameObject = LeftRightTrim;
		if (!hasLeft && !hasUp && !hasRight && !hasDown)
			ChoosenGameObject = CenterTrim;
		if (hasLeft && hasUp && hasRight && hasDown && hasLeftUpCorner && hasRightUpCorner && hasRightDownCorner && hasLeftDownCorner)
			ChoosenGameObject = CenterFull;
		if (hasLeft && hasUp && hasRight && hasDown && !hasLeftUpCorner && !hasRightUpCorner && !hasRightDownCorner && !hasLeftDownCorner)
			ChoosenGameObject = FullTrim;

		Instantiate(ChoosenGameObject, postion, new Quaternion(0,0,0,0));
		//Destroy(targetObject);
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
