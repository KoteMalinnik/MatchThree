using UnityEngine;

namespace Tiles
{
	/// <summary>
	/// Генератор тайлов.
	/// </summary>
	public class MapGenerator : MonoBehaviour
	{
		/// <summary>
		/// Экзмемпляр класса.
		/// </summary>
		static MapGenerator _instance = null;

		public static MapGenerator Instance
		{ get { return _instance ?? new GameObject("ObjectsGenerator").AddComponent<MapGenerator>(); } }

		void Awake()
		{
			_instance = this;
		}

		void OnDestroy()
		{
			_instance = null;
		}

		[SerializeField]
		/// <summary>
		/// Префаб тайла.
		/// </summary>
		Tile tilePrefab = null;

		[SerializeField]
		/// <summary>
		/// Массив цветов генерируемых объектов
		/// </summary>
		Color[] colors = null;

		Transform cachedTransform = null;

		void Start()
		{
			cachedTransform = transform;
			spawnTiles();
		}

		/// <summary>
		/// Генерирует тайлы в сетке тайлов.
		/// </summary>
		void spawnTiles()
		{
			var tilesGrid = Map.getTilesGrid();

			for (int coloumn = 0; coloumn < Map.gridWidth; coloumn++)
			{
				spawnTilesInColoumn(coloumn, tilesGrid);
			}
		}

		/// <summary>
		/// Генерирует тайлы в столбце.
		/// </summary>
		/// <param name="coloumn">Coloumn.</param>
		public void spawnTilesInColoumn(int coloumn, Tile[,] tilesGrid)
		{
			for (int raw = 0; raw < Map.gridHeight; raw++)
			{
				if (tilesGrid[coloumn, raw] != null)
				{
					//Debug.Log($"[TilesGenerator] Узел сетки ({coloumn},{raw}) занят.");
					continue;
				}

				var position = new Vector2(coloumn, raw);
				Creator.createTileAtPosition(tilePrefab, position, cachedTransform, colors);
			}
		}
	}
}