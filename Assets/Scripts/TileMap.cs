using UnityEngine;

public class TileMap : MonoBehaviour
{
	[SerializeField]
	/// <summary>
	/// Ширина сетки тайлов
	/// </summary>
	int _gridWidht = 10;
	public static int gridWidht { get; private set; } = 6;

	[SerializeField]
	/// <summary>
	/// Высота сетки тайлов
	/// </summary>
	int _gridHeight = 10;
	public static int gridHeight { get; private set; } = 6;

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
		if (gridWidht < 3) gridWidht = 3;
		if (gridHeight < 3) gridHeight = 3;

		if (_tileSize < 1f) _tileSize = 1f;
	}

	void Awake()
	{
		tileSize = _tileSize;
		tileDeltaPosition = tileSize / 6.25f;

		gridWidht = _gridWidht;
		gridHeight = _gridHeight;
	}
}
