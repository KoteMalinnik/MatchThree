﻿using UnityEngine;

public class TileMap : MonoBehaviour
{
	[SerializeField]
	/// <summary>
	/// Ширина сетки тайлов
	/// </summary>
	int mapWidht = 10;
	public int getWidth() { return mapWidht; }

	[SerializeField]
	/// <summary>
	/// Высота сетки тайлов
	/// </summary>
	int mapHeight = 10;
	public int getHeight() { return mapHeight; }

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
	}
}
