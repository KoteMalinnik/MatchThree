using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Генератор объектов сетки TileObject
/// </summary>
public class ObjectsGenerator : MonoBehaviour
{
	[SerializeField]
	TileObject tileObjectPrefab;

	/// <summary>
	/// Двумерный массив объектов в сетке. Нумерация по позиции в сетке
	/// </summary>
	TileObject[,] objects;

	[SerializeField]
	/// <summary>
	/// Массив цветов генерируемых объектов
	/// </summary>
	Color[] colors;

	int mapHeight;
	int mapWidth;

	void Awake()
	{
		mapHeight = TileMap.mapHeight;
		mapWidth = TileMap.mapWidht;
	}

	void Start()
	{
		spawnObjects();
	}

	/// <summary>
	/// Создание объектов в узлах сетки
	/// </summary>
	void spawnObjects()
	{
		
		objects = new TileObject[mapWidth, mapHeight];

		for (int i = 0; i < mapWidth; i++)
		{
			for (int j = 0; j < mapHeight; j++)
			{
				TileObject temp = Instantiate(tileObjectPrefab, Vector3.zero, Quaternion.identity);
				temp.transform.parent = transform;

				var color = colors[Random.Range(0, colors.Length)];
				var position = new Vector2(i, j);
				temp.setTileObjectParametrs(position, color);

				objects[i, j] = temp;
			}
		}
	}

	/// <summary>
	/// Возвращает TileObject в позиции position. Если в этой позиции нет объекта, возвращает null
	/// </summary>
	/// <returns>The tile object by position.</returns>
	/// <param name="position">Position.</param>
	public TileObject getTileObjectByPosition(Vector2 position)
	{
		if (position.x > mapWidth || position.x < 0) return null;
		if (position.y > mapHeight || position.y < 0) return null;

		for (int i = 0; i < mapWidth; i++)
			for (int j = 0; j < mapHeight; j++)
				if (objects[i, j].position == position)
					return objects[i, j];

		return null;
	}
}
