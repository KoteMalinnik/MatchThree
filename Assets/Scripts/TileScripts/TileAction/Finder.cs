﻿using System.Collections.Generic;
using UnityEngine;

namespace Tiles
{
	/// <summary>
	/// Поиск тайлов в сетке тайлов.
	/// </summary>
	public static class Finder
	{
		/// <summary>
		/// Возвращает TileObject по ID. Если тайла с таким ID нет, то возвращает null
		/// </summary>
		public static Tile getTileAtID(float rawIndex, float coloumnIndex)
		{
			var i = (int)rawIndex;
			if (i < 0 || i > Map.gridWidth - 1) return null;

			var j = (int)coloumnIndex;
			if (j < 0 || j > Map.gridHeight - 1) return null;

			Tile targetTile = Map.getTilesGrid()[i, j] ?? null;
			return targetTile;
		}

		/// <summary>
		/// Возвращает соседний тайл, который находится на расстоянии rawDelta тайлов по горизонтали и coloumnDelta тайлов по вертикали.
		/// Положительные значения Delta соостветствуют направлениям вправо и вверх.
		/// </summary>
		public static Tile getNearestTile(Tile tile, int rawDelta = 0, int coloumnDelta = 0)
		{
			var i = (int)tile.gridID.x;
			var j = (int)tile.gridID.y;
			Tile targetTile = getTileAtID(i + rawDelta, j + coloumnDelta);

			return targetTile;
		}

		/// <summary>
		/// Возвращает строку или столбец (в зависимости от параметра param), содержащий tile.
		/// </summary>
		/// <param name="tile">Tile.</param>
		/// <param name="param">"C" для столбца, "R" для ряда.</param>
		public static Tile[] getLine(Tile tile, string param)
		{
			var tilesInLine = new List<Tile>();
			int lineLength = param == "C" ? Map.gridHeight : Map.gridWidth;

			Tile temp = null;
			for (int i = 0; i < lineLength; i++)
			{
				temp = param == "C" ? getTileAtID(tile.gridID.x, i) : getTileAtID(i, tile.gridID.y);
				if (temp == null) continue;
				tilesInLine.Add(temp);
			}

			return tilesInLine.ToArray();
		}

		/// <summary>
		/// Возвращает строку или столбец (в зависимости от параметра param) по индексу в сетке тайлов.
		/// </summary>
		/// <param name="lineNumber">Номер столбца или строки. Нумерация начинается с 0 снизу вверх справа налево.</param>
		/// <param name="param">"C" для столбца, "R" для ряда.</param>
		public static Tile[] getLine(int lineNumber, string param)
		{
			var tilesInLine = new List<Tile>();
			int lineLength = param == "C" ? Map.gridHeight : Map.gridWidth;

			Tile temp = null;
			for (int i = 0; i < lineLength; i++)
			{
				temp = param == "C" ? getTileAtID(lineNumber, i) : getTileAtID(i, lineNumber);
				if (temp == null) continue;
				tilesInLine.Add(temp);
			}

			return tilesInLine.ToArray();
		}

		/// <summary>
		/// Возвращает идентификатор тайла в указанной позиции.
		/// </summary>
		/// <param name="position">Position.</param>
		public static Vector2 getIDByPosition(Vector2 position)
		{
			if (position.x < 0 || position.x > Map.gridWidth || position.y < 0 || position.y > Map.gridHeight)
			{
				Debug.LogError("[TilesFinder] Error");
				return new Vector2(-1, -1);
			}

			var ID = new Vector2(position.x, position.y) / Map.tileDeltaPosition;
			return ID;
		}

		public static Tile[] getTilesOfColor(Color color)
		{
			var tiles = new List<Tile>();

			for (int x = 0; x < Map.gridWidth; x++)
			{
				for (int y = 0; y < Map.gridHeight; y++)
				{
					var tile = getTileAtID(x, y);

					if (tile.color == color) tiles.Add(tile);
				}
			}

			return tiles.ToArray();
		}
	}	
}