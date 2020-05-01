using UnityEngine;
using UnityEditorInternal.Profiling.Memory.Experimental;

public static class TileMap
{
	/// <summary>
	/// Ширина сетки тайлов
	/// </summary>
	public static readonly int gridWidth = 6;

	/// <summary>
	/// Высота сетки тайлов
	/// </summary>
	public static readonly int gridHeight = 8;

	/// <summary>
	/// Размер тайла
	/// </summary>
	public static readonly float tileSize = 6f;

	/// <summary>
	/// Разница позиций соседних тайлов. При tileSize = 1 => tileDeltaPosition = 0.16
	/// </summary>
	public static readonly float tileDeltaPosition = tileSize / 6.25f;

	/// <summary>
	/// Двумерный массив тайлов
	/// </summary>
	static Tile[,] tilesGrid = new Tile[gridWidth, gridHeight];

	/// <summary>
	/// Возвращает grid
	/// </summary>
	/// <returns>The tiles grid.</returns>
	public static Tile[,] getTilesGrid() { return tilesGrid;}

	/// <summary>
	/// Устанавливает grid
	/// </summary>
	/// <param name="newTile">New tile.</param>
	public static void setTileInGrid(Tile newTile)
	{
		var index_x = (int)newTile.gridID.x;
		var index_y = (int)newTile.gridID.y;
		tilesGrid[index_x, index_y] = newTile;
	}
}
