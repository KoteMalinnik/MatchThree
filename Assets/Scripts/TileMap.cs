using UnityEngine;

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
}
