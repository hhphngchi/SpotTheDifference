using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	// generate a grid
	private int num_diffs = 5;
	private int num_total = 10;

	private int width = 1;
	private int height = 1;
	public GameObject Tile;
	private int tileScale = 10;
	public List<GameObject> gridPositions;
	public GameObject grid; // grid prefab

    public void getGrid()
    {
		if (num_total > width*height)
        {
			Debug.Log(" ds");
			while (num_total > width * height)
			{
				Debug.Log(tileScale * tileScale * num_total);
				Debug.Log("------");
				width = width + 1;
				height = height + 1;
			}
        }
	
		Debug.Log(width);
		Debug.Log(height);
		GameObject gridOne = Instantiate(grid, Vector3.zero, Quaternion.identity); 
		gridPositions = new List<GameObject>();
		for (int x = 0; x < width; x++)
        {
			for (int z = 0; z < height; z++)
			{
				Vector3 originalPosition = new Vector3(x * tileScale, 0, z * tileScale);
				GameObject pos = Instantiate(Tile, originalPosition, Quaternion.identity, gridOne.transform);
				
				// add position into the list
				gridPositions.Add(pos);
			}
		}
		spawnObjects();
		GameObject gridTwo = Instantiate(gridOne, new Vector3(width*tileScale + tileScale, 0, 0), Quaternion.identity);
		addDifferences(gridTwo);
    }

	// change material or color of an object
	public List<Texture> textureList;
	private bool settingColor = true;
	private List<Color> colorList = new List<Color>()
	{
		 Color.red,
		 Color.green,
		 Color.yellow,
		 Color.magenta,
		 new Color(255F, 0F, 255F),
		 new Color(0F, 255F, 255F),
		 new Color(255F, 255F, 0F),
		 new Color(128F, 0F, 128F),
		 new Color(128F, 0F, 0F)
	};

	// spawn random objects at random positions on the grid
	//private int totalItems = 10;
	public List<GameObject> spawnList;
	public List<Vector3> takenPostions;
	public List<GameObject> takenItems;
	private GameObject toSpawn;
	
	public void spawnObjects()
    {
		//find a random object
		int itemIndex = 0;
		for (int i = 0; i < num_total; i++)
		{
			// find a random pos on the grid then assign material to the object
			int posIndex = Random.Range(0, gridPositions.Count - 1);
			Debug.Log(gridPositions.Count);
			GameObject tile = gridPositions[posIndex];
			Vector3 tilePos = tile.transform.position;
			itemIndex = Random.Range(0, spawnList.Count - 1);
			Debug.Log(itemIndex);
			toSpawn = spawnList[itemIndex];
			while (takenPostions.Contains(tilePos))
			{
				posIndex = Random.Range(0, gridPositions.Count - 1);
				tile = gridPositions[posIndex];
				tilePos = tile.transform.position;
			}
			GameObject obj = Instantiate(toSpawn, tilePos, Quaternion.identity, tile.transform) as GameObject;
			obj.transform.localScale = new Vector3(tileScale, tileScale, tileScale); // change its local scale in x y z format
			if (settingColor)
			{
				obj.GetComponent<Renderer>().material.color = colorList[Random.Range(0, colorList.Count - 1)];
				settingColor = false;
			}
			else
			{
				obj.GetComponent<Renderer>().material.SetTexture("_MainTex", textureList[Random.Range(0, textureList.Count - 1)]);
				settingColor = true;
			}

			takenPostions.Add(tilePos);
			takenItems.Add(toSpawn);
		}
	}

	public void addDifferences(GameObject gridToDifferentiate)
    {
		List<GameObject> tilesWithObjects = new List<GameObject>();
		for (int i = 0; i < gridToDifferentiate.transform.childCount; i++)
        {			
			if (gridToDifferentiate.transform.GetChild(i).childCount > 0)
            {
				tilesWithObjects.Add(gridToDifferentiate.transform.GetChild(i).gameObject);
            }
		}
		
		for (int i = 0; i < num_diffs; i++)
        {
			int diff_position = Random.Range(0, tilesWithObjects.Count - 1);
			tilesWithObjects[diff_position].transform.GetChild(0).GetComponent<Renderer>().material.color = Color.black;
			tilesWithObjects.RemoveAt(diff_position);
		}

	}


	// Use this for initialization
	void Start()
	{
		getGrid();
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}
