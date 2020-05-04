
using UnityEngine;

/// <summary>
/// Поиск тайлов в сетке тайлов.
/// </summary>
public static class TilesFinder
{
	/// <summary>
	/// Возвращает TileObject по ID. Если тайла с таким ID нет, то возвращает null
	/// </summary>
	public static Tile getTileAtID(float rawIndex, float coloumnIndex)
	{
		var i = (int)rawIndex;
		if (i < 0 || i > TilesMap.gridWidth-1) return null;

		var j = (int)coloumnIndex;
		if (j < 0 || j > TilesMap.gridHeight-1) return null;

		Tile targetTile = TilesMap.getTilesGrid()[i, j] ?? null;
		return targetTile;
	}

	/// <summary>
	/// Возвращает соседний тайл, который находится на расстоянии rawDelta тайлов по горизонтали и coloumnDelta тайлов по вертикали.
	/// Положительные значения Delta соостветствуют направлениям вправо и вверх.
	/// </summary>
	public static Tile getNearestTile(Tile tile, int rawDelta = 0, int coloumnDelta = 0)
	{
		var i = (int)tile.gridID.x;
		var j = (int)tile.gridID.y;
		Tile targetTile = getTileAtID(i + rawDelta, j + coloumnDelta);

		return targetTile;
	}

	/// <summary>
	/// Возвращает строку или столбец (в зависимости от параметра param), содержащий tile.
	/// </summary>
	/// <param name="tile">Tile.</param>
	/// <param name="param">"C" для столбца, "R" для ряда.</param>
	public static Tile[] getLine(Tile tile, string param)
	{
		int arraySize = param == "C" ? TilesMap.gridHeight : TilesMap.gridWidth;
		Tile[] tilesInLine = new Tile[arraySize];

		Tile temp = null;
		for (int i = 0; i < tilesInLine.Length; i++)
		{
			temp = param == "C" ? getTileAtID(tile.gridID.x, i) : getTileAtID(i, tile.gridID.y);
			if (temp == null) break;
			tilesInLine[i] = temp;
		}

		return tilesInLine;
	}

	/// <summary>
	/// Возвращает идентификатор тайла в указанной позиции.
	/// </summary>
	/// <param name="position">Position.</param>
	public static Vector2 getIDByPosition(Vector2 position)
	{
		if  (position.x < 0 || position.x > TilesMap.gridWidth || position.y < 0 || position.y > TilesMap.gridHeight)
		{
			Debug.LogError("Error");
			return new Vector2(-1, -1);
		}

		var ID = new Vector2(position.x, position.y) / TilesMap.tileDeltaPosition;
		return ID;
	}
}
