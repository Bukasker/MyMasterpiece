using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapData : MonoBehaviour
{
	[SerializeField] private GameObject[] gameObjectsToRead;
	[SerializeField] private GameObject[] dirt;
	[SerializeField] private GameObject[] sand;
	[SerializeField] private GameObject[] grass;
	[SerializeField] private GameObject[] tree;
	[SerializeField] private GameObject[] ore;
	[SerializeField] private GameObject[] digeDirt;
	[SerializeField] private GameObject[] wateredDirt;
	[SerializeField] private GameObject[] water;
	[SerializeField] private GameObject[] ActionSource;
	private Vector3 positionXYZ;

	public static Dictionary<Vector3, TileType> GridDic = new Dictionary<Vector3, TileType>();
	public static Dictionary<Vector3, GameObject> GridDicGameObject = new Dictionary<Vector3, GameObject>();


	private TileType gameObjectType;

	private void Start()
	{
		GridDic.Clear();
		Read();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			GridDic.Clear();
			Read();
		}
	}
	private void Read()
	{
		for (int i = 0; i < gameObjectsToRead?.Length; i++)
		{
			for (int b = 0; b < gameObjectsToRead[i]?.transform.childCount; b++)
			{
				var readedGameObjects = gameObjectsToRead[i].transform.GetChild(b).gameObject;
				var x = readedGameObjects.transform.position.x;
				var y = readedGameObjects.transform.position.y;
				var z = readedGameObjects.transform.position.z;
				positionXYZ = new Vector3(x, y, z);

				bool foundType = false;

				if (!foundType)
				{
					for (int j = 0; j < dirt.Length; j++)
					{
						if (readedGameObjects.name == dirt[j].name)
						{
							gameObjectType = TileType.Dirt;
							foundType = true;
							break;
						}
					}
				}
				if (!foundType)
				{
					for (int j = 0; j < sand.Length; j++)
					{
						if (readedGameObjects.name == sand[j].name)
						{
							gameObjectType = TileType.Sand;
							foundType = true;
							break;
						}
					}
				}

				if (!foundType)
				{
					for (int j = 0; j < grass.Length; j++)
					{
						if (readedGameObjects.name == grass[j].name)
						{
							gameObjectType = TileType.Grass;
							foundType = true;
							break;
						}
					}
				}

				if (!foundType)
				{
					for (int j = 0; j < water.Length; j++)
					{
						if (readedGameObjects.name == water[j].name)
						{
							gameObjectType = TileType.Water;
							foundType = true;
							break;
						}
					}
				}
				
				if (!foundType)
				{
					for (int j = 0; j < digeDirt.Length; j++)
					{
						if (readedGameObjects.name == digeDirt[j].name)
						{
							gameObjectType = TileType.DigedDirt;
							foundType = true;
							break;
						}
					}
				}

				if (!foundType)
				{
					for (int j = 0; j < wateredDirt.Length; j++)
					{
						if (readedGameObjects.name == wateredDirt[j].name)
						{
							gameObjectType = TileType.WateredDirt;
							foundType = true;
							break;
						}
					}
				}
				if (!foundType)
				{
					for (int j = 0; j < tree.Length; j++)
					{
						if (readedGameObjects.name == tree[j].name)
						{
							gameObjectType = TileType.Tree;
							foundType = true;
							break;
						}
					}
				}
				if (!foundType)
				{
					for (int j = 0; j < ore.Length; j++)
					{
						if (readedGameObjects.name == ore[j].name)
						{
							gameObjectType = TileType.Ore;
							foundType = true;
							break;
						}
					}
				}

				if (!foundType)
				{
					for (int j = 0; j < ActionSource.Length; j++)
					{
						if (readedGameObjects.name == ActionSource[j].name)
						{
							gameObjectType = TileType.ActionSource;
							foundType = true;
							break;
						}
					}
				}
				if (foundType)
				{
					GridDicGameObject.Add(positionXYZ, readedGameObjects);
					AddToGrid();
				}
				else
				{
					//Debug.Log("Nie znaleziono typu dla " + readedGameObjects.name);
				}
			}
		}
	}

	public void AddToGrid()
	{
		GridDic.Add(positionXYZ, gameObjectType);
		//Debug.Log(gameObjectType);
	}

	public enum TileType
	{
		Dirt,
		Grass,
		Sand,
		Water,
		DigedDirt,
		WateredDirt,
		Tree,
		Ore,
		ActionSource
	}
}

