using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Поверка совпадений тайлов в строке или столбце.
/// </summary>
public static class MatchesValidator
{
	//Содержит совпавшие тайлы, которые надо будет уничтожить
	static List<Tile> matchedTiles = new List<Tile>(0);
	public static List<Tile> getMatchedTiles() { return matchedTiles; }

	/// <summary>
	/// Возвращает true, если first и second можно поменять местами.
	/// </summary>
	/// <param name="tile2">First.</param>
	/// <param name="tile1">Second.</param>
	public static bool couldReplaceTiles(Tile tile1, Tile tile2)
	{
		//Debug.Log("[MatchesValidator] Проверка тайлов на возможность перемещения");
		bool result = false;

		//Debug.Log("[MatchesValidator] Проверка на совпадения");
		matchedTiles.Clear();

		checkTile(tile1, "C");
		checkTile(tile1, "R");

		checkTile(tile2, "C");
		checkTile(tile2, "R");
		//Debug.Log("[MatchesValidator] Проверка на совпадения завершена");

		result = matchedTiles.Count >= 3;
		//Debug.Log("[MatchesValidator] Результат: " + result);

		return result;
	}

	/// <summary>
	/// Поиск совпадений в столбце и строке для тайла tile и запись совпадений в массив matchedTiles[].
	/// </summary>
	/// <param name="tile">Tile.</param>
	public static void checkTile(Tile tile, string param)
	{
		var temp = checkLineForMatches(tile, param);
		if (temp != null)
			foreach(Tile t in temp)
			{
				if (matchedTiles.Contains(t)) continue;
				matchedTiles.Add(t);
			}
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
		matchedTiles.Clear();

		var tilesGrid = TilesMap.getTilesGrid();

		for (int i = 0; i < TilesMap.gridWidth; i++)
		{
			for (int j = 0; j < TilesMap.gridHeight; j++)
			{
				checkTile(tilesGrid[i, j], "C");
				checkTile(tilesGrid[i, j], "R");
			}
		}

		if (getMatchedTiles().Count == 0)
		{
			Debug.Log("[MatchesValidator] <color=yellow>Новых совпадений не найдено.</color>");
			return;
		}

		Debug.Log("[MatchesValidator]  <color=yellow>Обнаружены новые совпадения.</color>");
		MatchesDestroyer.destroyMatches(matchedTiles.ToArray());
	}
}
