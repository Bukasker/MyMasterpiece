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

    public static Dictionary<Vector3, GameObject> DigedDic = new Dictionary<Vector3, GameObject>();
    public static Dictionary<Vector3, GameObject> WaterdDic = new Dictionary<Vector3, GameObject>();

    public static Dictionary<Vector3, GameObject> ChangedDigedTile = new Dictionary<Vector3, GameObject>();
	public static Dictionary<Vector3, GameObject> ChangedWateredTile = new Dictionary<Vector3, GameObject>();

	public bool hasLeft = false;
    public bool hasRight = false;
    public bool hasUp = false;
    public bool hasDown = false;

    public bool hasLeftUpCorner = false;
    public bool hasRightUpCorner = false;
    public bool hasLeftDownCorner = false;
    public bool hasRightDownCorner = false;

    [SerializeField] private float blockSize = 1.6f;

    [SerializeField] private bool isDiged;
    [SerializeField] private bool isWatered;
    private GameObject updatedGameObject;

    public void AddDigedTile(Vector3 position, GameObject selectedTile)
    {
        position = roundPosition(position);

        if (!DigedDic.ContainsKey(position))
        {
            if (!CheckDictionaryForKeyWithTrueDiged(position))
            {
				isDiged = true;
				targetObject = selectedTile;
				DigedDic.Add(position, selectedTile);
				CheckNeightbours(position);
				UpdateTilesAroud(position);
			}
        }
		isDiged = false;

	}
    public void RemoveDigedTile(Vector3 position, GameObject selectedTile)
    {
        targetObject = selectedTile;

        // Zaokr¹glij liczby w position do 2 miejsc po przecinku
        position = roundPosition(position);
        GameObject foundGameObject;
		if (WaterdDic.ContainsKey(position))
		{
			if (CheckDictionaryForKeyWithTrueWatered(position))
			{
				GameObject value = null;
				if (ChangedWateredTile.TryGetValue(position, out foundGameObject))
				{
					ChangedWateredTile.Remove(position);
					Destroy(foundGameObject);
                }
                WaterdDic.Remove(position, out value);

				isWatered = true;
				//CheckNeightbours(position);
				UpdateTilesAroud(position);
				isWatered = false;
				return;
			}
		}

		if (DigedDic.ContainsKey(position))
        {
            if (CheckDictionaryForKeyWithTrueDiged(position))
            {
                GameObject value = null;
                if (ChangedDigedTile.TryGetValue(position, out foundGameObject))
                {
                    ChangedDigedTile.Remove(position);
                    Destroy(foundGameObject);
                }
                DigedDic.Remove(position, out value);
				//ChoosenGameObject = DigedDirtGameObject[24];
				isDiged = true;
				//CheckNeightbours(position);
				UpdateTilesAroud(position);
				isDiged = false;

				return;
			}
        }
    }
    public void AddWateredTile(Vector3 position, GameObject selectedTile)
    {
        position = roundPosition(position);
        if (!WaterdDic.ContainsKey(position) && DigedDic.ContainsKey(position))
        {
            if (!CheckDictionaryForKeyWithTrueWatered(position) && CheckDictionaryForKeyWithTrueDiged(position))
            {
				isWatered = true;
				targetObject = selectedTile;
				WaterdDic.Add(position, selectedTile);
				CheckNeightbours(position);
				UpdateTilesAroud(position);
			}
        }
		isWatered = false;
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
                if (DigedDic.TryGetValue(leftPos, out targetObjectAround))
                {
                    targetObject = targetObjectAround;
                    CheckNeightbours(leftPos);
                }
            }
            if (CheckDictionaryForKeyWithTrueDiged(rightPos))
            {
                if (DigedDic.TryGetValue(rightPos, out targetObjectAround))
                {
                    targetObject = targetObjectAround;
                    CheckNeightbours(rightPos);
                }
            }
            if (CheckDictionaryForKeyWithTrueDiged(upPos))
            {
                if (DigedDic.TryGetValue(upPos, out targetObjectAround))
                {
                    targetObject = targetObjectAround;
                    CheckNeightbours(upPos);
                }
            }
            if (CheckDictionaryForKeyWithTrueDiged(downPos))
            {
                if (DigedDic.TryGetValue(downPos, out targetObjectAround))
                {
                    targetObject = targetObjectAround;
                    CheckNeightbours(downPos);
                }
            }



            if (CheckDictionaryForKeyWithTrueDiged(leftUpPos))
            {
                if (DigedDic.TryGetValue(leftUpPos, out targetObjectAround))
                {
                    targetObject = targetObjectAround;
                    CheckNeightbours(leftUpPos);
                }
            }
            if (CheckDictionaryForKeyWithTrueDiged(rightUpPos))
            {
                if (DigedDic.TryGetValue(rightUpPos, out targetObjectAround))
                {
                    targetObject = targetObjectAround;
                    CheckNeightbours(rightUpPos);
                }
            }
            if (CheckDictionaryForKeyWithTrueDiged(leftDownPos))
            {
                if (DigedDic.TryGetValue(leftDownPos, out targetObjectAround))
                {
                    targetObject = targetObjectAround;
                    CheckNeightbours(leftDownPos);
                }
            }
			if (CheckDictionaryForKeyWithTrueDiged(rightDownPos))
			{
				if (DigedDic.TryGetValue(rightDownPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(rightDownPos);
				}
			}
		}
        else if(isWatered)
        {
			if (CheckDictionaryForKeyWithTrueWatered(leftPos))
			{
				if (WaterdDic.TryGetValue(leftPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(leftPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueWatered(rightPos))
			{
				if (WaterdDic.TryGetValue(rightPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(rightPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueWatered(upPos))
			{
				if (WaterdDic.TryGetValue(upPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(upPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueWatered(downPos))
			{
				if (WaterdDic.TryGetValue(downPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(downPos);
				}
			}



			if (CheckDictionaryForKeyWithTrueWatered(leftUpPos))
			{
				if (WaterdDic.TryGetValue(leftUpPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(leftUpPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueWatered(rightUpPos))
			{
				if (WaterdDic.TryGetValue(rightUpPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(rightUpPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueWatered(leftDownPos))
			{
				if (WaterdDic.TryGetValue(leftDownPos, out targetObjectAround))
				{
					targetObject = targetObjectAround;
					CheckNeightbours(leftDownPos);
				}
			}
			if (CheckDictionaryForKeyWithTrueWatered(rightDownPos))
			{
				if (WaterdDic.TryGetValue(rightDownPos, out targetObjectAround))
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

        if (isDiged)
        {
            hasLeft = DigedDic.ContainsKey(leftPos);
            hasRight = DigedDic.ContainsKey(rightPos);
            hasUp = DigedDic.ContainsKey(upPos);
            hasDown = DigedDic.ContainsKey(downPos);

            hasLeftUpCorner = DigedDic.ContainsKey(leftUpPos);
            hasRightUpCorner = DigedDic.ContainsKey(rightUpPos);
            hasLeftDownCorner = DigedDic.ContainsKey(leftDownPos);
            hasRightDownCorner = DigedDic.ContainsKey(rightDownPos);
        }
        else if (isWatered)
        {
            hasLeft = WaterdDic.ContainsKey(leftPos);
            hasRight = WaterdDic.ContainsKey(rightPos);
            hasUp = WaterdDic.ContainsKey(upPos);
            hasDown = WaterdDic.ContainsKey(downPos);

            hasLeftUpCorner = WaterdDic.ContainsKey(leftUpPos);
            hasRightUpCorner = WaterdDic.ContainsKey(rightUpPos);
            hasLeftDownCorner = WaterdDic.ContainsKey(leftDownPos);
            hasRightDownCorner = WaterdDic.ContainsKey(rightDownPos);
        }
        if (hasLeft && !hasUp && !hasRight && !hasDown)
            ChoosenGameObject = DigedDirtGameObject[0];
        if (!hasLeft && hasUp && !hasRight && !hasDown)
            ChoosenGameObject = DigedDirtGameObject[1];
        if (!hasLeft && !hasUp && hasRight && !hasDown)
            ChoosenGameObject = DigedDirtGameObject[2];
        if (!hasLeft && !hasUp && !hasRight && hasDown)
            ChoosenGameObject = DigedDirtGameObject[3];

        if (hasLeft && !hasUp && hasRight && hasDown)
            ChoosenGameObject = DigedDirtGameObject[4];
        if (hasLeft && hasUp && hasRight && !hasDown)
            ChoosenGameObject = DigedDirtGameObject[5];
        if (hasLeft && hasUp && !hasRight && hasDown)
            ChoosenGameObject = DigedDirtGameObject[6];
        if (!hasLeft && hasUp && hasRight && hasDown)
            ChoosenGameObject = DigedDirtGameObject[7];

        if (hasLeft && hasUp && hasRight && hasDown && !hasRightUpCorner)
            ChoosenGameObject = DigedDirtGameObject[8];
        if (hasLeft && hasUp && hasRight && hasDown && !hasLeftDownCorner)
            ChoosenGameObject = DigedDirtGameObject[9];
        if (hasLeft && hasUp && hasRight && hasDown && !hasRightDownCorner)
            ChoosenGameObject = DigedDirtGameObject[10];
        if (hasLeft && hasUp && hasRight && hasDown && !hasLeftUpCorner)
            ChoosenGameObject = DigedDirtGameObject[11];

        if (hasLeft && hasUp && !hasRight && !hasDown && hasLeftUpCorner)
            ChoosenGameObject = DigedDirtGameObject[12];
        if (!hasLeft && hasUp && hasRight && !hasDown && hasRightUpCorner)
            ChoosenGameObject = DigedDirtGameObject[13];
        if (!hasLeft && !hasUp && hasRight && hasDown && hasRightDownCorner)
            ChoosenGameObject = DigedDirtGameObject[14];
        if (hasLeft && !hasUp && !hasRight && hasDown && hasLeftDownCorner)
            ChoosenGameObject = DigedDirtGameObject[15];

        if (hasLeft && hasUp && !hasRight && !hasDown && !hasLeftUpCorner)
            ChoosenGameObject = DigedDirtGameObject[16];
        if (!hasLeft && hasUp && hasRight && !hasDown && !hasRightUpCorner)
            ChoosenGameObject = DigedDirtGameObject[17];
        if (!hasLeft && !hasUp && hasRight && hasDown && !hasRightDownCorner)
            ChoosenGameObject = DigedDirtGameObject[18];
        if (hasLeft && !hasUp && !hasRight && hasDown && !hasLeftDownCorner)
            ChoosenGameObject = DigedDirtGameObject[19];

        if (!hasLeft && hasUp && !hasRight && hasDown)
            ChoosenGameObject = DigedDirtGameObject[20];
        if (hasLeft && !hasUp && hasRight && !hasDown)
            ChoosenGameObject = DigedDirtGameObject[21];
        if (!hasLeft && !hasUp && !hasRight && !hasDown)
            ChoosenGameObject = DigedDirtGameObject[22];
        if (hasLeft && hasUp && hasRight && hasDown)
            ChoosenGameObject = DigedDirtGameObject[23];

        if (isWatered)
        {
            ChoosenGameObject = WateredDirtGameObject[0];
        }


        if (isDiged)
        {
            var createdTargetObject = targetObject;
			ChangedDigedTile.TryGetValue(postion, out createdTargetObject);
			ChangedDigedTile.Remove(postion);
            Destroy(createdTargetObject);

			ChangedDigedTile.Add(roundPosition(postion), Instantiate(ChoosenGameObject, new Vector3(postion.x, postion.y + 0.001f, postion.z), new Quaternion(0, 0, 0, 0)));
        }
        if (isWatered)
        {
			ChangedWateredTile.Add(roundPosition(postion), Instantiate(ChoosenGameObject, new Vector3(postion.x, postion.y + 0.002f, postion.z), new Quaternion(0, 0, 0, 0)));
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
        GameObject value;
        if (DigedDic.TryGetValue(position, out value))
        {
            return value;
        }
        return false;
    }

    bool CheckDictionaryForKeyWithTrueWatered(Vector3 position)
    {
        GameObject value;
        if (WaterdDic.TryGetValue(position, out value))
        {
            return value;
        }
        return false;
    }

}
