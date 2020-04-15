﻿using UnityEngine;

/// <summary>
/// Класс проверки на совпадение объектов в строке или столбце
/// </summary>
public class ValidateObjectsInRawAndColumn : MonoBehaviour
{
	static ObjectsGenerator objectsGenerator;
	static float tileDeltaPosition;
	static int mapWidht = TileMap.mapWidht;
	static int mapHeight = TileMap.mapHeight;

	void Start()
	{
		tileDeltaPosition = TileMap.tileDeltaPosition;
		mapWidht = TileMap.mapWidht;
		mapHeight = TileMap.mapHeight;
		objectsGenerator = GetComponent<ObjectsGenerator>();
	}

	/// <summary>
	/// Возвращает true, если first и second можно поменять местами
	/// </summary>
	/// <returns><c>true</c>, if replace tile objects was caned, <c>false</c> otherwise.</returns>
	/// <param name="firstTile">First.</param>
	/// <param name="secondTile">Second.</param>
	public static bool couldReplaceTiles(TileObject firstTile, TileObject secondTile)
	{
		Vector2 firstTilePosition = firstTile.position;
		Vector2 secondTilePosition = secondTile.position;

		//Нет необходимости что-либо проверять, если второй объект на в пределах одного объекта по горизонтали или вертикали
		bool inRangeOfOneTile = (firstTilePosition - secondTilePosition).magnitude < tileDeltaPosition + 0.1f;
		if (!inRangeOfOneTile) return false;
		Debug.Log("В пределах одного тайла");

		//Нет необходимости проверять, если они одного цвета
		bool colorsAreDifferent = firstTile.color != secondTile.color;
		if (!colorsAreDifferent) return false;
		Debug.Log("Разные цвета тайлов");

		//Если у нас будет хотя бы одно совпадение в одном из четырех случаев, то будет возвращено true, а иначе будет возвращено false
		bool haveMatches = checkLineForMatches(firstTile, true);
		if (!haveMatches) haveMatches = checkLineForMatches(firstTile, false);
		if (!haveMatches) haveMatches = checkLineForMatches(firstTile, true);
		if (!haveMatches) haveMatches = checkLineForMatches(secondTile, false);
		if (!haveMatches) haveMatches = checkLineForMatches(secondTile, true);
		Debug.Log("Проверка на совпадения завершена с результатом: " + haveMatches);

		return haveMatches;
	}

	/// <summary>
	/// Возвращает true, если в строке или столбце объекта obj совпадение хотя бы трех объектов по цвету с obj.
	/// </summary>
	/// <returns><c>true</c>, if line for matches was checked, <c>false</c> otherwise.</returns>
	/// <param name="obj">Object.</param>
	/// <param name="checkHorizontalLine">Если <c>true</c> то проверка будет производиться в строке, иначе - в столбце.</param>
	static bool checkLineForMatches(TileObject obj, bool checkHorizontalLine)
	{
		//Получаю позицию объекта, столбец или строку которого надо проверить
		Vector2 objPosition = obj.position;

		//Достаточно трех совпадений, поэтому будет начинать за 2 объекта от исходного
		float newX = objPosition.x - 2 * tileDeltaPosition >= 0 ? objPosition.x - 2 * tileDeltaPosition : 0;
		float newY = objPosition.y - 2 * tileDeltaPosition >= 0 ? objPosition.y - 2 * tileDeltaPosition : 0;
		//Позиция проверяемого объекта
		Vector2 checkPosition = checkHorizontalLine ? new Vector2(newX, objPosition.y) : new Vector2(objPosition.x, newY);

		//Количество объектов в линии
		int objectsInLineCount = checkHorizontalLine ? mapWidht : mapHeight;
		//Инкремент для цикла
		Vector2 delta = checkHorizontalLine ? Vector2.right : Vector2.up;
		delta *= tileDeltaPosition;

		//Счетчик объектов, которые совпали по цвету с исходным
		int matchedCounter = 0;
		//Результат проверки
		bool result = false;

		for (int i = 0; i < objectsInLineCount; i++, checkPosition += delta)
		{
			TileObject checkingObject = objectsGenerator.getTileObjectByPosition(checkPosition);
			if (checkingObject == null) continue;

			if (checkingObject.color == obj.color)
			{
				matchedCounter++;
				result |= checkingObject == obj;

				//Заканчиваем проверку, если есть достаточно совпадений и в них содержиться исходный объект
				if (matchedCounter > 2 && result) break;
			}
			else
			{
				matchedCounter = 0;
				result = false;
			}
		}

		//Проверка провалена, если количество объектов меньше трех и они не содержат исходный объект
		result &= matchedCounter >= 3;
		return result;
	}

	/// <summary>
	/// Преобразование Color в string. Например, Color.red вернет "red"
	/// </summary>
	/// <returns>The to string.</returns>
	/// <param name="color">Color.</param>
	static string colorToString(Color color)
	{
		if (color == Color.red) return "red";
		if (color == Color.green) return "green";
		if (color == Color.blue) return "blue";
		if (color == new Color(1, 1, 0, 1)) return "yellow";

		return "null";
	}
}
