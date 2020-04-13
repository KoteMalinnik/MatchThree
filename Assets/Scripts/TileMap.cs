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

	void OnValidate()
	{
		if (mapWidht < 3) mapWidht = 3;
		if (mapHeight < 3) mapHeight = 3;

		if (_tileSize < 1f) _tileSize = 1f;
	}

	void Awake()
	{
		tileSize = _tileSize;
		mapWidht = _mapWidht;
		mapHeight = _mapHeight;
	}
}
