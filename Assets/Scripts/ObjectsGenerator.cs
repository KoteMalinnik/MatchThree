using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Генератор тайлов
/// </summary>
public class ObjectsGenerator : MonoBehaviour
{
	[SerializeField]
	TileObject tileObjectPrefab;

	[SerializeField]
	/// <summary>
	/// Массив цветов генерируемых объектов
	/// </summary>
	Color[] colors;

	void Start()
	{
		spawnTiles();
	}

	/// <summary>
	/// Создание объектов в узлах сетки
	/// </summary>
	void spawnTiles()
	{
		float posX = 0.0f;
		for (int i = 0; i < TileMap.gridWidth; i++, posX += TileMap.tileDeltaPosition)
		{
			float posY = 0.0f;
			for (int j = 0; j < TileMap.gridHeight; j++, posY += TileMap.tileDeltaPosition)
			{
				var position = new Vector2(posX, posY);
				createTileAtPosition(position);
			}
		}
	}

	void createTileAtPosition(Vector2 position)
	{
		TileObject tile = Instantiate(tileObjectPrefab, Vector3.zero, Quaternion.identity);
		tile.transform.parent = transform;

		var leftTileID = TileFinder.getIDByPosition(position) + Vector2.left;
		TileObject leftTile = TileFinder.getTileAtID(leftTileID.x, leftTileID.y);

		var bottomTileID = TileFinder.getIDByPosition(position) + Vector2.down;
		TileObject bottomTile = TileFinder.getTileAtID(bottomTileID.x, bottomTileID.y);

		var color = setTileColor(leftTile, bottomTile);

		tile.setTileParametrs(position, color);
	}

	/// <summary>
	/// Возвращает цвет, который не имеют соседние тайлы
	/// </summary>
	Color setTileColor(TileObject leftTile, TileObject bottomTile)
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
