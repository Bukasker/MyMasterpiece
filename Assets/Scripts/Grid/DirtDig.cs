using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using UnityEngine;

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


	public void AddDigedTile(Vector3 position,GameObject selectedTile)
	{
		targetObject = selectedTile;
		DigedDic.Add(position, true);
		Debug.Log(position);
		CheckNeightbours(position);
		//check tiles aroud position 
		UpdateTilesAroud();
	}

	public void UpdateTilesAroud()
	{

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

		hasLeft = DigedDic.ContainsKey(postion - new Vector3(blockSize, 0, 0));
		Debug.Log(postion - new Vector3(blockSize, 0, 0)+" Blok po lewej");

		//hasRight = DigedDic.ContainsKey(postion + new Vector3(blockSize, 0, 0));
		//hasUp = DigedDic.ContainsKey(postion + new Vector3(0, 0, blockSize));
		//hasDown = DigedDic.ContainsKey(postion - new Vector3(0, 0, blockSize));

		//hasLeftUpCorner = DigedDic.ContainsKey(postion - new Vector3(blockSize, 0, -blockSize));
		//hasRightUpCorner = DigedDic.ContainsKey(postion + new Vector3(blockSize, 0, -blockSize));
		//hasLeftDownCorner = DigedDic.ContainsKey(postion + new Vector3(-blockSize, 0, blockSize));
		//hasRightDownCorner = DigedDic.ContainsKey(postion + new Vector3(blockSize, 0, blockSize));


		if (hasLeft && !hasUp && !hasRight && !hasDown)
			ChoosenGameObject = Left;
		if (!hasLeft && hasUp && !hasRight && !hasDown)
			ChoosenGameObject = Up;
		if (!hasLeft && !hasUp && hasRight && !hasDown)
			ChoosenGameObject = Right;
		if (!hasLeft && !hasUp && !hasRight && hasDown)
			ChoosenGameObject = Down;

		if (hasLeft && !hasUp && hasRight && hasDown && !hasLeftUpCorner && !hasRightUpCorner && hasRightDownCorner && hasLeftDownCorner)
			ChoosenGameObject = LeftDownRightFull;
		if (hasLeft && hasUp && hasRight && !hasDown && hasLeftUpCorner && hasRightUpCorner && !hasRightDownCorner && !hasLeftDownCorner)
			ChoosenGameObject = LeftUpRightFull;
		if (hasLeft && hasUp && !hasRight && hasDown && hasLeftUpCorner && !hasRightUpCorner && !hasRightDownCorner && hasLeftDownCorner)
			ChoosenGameObject = LeftDownUpFull;
		if (!hasLeft && hasUp && hasRight && hasDown && !hasLeftUpCorner && hasRightUpCorner && hasRightDownCorner && !hasLeftDownCorner)
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

		Instantiate(ChoosenGameObject, targetObject.transform.position,new Quaternion(0,0,0,0));
		Destroy(targetObject);
	}


}
