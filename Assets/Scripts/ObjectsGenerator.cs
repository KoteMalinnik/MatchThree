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

	void Start()
	{
		float tileDeltaPosition = TileMap.tileDeltaPosition;
		int mapHeight = TileMap.mapHeight;
		int mapWidth = TileMap.mapWidht;

		spawnObjects(mapHeight, mapWidth, tileDeltaPosition);
	}

	/// <summary>
	/// Создание объектов в узлах сетки
	/// </summary>
	void spawnObjects(int mapHeight, int mapWidth, float tileDeltaPosition)
	{
		objects = new TileObject[mapWidth, mapHeight];

		float posX = 0.0f;
		for (int i = 0; i < mapWidth; i++, posX += tileDeltaPosition)
		{
			float posY = 0.0f;
			for (int j = 0; j < mapHeight; j++, posY += tileDeltaPosition)
			{
				TileObject tile = Instantiate(tileObjectPrefab, Vector3.zero, Quaternion.identity);
				tile.transform.parent = transform;

				TileObject nearTile_Left = null;
				TileObject nearTile_Bottom = null;

				if (i > 0) nearTile_Left = objects[i - 1, j];
				if (j > 0) nearTile_Bottom = objects[i, j - 1];

				var position = new Vector2(posX, posY);
				var color = setTileObjectColor(nearTile_Left, nearTile_Bottom);

				tile.setTileObjectParametrs(position, color, nearTile_Left, nearTile_Bottom);
				objects[i, j] = tile;
			}
		}
	}

	/// <summary>
	/// Возвращает цвет, который не имеют соседние тайлы
	/// </summary>
	Color setTileObjectColor(TileObject leftTile, TileObject bottomTile)
	{
		var resultColor = new Color();

		var availableColors = new List<Color>(colors.Length);
		for (int i = 0; i < colors.Length; i++) availableColors.Add(colors[i]);

		if(leftTile != null)
		{
			Color leftTileColor = leftTile.color;
			if (availableColors.Contains(leftTileColor)) availableColors.Remove(leftTileColor);
		}

		if (bottomTile != null)
		{
			Color bottomTileColor = bottomTile.color;
			if (availableColors.Contains(bottomTileColor)) availableColors.Remove(bottomTileColor);
		}

		resultColor = availableColors[Random.Range(0, availableColors.Count)];
		return resultColor;
	}

	/// <summary>
	/// Возвращает TileObject в позиции position. Если в этой позиции нет объекта, возвращает null
	/// </summary>
	/// <returns>The tile object by position.</returns>
	/// <param name="position">Position.</param>
	public TileObject getTileObjectByPosition(Vector2 position)
	{
		int mapHeight = TileMap.mapHeight;
		int mapWidth = TileMap.mapWidht;

		if (position.x > mapWidth || position.x < 0) return null;
		if (position.y > mapHeight || position.y < 0) return null;

		for (int i = 0; i < mapWidth; i++)
			for (int j = 0; j < mapHeight; j++)
				if (objects[i, j].position == position)
					return objects[i, j];

		return null;
	}

	//public TileObject[] getColoumn(TileObject tile)
	//{
		
	//}
}
