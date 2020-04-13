using UnityEngine;

public class TileMap : MonoBehaviour
{
	[SerializeField]
	/// <summary>
	/// Ширина сетки тайлов
	/// </summary>
	int _mapWidht = 10;
	public static int mapWidht { get; private set; } = 6;

	[SerializeField]
	/// <summary>
	/// Высота сетки тайлов
	/// </summary>
	int _mapHeight = 10;
	public static int mapHeight { get; private set; } = 6;

	[SerializeField]
	/// <summary>
	/// Размер тайла
	/// </summary>
	float _tileSize = 6f;
	public static float tileSize { get; private set;} = 6f;

	/// <summary>
	/// Разница позиций соседних тайлов. При tileSize = 1 => tileDeltaPosition = 0.16
	/// </summary>
	public static float tileDeltaPosition { get; private set; } = 0.16f;

	void OnValidate()
	{
		if (mapWidht < 3) mapWidht = 3;
		if (mapHeight < 3) mapHeight = 3;

		if (_tileSize < 1f) _tileSize = 1f;
	}

	void Awake()
	{
		tileSize = _tileSize;
		tileDeltaPosition = tileSize / 6.25f;

		mapWidht = _mapWidht;
		mapHeight = _mapHeight;
	}
}
