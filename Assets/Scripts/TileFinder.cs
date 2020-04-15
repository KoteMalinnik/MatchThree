using UnityEngine;

/// <summary>
/// Содержит функции поиска TileObject
/// </summary>
public static class TileFinder
{
	/// <summary>
	/// Возвращает TileObject по ID. Если тайла с таким ID нет, то возвращает null
	/// </summary>
	public static TileObject getTileAtID(float rawIndex, float coloumnIndex)
	{
		var i = (int)rawIndex;
		if (i < 0 || i > TileMap.gridWidth) return null;

		var j = (int)coloumnIndex;
		if (j < 0 || j > TileMap.gridHeight) return null;

		TileObject targetTile = TileMap.getTilesGrid()[i, j] ?? null;
		return targetTile;
	}

	/// <summary>
	///Возвращает соседний тайл, который находится на расстоянии rawDelta тайлов по горизонтали и coloumnDelta тайлов по вертикали
	/// </summary>
	public static TileObject getNearestTile(TileObject tile, int rawDelta = 0, int coloumnDelta = 0)
	{
		var i = (int)tile.gridID.x;
		var j = (int)tile.gridID.y;
		TileObject targetTile = getTileAtID(i + rawDelta, j + coloumnDelta);

		return targetTile;
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

		TileObject temp = null;
		for (int i = 0; i < tilesInLine.Length; i++)
		{
			temp = param == "C" ? getTileAtID(tile.gridID.x, i) : getTileAtID(i, tile.gridID.y);
			if (temp == null) break;
			tilesInLine[i] = temp;
		}

		return tilesInLine;
	}

	public static Vector2 getIDByPosition(Vector2 position)
	{
		if  (position.x < 0 || position.x > TileMap.gridWidth || position.y < 0 || position.y > TileMap.gridHeight)
		{
			Debug.LogError("Error");
			return new Vector2(-1, -1);
		}

		var ID = new Vector2(position.x, position.y) / TileMap.tileDeltaPosition;
		return ID;
	}
}
