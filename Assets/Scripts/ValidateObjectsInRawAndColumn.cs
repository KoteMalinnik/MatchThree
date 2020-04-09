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

		bool inRangeOfOneUnit = (firstPos - secondPos).magnitude.Equals(1.0f);
		if (!inRangeOfOneUnit) return false;

		bool areNotSimilarColors = first.color != second.color;
		if (!areNotSimilarColors) return false;

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
		//Debug.Log($"<color=yellow>Проверка совпадений на линии. Цвет {colorToString(obj.color)}</color>");

		Vector2 objPosition = obj.getParentNode().getPosition();
		Vector2 checkPosition = checkHorizontalLine ? new Vector2(0, objPosition.y) : new Vector2(objPosition.x, 0);

		//Debug.Log(horizontalLineCheck ? $"Строка {checkPosition.y + 1}" : $"Столбец {checkPosition.x + 1}");

		int objectsInLineCount = checkHorizontalLine ? tileMap.getWidth() : tileMap.getHeight();
		Vector2 delta = checkHorizontalLine ? Vector2.right : Vector2.up;

		TileObject[] matchedObjects = new TileObject[0];
		bool matchedObjectsContainsObj = false;

		for (int i = 0; i < objectsInLineCount; i++)
		{
			TileObject checkingObject = objectsGenerator.getTileObjectByPosition(checkPosition);
			if (checkingObject == null) continue;

			//string debug = $"Позиция: {checkingObject.getParentNode().getID()}. " +
			//		  $"Цвет: {colorToString(checkingObject.color)}. " +
			//		  $"Является исходным: {checkingObject == obj}.";

			if (checkingObject.color == obj.color)
			{
				var temp = matchedObjects;
				matchedObjects = new TileObject[matchedObjects.Length + 1];
				for (int k = 0; k < temp.Length; k++) matchedObjects[k] = temp[k];

				matchedObjects[matchedObjects.Length - 1] = checkingObject;

				if(matchedObjectsContainsObj == false) matchedObjectsContainsObj = checkingObject == obj;

				//debug += $" <color=green>Совпадение объектов. Количество совпадений: {matchedObjects.Length}. Содержит исходный: {matchedObjectsContainsObj}</color>";

				//Заканчиваем проверку, если есть достаточно совпадений
				if (matchedObjects.Length > 2 && matchedObjectsContainsObj)
				{
					//Debug.Log("Проверка закончена. " + debug);
					break;
				}
			}
			else
			{
				matchedObjects = new TileObject[0];
				matchedObjectsContainsObj = false;
				//debug += $"<color=red>Объекты не совпали. Количество совпадений: {matchedObjects.Length}</color>";
			}

			//Debug.Log(debug);
			checkPosition += delta;
		}

		matchedObjectsContainsObj = matchedObjects.Length > 2;

		//Debug.Log($"<color=yellow>Возврат {matchedObjectsContainsObj}</color>");
		return matchedObjectsContainsObj;
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
