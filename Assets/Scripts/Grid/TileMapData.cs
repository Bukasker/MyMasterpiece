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
	[SerializeField] private GameObject[] occupanted;
	[SerializeField] private GameObject[] wateredDirt;
	[SerializeField] private GameObject[] water;
	private Vector2 positionXZ;

	public static Dictionary<Vector2, TileType> GridDic = new Dictionary<Vector2, TileType>();

	private TileType gameObjectType;

	private void Start()
	{
		GridDic.Clear();
		Read();
	}
	private void Read()
	{
		for (int i = 0; i < gameObjectsToRead?.Length; i++)
		{
			for (int b = 0; b < gameObjectsToRead[i]?.transform.childCount; b++)
			{
				var readedGameObjects = gameObjectsToRead[i].transform.GetChild(b).gameObject;
				var x = readedGameObjects.transform.position.x;
				var z = readedGameObjects.transform.position.z;
				positionXZ = new Vector2(x, z);

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
					for (int j = 0; j < occupanted.Length; j++)
					{
						if (readedGameObjects.name == occupanted[j].name)
						{
							gameObjectType = TileType.Occupated;
							break;
						}
					}
				}
				if (!foundType)
				{
					for (int j = 0; j < occupanted.Length; j++)
					{
						if (readedGameObjects.name == wateredDirt[j].name)
						{
							gameObjectType = TileType.WateredDirt;
							break;
						}
					}
				}
				if (!foundType)
				{
					for (int j = 0; j < occupanted.Length; j++)
					{
						if (readedGameObjects.name == tree[j].name)
						{
							gameObjectType = TileType.Tree;
							break;
						}
					}
				}
				if (!foundType)
				{
					for (int j = 0; j < occupanted.Length; j++)
					{
						if (readedGameObjects.name == ore[j].name)
						{
							gameObjectType = TileType.Ore;
							break;
						}
					}
				}

				if (foundType)
				{
					AddToGrid();
				}
				foundType = false;
			}
		}
	}

	public void AddToGrid()
	{
		GridDic.Add(positionXZ, gameObjectType);
	}

	public enum TileType
	{
		Dirt,
		Grass,
		Sand,
		Water,
		WateredDirt,
		Tree,
		Ore,
		Occupated
	}
}

