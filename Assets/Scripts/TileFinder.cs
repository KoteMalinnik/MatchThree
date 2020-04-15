using UnityEngine;

/// <summary>
/// Содержит функции поиска TileObject
/// </summary>
public static class TileFinder
{
	static readonly int gridWidth = TileMap.gridWidth;
	static readonly int gridHeight = TileMap.gridHeight;
	static readonly float tileDeltaPosition = TileMap.tileDeltaPosition;

    /// <summary>
	/// Возвращает TileObject в позиции position. Если в этой позиции нет объекта, возвращает null
	/// </summary>
	/// <returns>The tile object by position.</returns>
	/// <param name="position">Position.</param>
	public static TileObject getTileObjectByPosition(Vector2 position)
	{
		if (position.x > gridWidth || position.x < 0) return null;
		if (position.y > gridHeight || position.y < 0) return null;

		TileObject[,] tiles = ObjectsGenerator.tiles;

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
	public static TileObject getNearestTile(TileObject tile, Vector2 direction)
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
	public static TileObject[] getColoumn(TileObject tile)
	{
		TileObject[] tilesInColoumn = new TileObject[gridHeight];

		TileObject bottomestTileInTheGrid = tile;
		for (int i = 0; i < tilesInColoumn.Length && bottomestTileInTheGrid.nearTile_Bottom != null; i++)
			bottomestTileInTheGrid = bottomestTileInTheGrid.nearTile_Bottom;

		TileObject checkingTile = bottomestTileInTheGrid;
		for (int i = 0; i < tilesInColoumn.Length; i++)
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
	public static TileObject[] getRaw(TileObject tile)
	{
		TileObject[] tilesInRaw = new TileObject[gridWidth];

		TileObject leftestTileInTheGrid = tile;
		for (int i = 0; i < tilesInRaw.Length && leftestTileInTheGrid.nearTile_Left != null; i++)
			leftestTileInTheGrid = leftestTileInTheGrid.nearTile_Left;

		TileObject checkingTile = leftestTileInTheGrid;
		for (int i = 0; i < tilesInRaw.Length; i++)
		{
			tilesInRaw[i] = checkingTile;
			checkingTile = getNearestTile(checkingTile, Vector2.right);
			if (checkingTile == null) break;
		}

		return tilesInRaw;
	}
}
