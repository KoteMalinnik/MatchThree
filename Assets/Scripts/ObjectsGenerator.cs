using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
	public static TileObject[,] tiles { get; private set; }

	[SerializeField]
	/// <summary>
	/// Массив цветов генерируемых объектов
	/// </summary>
	Color[] colors;

	float tileDeltaPosition = TileMap.tileDeltaPosition;
	int gridHeight = TileMap.gridHeight;
	int gridWidth = TileMap.gridWidth;

	void Start()
	{
		spawnObjects();
	}

	/// <summary>
	/// Создание объектов в узлах сетки
	/// </summary>
	void spawnObjects()
	{
		tiles = new TileObject[gridWidth, gridHeight];

		float posX = 0.0f;
		for (int i = 0; i < gridWidth; i++, posX += tileDeltaPosition)
		{
			float posY = 0.0f;
			for (int j = 0; j < gridHeight; j++, posY += tileDeltaPosition)
			{
				TileObject tile = Instantiate(tileObjectPrefab, Vector3.zero, Quaternion.identity);
				tile.transform.parent = transform;

				TileObject nearTile_Left = null;
				TileObject nearTile_Bottom = null;

				if (i > 0) nearTile_Left = tiles[i - 1, j];
				if (j > 0) nearTile_Bottom = tiles[i, j - 1];

				var position = new Vector2(posX, posY);
				var color = setTileObjectColor(nearTile_Left, nearTile_Bottom);

				tile.setTileObjectParametrs(position, color, nearTile_Left, nearTile_Bottom);
				tiles[i, j] = tile;
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

		if (leftTile != null)
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
}
