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
				TileObject temp = Instantiate(tileObjectPrefab, Vector3.zero, Quaternion.identity);
				temp.transform.parent = transform;

				var position = new Vector2(posX, posY);
				var color = setTileObjectColor(position, tileDeltaPosition);

				temp.setTileObjectParametrs(position, color);
				objects[i, j] = temp;
			}
		}
	}

	/// <summary>
	/// Возвращает цвет, который не имеют соседние тайлы
	/// </summary>
	/// <returns>The nearest tile objects for color match.</returns>
	Color setTileObjectColor(Vector2 sourceObjectPosition, float tileDeltaPosition)
	{
		//Надо анализировать цвет объектов, которые находятся левее и ниже исходного
		int index = Random.Range(0, colors.Length);
		Color resultColor = colors[index];

		var availableColors = new List<Color>(colors.Length);
		for (int i = 0; i < colors.Length; i++) availableColors.Add(colors[i]);

		checkTileColor(ref availableColors, ref resultColor, sourceObjectPosition, deltaPositionX: tileDeltaPosition);
		checkTileColor(ref availableColors, ref resultColor, sourceObjectPosition, deltaPositionY: tileDeltaPosition);

		return resultColor;
	}

	void checkTileColor(ref List<Color> availableColors, ref Color resultColor, Vector2 sourceObjectPosition, float deltaPositionX = 0, float deltaPositionY = 0)
	{
		var checkPosition = new Vector2(sourceObjectPosition.x - deltaPositionX, sourceObjectPosition.y - deltaPositionY);

		if (getTileObjectByPosition(checkPosition) != null)
		{
			Color tileColor = getTileObjectByPosition(checkPosition).color;

			if (tileColor == resultColor)
			{
				availableColors.Remove(tileColor);
				resultColor = availableColors[Random.Range(0, availableColors.Count)];
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
}
