using UnityEngine;

/// <summary>
/// Содержит функции поиска TileObject
/// </summary>
public static class TileFinder
{
    /// <summary>
	/// Возвращает TileObject в позиции position. Если в этой позиции нет объекта, возвращает null
	/// </summary>
	/// <returns>The tile object by position.</returns>
	/// <param name="position">Position.</param>
	public static TileObject getTileAtPosition(Vector2 position)
	{
		if (position.x > TileMap.gridWidth || position.x < 0) return null;
		if (position.y > TileMap.gridHeight || position.y < 0) return null;

		TileObject[,] tiles = ObjectsGenerator.tiles;

		for (int i = 0; i < TileMap.gridWidth; i++)
			for (int j = 0; j < TileMap.gridHeight; j++)
				if (tiles[i, j].position == position)
					return tiles[i, j];

		return null;
	}

	/// <summary>
	///Возвращает соседний тайл в направлении direction
	/// </summary>
	/// <param name="direction">Vector2.(up, down, left, right).</param>
	public static TileObject getNearestTile(TileObject tile, Vector2 direction)
	{
		if (direction != Vector2.up && direction != Vector2.down && direction != Vector2.left && direction != Vector2.right)
		{
			Debug.LogError("getNearestTile: Неверное направление");
			return null;
		}

		direction *= TileMap.tileDeltaPosition;
		var targetTilePosition = tile.position + direction;

		return getTileAtPosition(targetTilePosition);
	}

	/// <summary>
	/// Возвращает столбец, содержащий tile
	/// </summary>
	/// <param name="tile">Tile.</param>
	/// <param name="param">"C" для столбца, "R" для ряда.</param>
	public static TileObject[] getLine(TileObject tile, string param)
	{
		int arraySize = param == "C" ? TileMap.gridHeight : TileMap.gridWidth;
		TileObject[] tilesInLine = new TileObject[arraySize];

		Vector2 direction = param == "C" ? Vector2.down : Vector2.left;

		TileObject checkingTile = tile;
		//Нахождение самого левого/нижнего тайла в линии
		for (int i = 0; i < tilesInLine.Length && getNearestTile(checkingTile, direction) != null; i++)
			checkingTile = getNearestTile(checkingTile, direction);

		direction *= -1;
		for (int i = 0; i < tilesInLine.Length; i++)
		{
			tilesInLine[i] = checkingTile;
			checkingTile = getNearestTile(checkingTile, direction);
			if (checkingTile == null) break;
		}

		return tilesInLine;
	}
}
