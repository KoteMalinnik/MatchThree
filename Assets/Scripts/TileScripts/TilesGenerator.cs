using UnityEngine;

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
	/// Генерирует объекты в сетке тайлов.
	/// </summary>
	void spawnTiles()
	{
		float posX = 0.0f;
		for (int i = 0; i < TilesMap.gridWidth; i++, posX += TilesMap.tileDeltaPosition)
		{
			float posY = 0.0f;
			for (int j = 0; j < TilesMap.gridHeight; j++, posY += TilesMap.tileDeltaPosition)
			{
				var position = new Vector2(posX, posY);
				TileCreator.createTileAtPosition(tilePrefab, position, cachedTransform, colors);
			}
		}
	}
}
