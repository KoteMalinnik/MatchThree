using UnityEngine;
using System.Collections;

/// <summary>
/// Генератор тайлов.
/// </summary>
public class TilesGenerator : MonoBehaviour
{
	/// <summary>
	/// Экзмемпляр класса.
	/// </summary>
	static TilesGenerator _instance = null;

	public static TilesGenerator Instance
		{ get { return _instance ?? new GameObject("ObjectsGenerator").AddComponent<TilesGenerator>();} }

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
		var tilesGrid = TilesMap.getTilesGrid();

		for (int coloumn = 0; coloumn < TilesMap.gridWidth; coloumn++)
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
		for (int raw = 0; raw < TilesMap.gridHeight; raw++)
		{
			if (tilesGrid[coloumn, raw] != null)
			{
				//Debug.Log($"[TilesGenerator] Узел сетки ({coloumn},{raw}) занят.");
				continue;
			}

			var position = new Vector2(coloumn, raw);
			TileCreator.createTileAtPosition(tilePrefab, position, cachedTransform, colors);
		}
	}
}
