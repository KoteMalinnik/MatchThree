using System.Collections.Generic;
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

	void Start()
	{
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
				createTileAtPosition(position);
			}
		}
	}

	/// <summary>
	/// Создает тайл в позиции.
	/// </summary>
	/// <param name="position">Position.</param>
	public static Tile createTileAtPosition(Vector2 position)
	{
		Tile tile = Instantiate(Instance.tilePrefab, Vector3.zero, Quaternion.identity);
		tile.transform.parent = Instance.transform;

		var leftTileID = TilesFinder.getIDByPosition(position) + Vector2.left;
		Tile leftTile = TilesFinder.getTileAtID(leftTileID.x, leftTileID.y);

		var bottomTileID = TilesFinder.getIDByPosition(position) + Vector2.down;
		Tile bottomTile = TilesFinder.getTileAtID(bottomTileID.x, bottomTileID.y);

		var color = Instance.getUniqueColor(leftTile, bottomTile);

		tile.setTileParametrs(position, color);

		return tile;
	}

	/// <summary>
	/// Возвращает цвет, который не имеют соседние тайлы
	/// </summary>
	Color getUniqueColor(Tile leftTile, Tile bottomTile)
	{
		var resultColor = new Color();

		var availableColors = new List<Color>(colors.Length);
		for (int i = 0; i < colors.Length; i++) availableColors.Add(colors[i]);

		if (leftTile != null)
		{
			Color leftTileColor = leftTile.color;
			if (availableColors.Contains(leftTileColor)) availableColors.Remove(leftTileColor);
		}

		if (bottomTile != null)
		{
			Color bottomTileColor = bottomTile.color;
			if (availableColors.Contains(bottomTileColor)) availableColors.Remove(bottomTileColor);
		}

		resultColor = availableColors[Random.Range(0, availableColors.Count)];
		return resultColor;
	}

}
