using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Класс проверки на совпадение объектов в строке или столбце
/// </summary>
public static class ValidateObjectsInRawAndColumn
{
	/// <summary>
	/// Возвращает true, если first и second можно поменять местами
	/// </summary>
	/// <returns><c>true</c>, if replace tile objects was caned, <c>false</c> otherwise.</returns>
	/// <param name="firstTile">First.</param>
	/// <param name="secondTile">Second.</param>
	public static bool couldReplaceTiles(TileObject secondTile, TileObject firstTile)
	{
		Debug.Log("Проверка тайлов на возможность перемещения");
		bool result = false;
		//Нет необходимости что-либо проверять, если второй объект на в пределах одного объекта по горизонтали или вертикали
		result = (firstTile.position - secondTile.position).magnitude < TileMap.tileDeltaPosition + 0.1f;
		if (!result) return false;
		Debug.Log("В пределах одного тайла");

		//Нет необходимости проверять, если они одного цвета
		result = firstTile.color != secondTile.color;
		if (!result) return false;
		Debug.Log("Разные цвета тайлов");


		Debug.Log("Проверка на совпадения");
		//Содержит совпавшие тайлы, которые надо будет уничтожить
		TileObject[] matchedTilesToDestroy = new TileObject[0];

		matchedTilesToDestroy = addArrayToArray(matchedTilesToDestroy, checkLineForMatches(firstTile, "C"));
		matchedTilesToDestroy = addArrayToArray(matchedTilesToDestroy, checkLineForMatches(firstTile, "R"));

		//Исключаем необходимость лишней проверки
		if (Mathf.Abs(firstTile.position.x - secondTile.position.x) < 0.01f) //Если оба тайла в одном столбце
			matchedTilesToDestroy = addArrayToArray(matchedTilesToDestroy, checkLineForMatches(secondTile, "R"));

		if (Mathf.Abs(firstTile.position.y - secondTile.position.y) < 0.01f) //Если оба тайла в одном ряду
			matchedTilesToDestroy = addArrayToArray(matchedTilesToDestroy, checkLineForMatches(secondTile, "C"));
		
		Debug.Log("Проверка на совпадения завершена");

		result = matchedTilesToDestroy.Length >= 3;
		Debug.Log("Результат: " + result);

		if (result) MathesDestroyer.destroyMatches(matchedTilesToDestroy);

		return result;
	}

	static TileObject[] addArrayToArray(TileObject[] sourceArray, TileObject[] addingArray)
	{
		if (addingArray == null) return sourceArray;

		TileObject[] newArray = new TileObject[sourceArray.Length + addingArray.Length];

		for (int i = 0; i < sourceArray.Length; i++) newArray[i] = sourceArray[i];
		for (int i = 0; i < addingArray.Length; i++) newArray[i + sourceArray.Length] = addingArray[i];

		return newArray;
	}

	/// <summary>
	/// Возвращает массив тайлов, с которыми произошло совпадение и при этом среди них есть tile.
	/// </summary>
	static TileObject[] checkLineForMatches(TileObject tile, string param)
	{
		TileObject[] line = TileFinder.getLine(tile, param);
		
		var mathedTiles = new List<TileObject>();

		//Цикл добавляет последовательно идущие элементы в список
		for (int i = 0; i < line.Length && line[i] != null; i++)
		{
			//Если прошли почти весь ряд, но не нашли совпадений, то выходим из цикла
			if (mathedTiles.Count == 0 && line.Length - i < 3) break;

			//Если цвета тайлов совпадают, то добавляем в список
			if (tile.color == line[i].color)
			{
				mathedTiles.Add(line[i]);
				continue;
			}

			//Если цвета тайлов не совпадают

			//Если еще ничего не добавили в список, то смотрим на следующий тайл
			if (mathedTiles.Count == 0) continue;

			//Если количество элементов списка меньше минимального, то очищаем список
			if (mathedTiles.Count < 3)
			{
				mathedTiles.Clear();
				continue;
			}

			//Если список содержит исходный тайл, то заканчиваем поиск
			if(mathedTiles.Contains(tile))
			{
				break;
			}

			//В противном случае очищаем список
			mathedTiles.Clear();
		}
		
		//Если меньше трех совпадений, то проверка не пройдена
		if (mathedTiles.Count < 3) return null;

		//Если массив содержит исходный объект, то проверка пройдена
		if (mathedTiles.Contains(tile)) return mathedTiles.ToArray();

		return null;
	}
}
