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
	TileObject[,] tiles;

	[SerializeField]
	/// <summary>
	/// Массив цветов генерируемых объектов
	/// </summary>
	Color[] colors;

	float tileDeltaPosition = TileMap.tileDeltaPosition;
	int gridHeight = TileMap.gridHeight;
	int gridWidth = TileMap.gridWidht;

	void Start()
	{
		tileDeltaPosition = TileMap.tileDeltaPosition;
		gridHeight = TileMap.gridHeight;
		gridWidth = TileMap.gridWidht;

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

	/// <summary>
	/// Возвращает TileObject в позиции position. Если в этой позиции нет объекта, возвращает null
	/// </summary>
	/// <returns>The tile object by position.</returns>
	/// <param name="position">Position.</param>
	public TileObject getTileObjectByPosition(Vector2 position)
	{
		if (position.x > gridWidth || position.x < 0) return null;
		if (position.y > gridHeight || position.y < 0) return null;

		for (int i = 0; i < gridWidth; i++)
			for (int j = 0; j < gridHeight; j++)
				if (tiles[i, j].position == position)
					return tiles[i, j];

		return null;
	}

	/// <summary>
	///Возвращает соседний тайл в направлении direction
	/// </summary>
	/// <param name="direction">Vector2.(up, down, left, right).</param>
	public TileObject getNearestTile(TileObject tile, Vector2 direction)
	{
		if (direction != Vector2.up && direction != Vector2.down && direction != Vector2.left && direction != Vector2.right)
		{
			Debug.LogError("getNearestTile: Неверное направление");
			return null;
		}

		direction *= tileDeltaPosition;
		var targetTilePosition = tile.position + direction;

		return getTileObjectByPosition(targetTilePosition);
	}

	/// <summary>
	/// Возвращает столбец, содержащий tile
	/// </summary>
	/// <param name="tile">Tile.</param>
	public TileObject[] getColoumn(TileObject tile)
	{
		TileObject[] tilesInColoumn = new TileObject[gridHeight];

		TileObject bottomestTileInTheGrid = tile;
		while(bottomestTileInTheGrid.nearTile_Bottom != null)
			bottomestTileInTheGrid = bottomestTileInTheGrid.nearTile_Bottom;

		TileObject checkingTile = bottomestTileInTheGrid;
		for (int i = 0; i < gridHeight; i++)
		{
			tilesInColoumn[i] = checkingTile;
			checkingTile = getNearestTile(checkingTile, Vector2.up);
			if (checkingTile == null) break;
		}

		return tilesInColoumn;
	}

	/// <summary>
	/// Возвращает строку, в которой находится tile
	/// </summary>
	/// <returns>The raw.</returns>
	/// <param name="tile">Tile.</param>
	public TileObject[] getRaw(TileObject tile)
	{
		TileObject[] tilesInRaw = new TileObject[gridWidth];

		TileObject leftestTileInTheGrid = tile;
		while (leftestTileInTheGrid.nearTile_Left != null)
			leftestTileInTheGrid = leftestTileInTheGrid.nearTile_Left;

		TileObject checkingTile = leftestTileInTheGrid;
		for (int i = 0; i < gridWidth; i++)
		{
			checkingTile.transform.localScale /= 2;
			tilesInRaw[i] = checkingTile;
			checkingTile = getNearestTile(checkingTile, Vector2.right);
			if (checkingTile == null) break;
		}

		return tilesInRaw;
	}
}
