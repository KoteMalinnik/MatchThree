using UnityEngine;

namespace Tiles
{
	/// <summary>
	/// Карта тайлов.
	/// </summary>
	public class Map : MonoBehaviour
	{
		[SerializeField]
		int _gridWidth = 1;

		[SerializeField]
		int _gridHeight = 1;

		void Awake()
		{
			gridWidth = _gridWidth;
			gridHeight = _gridHeight;
			Destroy(this);
		}

		/// <summary>
		/// Ширина сетки тайлов.
		/// </summary>
		public static int gridWidth = 8;

		/// <summary>
		/// Высота сетки тайлов.
		/// </summary>
		public static int gridHeight = 8;

		/// <summary>
		/// Размер тайла.
		/// </summary>
		public static readonly float tileSize = 6.25f;

		/// <summary>
		/// Разница позиций соседних тайлов. При tileSize = 1 => tileDeltaPosition = 0.16
		/// </summary>
		public static readonly float tileDeltaPosition = tileSize / 6.25f;

		/// <summary>
		/// Сетка тайлов.
		/// </summary>
		static Tile[,] tilesGrid = new Tile[gridWidth, gridHeight];

		/// <summary>
		/// Возвращает сетку тайлов.
		/// </summary>
		/// <returns>The tiles grid.</returns>
		public static Tile[,] getTilesGrid() { return tilesGrid; }

		/// <summary>
		/// Устанавливает тайл в сетке тайлов.
		/// </summary>
		/// <param name="tile">Tile.</param>
		public static void setTileInGrid(Tile tile)
		{
			var index_x = (int)tile.gridID.x;
			var index_y = (int)tile.gridID.y;
			tilesGrid[index_x, index_y] = tile;
		}

		/// <summary>
		/// Удаляет тайл из сетки тайлов.
		/// </summary>
		/// <param name="tile">Tile.</param>
		public static void removeTileFromGrid(Tile tile)
		{
			var index_x = (int)tile.gridID.x;
			var index_y = (int)tile.gridID.y;
			tilesGrid[index_x, index_y] = null;
		}
	}
}