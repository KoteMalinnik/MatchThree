﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Поверка совпадений тайлов в строке или столбце.
/// </summary>
public static class MatchesValidator
{
	//Содержит совпавшие тайлы, которые надо будет уничтожить
	static Tile[] matchedTiles = new Tile[0];
	public static Tile[] getMatchedTiles() { return matchedTiles; }
	public static void clearMatchedTile() { matchedTiles = new Tile[0]; }

	/// <summary>
	/// Возвращает true, если first и second можно поменять местами.
	/// </summary>
	/// <param name="tile2">First.</param>
	/// <param name="tile1">Second.</param>
	public static bool couldReplaceTiles(Tile tile1, Tile tile2)
	{
		Debug.Log("[MatchesValidator] Проверка тайлов на возможность перемещения");
		bool result = false;
		//Нет необходимости что-либо проверять, если второй объект на в пределах одного объекта по горизонтали или вертикали
		result = (tile2.position - tile1.position).magnitude < TilesMap.tileDeltaPosition + 0.1f;
		if (!result) return false;
		Debug.Log("[MatchesValidator] В пределах одного тайла");

		//Нет необходимости проверять, если они одного цвета
		result = tile2.color != tile1.color;
		if (!result) return false;
		Debug.Log("[MatchesValidator] Разные цвета тайлов");

		Debug.Log("[MatchesValidator] Проверка на совпадения");
		clearMatchedTile();
		checkTile(tile1);
		checkTile(tile2);
		Debug.Log("[MatchesValidator] Проверка на совпадения завершена");

		result = matchedTiles.Length >= 3;
		Debug.Log("[MatchesValidator] Результат: " + result);

		return result;
	}

	/// <summary>
	/// Поиск совпадений в столбце и строке для тайла tile и запись совпадений в массив matchedTiles[].
	/// </summary>
	/// <param name="tile">Tile.</param>
	public static void checkTile(Tile tile)
	{
		matchedTiles = addArrayToArray(matchedTiles, checkLineForMatches(tile, "R"));
		matchedTiles = addArrayToArray(matchedTiles, checkLineForMatches(tile, "C"));
	}

	/// <summary>
	/// Склеивает два массива тайлов в один и возвращает его.
	/// </summary>
	/// <returns>The array to array.</returns>
	/// <param name="sourceArray">Source array.</param>
	/// <param name="addingArray">Adding array.</param>
	static Tile[] addArrayToArray(Tile[] sourceArray, Tile[] addingArray)
	{
		if (addingArray == null) return sourceArray;

		Tile[] newArray = new Tile[sourceArray.Length + addingArray.Length];

		for (int i = 0; i < sourceArray.Length; i++) newArray[i] = sourceArray[i];
		for (int i = 0; i < addingArray.Length; i++) newArray[i + sourceArray.Length] = addingArray[i];

		return newArray;
	}

	/// <summary>
	/// Возвращает массив тайлов, которые совпали в линии.
	/// </summary>
	static Tile[] checkLineForMatches(Tile tile, string param)
	{
		Tile[] line = TilesFinder.getLine(tile, param);
		
		var matchedTilesInLine = new List<Tile>(0);

		/*
		 *	Алгоритм поиска совпадений 
		 * 
		 * 	Находим первый элемент, который совпал с цветом тайла.
		 * 	Добавляем его в список, если этого элемента там нет
		 * 	
		 * 	При несовпадении тайлов, если в список уже добавлены какие-либо элементы,
		 * 	проверяем количество элементов в списке для нахождения цепочек тайлов
		 * 	длинной более трех.
		 * 	Если же цепочка состоит из более трех элементов, то прерываем поиск.
		 * 
		 */

		//Цикл добавляет последовательно идущие элементы в список
		for (int i = 0; i < line.Length && line[i] != null; i++)
		{
			//Если цвета тайлов совпадают, то добавляем в список
			//Если в списке уже содержится этот тайл, то пропускаем.
			if (tile.color == line[i].color)
			{
				if(!matchedTilesInLine.Contains(line[i])) matchedTilesInLine.Add(line[i]);
				continue;
			}

			//Если цвета тайлов не совпадают

			//Если еще ничего не добавили в список, то смотрим на следующий тайл
			if (matchedTilesInLine.Count == 0) continue;

			//Если количество элементов списка меньше минимального, то очищаем список
			if (matchedTilesInLine.Count < 3)
			{
				matchedTilesInLine.Clear();
				continue;
			}

			break;
		}
		
		//Если меньше трех совпадений, то проверка не пройдена
		if (matchedTilesInLine.Count < 3) return null;

		//Если есть совпадения. Проверка пройдена
		return matchedTilesInLine.ToArray();
	}

	/// <summary>
	/// Проверит всю сетку тайлов на наличие совпадений и вернет их, если они есть. В противном слуае вернет null.
	/// </summary>
	public static void checkAllTileMapForMatches()
	{
		Debug.Log("[MatchesValidator] Проверка сетки тайлов на совпадения.");
		clearMatchedTile();

		var tilesGrid = TilesMap.getTilesGrid();

		for (int i = 0; i < TilesMap.gridWidth; i++)
		{
			for (int j = 0; j < TilesMap.gridHeight; j++)
			{
				checkTile(tilesGrid[i,j]);
			}
		}

		if (getMatchedTiles().Length == 0)
		{
			Debug.Log("[MatchesValidator] <color=yellow>Новых совпадений не найдено.</color>");
			return;
		}

		Debug.Log("[MatchesValidator]  <color=yellow>Обнаружены новые совпадения.</color>");
		MatchesDestroyer.destroyMatches(getMatchedTiles());
	}
}
