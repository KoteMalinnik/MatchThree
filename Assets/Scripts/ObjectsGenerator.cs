using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGenerator : MonoBehaviour
{
	[SerializeField]
	TileObject tileObjectPrefab;

	TileMap tileMap;
	TileObject[] objects;

	[SerializeField]
	Color[] colors;

	void Awake()
	{
		tileMap = GetComponent<TileMap>();
	}

	void Start()
	{
		spawnObjects();
	}

	void spawnObjects()
	{
		int height = tileMap.getHeight();
		int width = tileMap.getWidth();
		var objectsCount = height * width;
		objects = new TileObject[objectsCount];

		for (int i = 0, count = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++, count++)
			{
				TileObject temp = Instantiate(tileObjectPrefab, Vector3.zero, Quaternion.identity);
				temp.transform.parent = transform;

				var color = colors[Random.Range(0, colors.Length)];
				temp.setColor(color);

				objects[count] = temp;
				tileMap.getTileNodeByIndex(count).setTileObject(temp);
			}
		}
	}

	public TileObject getTileObjectByPosition(Vector2 position)
	{
		if (position.x > tileMap.getWidth()) return null;
		if (position.y > tileMap.getHeight()) return null;

		for (int i = 0; i < objects.Length; i++)
			if (objects[i].getParentNode().getPosition() == position)
				return objects[i];

		return null;
	}
}
