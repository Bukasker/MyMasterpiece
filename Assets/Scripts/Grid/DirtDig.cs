using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public class DirtDig : MonoBehaviour
{


	[Header("Diged Dirt GameObjects")]
	[SerializeField] private GameObject[] DigedDirtGameObject;

	[Header("Watered Dirt GameObjects")]
	[SerializeField] private GameObject[] WateredDirtGameObject;

	[Header("Script Objects")]
	[SerializeField] private GameObject ChoosenGameObject;
	[SerializeField] private GameObject targetObject;
	[SerializeField] private GameObject targetObjectAround;

	[SerializeField] private TileMapData tileMapData;

	public static Dictionary<Vector3, bool> DigedDic = new Dictionary<Vector3, bool>();
	public static Dictionary<Vector3, bool> WaterdDic = new Dictionary<Vector3, bool>();
	public static Dictionary<Vector3, GameObject> CreatedGameObjects = new Dictionary<Vector3, GameObject>();


	public bool hasLeft = false;
	public bool hasRight = false;
	public bool hasUp = false;
	public bool hasDown = false;

	public bool hasLeftUpCorner = false;
	public bool hasRightUpCorner = false;
	public bool hasLeftDownCorner = false;
	public bool hasRightDownCorner = false;

	[SerializeField] private float blockSize = 1.6f;

	private bool isDiged;
	private bool isWatered;
	

	public void AddDigedTile(Vector3 position, GameObject selectedTile)
	{
		isDiged = true;
		targetObject = selectedTile;

		// Zaokr¹glij liczby w position do 2 miejsc po przecinku
		position.x = (float)Math.Round(position.x, 2);
		position.y = (float)Math.Round(position.y, 2);
		position.z = (float)Math.Round(position.z, 2);

		if (!DigedDic.ContainsKey(position))
		{
			if (!CheckDictionaryForKeyWithTrueDiged(position))
				DigedDic.Add(position, true);
		}

		CheckNeightbours(position);
		UpdateTilesAroud(position);
		isDiged = false;

	}
	public void RemoveDigedTile(Vector3 position, GameObject selectedTile)
	{
		targetObject = selectedTile;

		// Zaokr¹glij liczby w position do 2 miejsc po przecinku
		position.x = (float)Math.Round(position.x, 2);
		position.y = (float)Math.Round(position.y, 2);
		position.z = (float)Math.Round(position.z, 2);
		GameObject foundGameObject;

		if (DigedDic.ContainsKey(position))
		{
			if (CheckDictionaryForKeyWithTrueDiged(position))
			{
				bool value = true;
				if (CreatedGameObjects.TryGetValue(position, out foundGameObject))
				{
					Destroy(foundGameObject);
				}
				DigedDic.Remove(position, out value);
			}
		}
		if (WaterdDic.ContainsKey(position))
		{
			if (CheckDictionaryForKeyWithTrueWatered(position))
			{
				bool value = true;
				if (CreatedGameObjects.TryGetValue(position, out foundGameObject))
				{
					Destroy(foundGameObject);
				}
				WaterdDic.Remove(position, out value);
			}
		}
		ChoosenGameObject = DigedDirtGameObject[24];
		isDiged = true;
		CheckNeightbours(position);
		UpdateTilesAroud(position);
		isWatered = true;
		CheckNeightbours(position);
		UpdateTilesAroud(position);
		isDiged = false;
		isWatered = false;

	}
	public void AddWateredTile(Vector3 position, GameObject selectedTile)
	{
		targetObject = selectedTile;

		// Zaokr¹glij liczby w position do 2 miejsc po przecinku
		position.x = (float)Math.Round(position.x, 2);
		position.y = (float)Math.Round(position.y, 2);
		position.z = (float)Math.Round(position.z, 2);

		if (!WaterdDic.ContainsKey(position))
		{
			if (!CheckDictionaryForKeyWithTrueWatered(position))
				WaterdDic.Add(position, true);
		}

		CheckNeightbours(position);
		UpdateTilesAroud(position);
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
		if (isDiged)
		{
			if (CheckDictionaryForKeyWithTrueDiged(leftPos))
			{
				if (TileMapData.GridDicGameObject.TryGetValue(leftPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(leftPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueDiged(rightPos))
			{
				if (TileMapData.GridDicGameObject.TryGetValue(rightPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(rightPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueDiged(upPos))
			{
				if (TileMapData.GridDicGameObject.TryGetValue(upPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(upPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueDiged(downPos))
			{
				if (TileMapData.GridDicGameObject.TryGetValue(downPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(downPos);
				}
			}



			if (CheckDictionaryForKeyWithTrueDiged(leftUpPos))
			{
				if (TileMapData.GridDicGameObject.TryGetValue(leftUpPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(leftUpPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueDiged(rightUpPos))
			{
				if (TileMapData.GridDicGameObject.TryGetValue(rightUpPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(rightUpPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueDiged(leftDownPos))
			{
				if (TileMapData.GridDicGameObject.TryGetValue(leftDownPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(leftDownPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueDiged(rightDownPos))
			{
				if (TileMapData.GridDicGameObject.TryGetValue(rightDownPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(rightDownPos);
				}
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


		hasLeft = isDiged ? DigedDic.ContainsKey(leftPos) : WaterdDic.ContainsKey(leftPos);
		hasRight = isDiged ? DigedDic.ContainsKey(rightPos) : WaterdDic.ContainsKey(rightPos);
		hasUp = isDiged ? DigedDic.ContainsKey(upPos) : WaterdDic.ContainsKey(upPos);
		hasDown = isDiged ? DigedDic.ContainsKey(downPos) : WaterdDic.ContainsKey(downPos);

		hasLeftUpCorner = isDiged ? DigedDic.ContainsKey(leftUpPos) : WaterdDic.ContainsKey(leftUpPos);
		hasRightUpCorner = isDiged ? DigedDic.ContainsKey(rightUpPos) : WaterdDic.ContainsKey(rightUpPos);
		hasLeftDownCorner = isDiged ? DigedDic.ContainsKey(leftDownPos) : WaterdDic.ContainsKey(leftDownPos);
		hasRightDownCorner = isDiged ? DigedDic.ContainsKey(rightDownPos) : WaterdDic.ContainsKey(rightDownPos);

		if (ChoosenGameObject != DigedDirtGameObject[24])
		{
			if (hasLeft && !hasUp && !hasRight && !hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[0] : WateredDirtGameObject[0];
			if (!hasLeft && hasUp && !hasRight && !hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[1] : WateredDirtGameObject[1];
			if (!hasLeft && !hasUp && hasRight && !hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[2] : WateredDirtGameObject[2];
			if (!hasLeft && !hasUp && !hasRight && hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[3] : WateredDirtGameObject[3];

			if (hasLeft && !hasUp && hasRight && hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[4] : WateredDirtGameObject[4];
			if (hasLeft && hasUp && hasRight && !hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[5] : WateredDirtGameObject[5];
			if (hasLeft && hasUp && !hasRight && hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[6] : WateredDirtGameObject[6];
			if (!hasLeft && hasUp && hasRight && hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[7] : WateredDirtGameObject[7];

			if (hasLeft && hasUp && hasRight && hasDown && !hasRightUpCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[8] : WateredDirtGameObject[8];
			if (hasLeft && hasUp && hasRight && hasDown && !hasLeftDownCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[9] : WateredDirtGameObject[9];
			if (hasLeft && hasUp && hasRight && hasDown && !hasRightDownCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[10] : WateredDirtGameObject[10];
			if (hasLeft && hasUp && hasRight && hasDown && !hasLeftUpCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[11] : WateredDirtGameObject[11];

			if (hasLeft && hasUp && !hasRight && !hasDown && hasLeftUpCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[12] : WateredDirtGameObject[12];
			if (!hasLeft && hasUp && hasRight && !hasDown && hasRightUpCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[13] : WateredDirtGameObject[13];
			if (!hasLeft && !hasUp && hasRight && hasDown && hasRightDownCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[14] : WateredDirtGameObject[14];
			if (hasLeft && !hasUp && !hasRight && hasDown && hasLeftDownCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[15] : WateredDirtGameObject[15];

			if (hasLeft && hasUp && !hasRight && !hasDown && !hasLeftUpCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[16] : WateredDirtGameObject[16];
			if (!hasLeft && hasUp && hasRight && !hasDown && !hasRightUpCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[17] : WateredDirtGameObject[17];
			if (!hasLeft && !hasUp && hasRight && hasDown && !hasRightDownCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[18] : WateredDirtGameObject[18];
			if (hasLeft && !hasUp && !hasRight && hasDown && !hasLeftDownCorner)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[19] : WateredDirtGameObject[19];

			if (!hasLeft && hasUp && !hasRight && hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[20] : WateredDirtGameObject[20];
			if (hasLeft && !hasUp && hasRight && !hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[21] : WateredDirtGameObject[21];
			if (!hasLeft && !hasUp && !hasRight && !hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[22] : WateredDirtGameObject[22];
			if (hasLeft && hasUp && hasRight && hasDown)
				ChoosenGameObject = isDiged ? DigedDirtGameObject[23] : WateredDirtGameObject[23];
		}


		if (isDiged)
		{
			CreatedGameObjects.Add(roundPosition(postion), Instantiate(ChoosenGameObject, new Vector3(postion.x, postion.y + 0.001f, postion.z), new Quaternion(0, 0, 0, 0)));
		}
		else if(isWatered)
		{
			CreatedGameObjects.Add(roundPosition(postion), Instantiate(ChoosenGameObject, new Vector3(postion.x, postion.y + 0.002f, postion.z), new Quaternion(0, 0, 0, 0)));
		}
		ChoosenGameObject = null;
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
	bool CheckDictionaryForKeyWithTrueDiged(Vector3 position)
	{
		bool value;
		if (DigedDic.TryGetValue(position, out value))
		{
			return value;
		}
		return false;
	}

	bool CheckDictionaryForKeyWithTrueWatered(Vector3 position)
	{
		bool value;
		if (WaterdDic.TryGetValue(position, out value))
		{
			return value;
		}
		return false;
	}

}
