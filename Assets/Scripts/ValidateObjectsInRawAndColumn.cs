using UnityEngine;

/// <summary>
/// Класс проверки на совпадение объектов в строке или столбце
/// </summary>
public class ValidateObjectsInRawAndColumn : MonoBehaviour
{
	static TileMap tileMap;
	static ObjectsGenerator objectsGenerator;

	void Awake()
	{
		tileMap = GetComponent<TileMap>();
		objectsGenerator = GetComponent<ObjectsGenerator>();
	}

	/// <summary>
	/// Возвращает true, если first и second можно поменять местами
	/// </summary>
	/// <returns><c>true</c>, if replace tile objects was caned, <c>false</c> otherwise.</returns>
	/// <param name="first">First.</param>
	/// <param name="second">Second.</param>
	public static bool canReplaceTileObjects(TileObject first, TileObject second)
	{
		Vector2 firstPos = first.getParentNode().getPosition();
		Vector2 secondPos = second.getParentNode().getPosition();

		//Нет необходимости что-либо проверять, если второй объект на в пределах одного объекта по горизонтали или вертикали
		bool inRangeOfOneUnit = (firstPos - secondPos).magnitude.Equals(1.0f);
		if (!inRangeOfOneUnit) return false;

		//Нет необходимости проверять, если они одного цвета
		bool areNotSimilarColors = first.color != second.color;
		if (!areNotSimilarColors) return false;

		//Если у нас будет хотя бы одно совпадение в одном из трех случаев, то будет возвращено true, а иначе будет возвращено false
		bool haveMatches = checkLineForMatches(first, true);
		if (!haveMatches) haveMatches = checkLineForMatches(first, false);
		if (!haveMatches)
		{
			if (Equals(firstPos.x, secondPos.x)) checkLineForMatches(second, true);
			else if (Equals(firstPos.y, secondPos.y)) checkLineForMatches(second, false);
			else Debug.Log("<color=red>!!!ERROR!!! Условие неверно считало позиции");
		}

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
		Vector2 objPosition = obj.getParentNode().getPosition();

		//Достаточно трех совпадений, поэтому будет начинать за 2 объекта от исходного
		float newX = objPosition.x - 2 >= 0 ? objPosition.x - 2 : 0;
		float newY = objPosition.y - 2 >= 0 ? objPosition.y - 2 : 0;
		//Позиция проверяемого объекта
		Vector2 checkPosition = checkHorizontalLine ? new Vector2(newX, objPosition.y) : new Vector2(objPosition.x, newY);

		//Количество объектов в линии
		int objectsInLineCount = checkHorizontalLine ? tileMap.getWidth() : tileMap.getHeight();
		//Инкремент для цикла
		Vector2 delta = checkHorizontalLine ? Vector2.right : Vector2.up;

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

		return "null";
	}

}
