using UnityEngine;

public class ValidateObjectsInRawAndColumn : MonoBehaviour
{
	static TileMap tileMap;
	static ObjectsGenerator objectsGenerator;

	void Awake()
	{
		tileMap = GetComponent<TileMap>();
		objectsGenerator = GetComponent<ObjectsGenerator>();
	}

	public static bool canMoveTileObjects(TileObject first, TileObject second)
	{
		Vector2 firstPos = first.getParentNode().getPosition();
		Vector2 secondPos = second.getParentNode().getPosition();

		bool inRangeOfOneUnit = (firstPos - secondPos).magnitude.Equals(1.0f);
		if (!inRangeOfOneUnit) return false;

		bool areNotSimilarColors = first.color != second.color;
		if (!areNotSimilarColors) return false;

		bool haveMatches = checkRawAndColumnForMatches(first, secondPos);
		if (!haveMatches) return false;

		return true;
	}

	static bool checkRawAndColumnForMatches(TileObject firstObject, Vector2 secondPosition)
	{
		Debug.Log("<color=yellow>Проверка совпадений в столбце</color>");

		//if(secondPosition.y < tileMap.getHeight())
		//TileObject upperObjectAtSecondPosition = objectsGenerator.getTileObjectByPosition(secondPosition + Vector2.up);
		//if(upperObjectAtSecondPosition.color == firstObject.color)
		//{
		//	TileObject downerObjectAtSecondPosition = objectsGenerator.getTileObjectByPosition(secondPosition + Vector2.down);
		//	if (downerObjectAtSecondPosition.color == firstObject.color)
		//	{
		//		Debug.Log("<color=green>Проверка совпадений в столбце пройдена</color>");
		//		return true;
		//	}

		//	return false;
		//}


		return false;
		//return true;
	}

	//Задавать дельту как Vector2(0, -2)
	//Если в позиции delta возвращает null, то delta += Vector2.up
	//Инкремент возможен, пока delta <= Vector2(0, 2);
	//Исключить delta = Vector2.zero

	//Додумать
	static bool objectMatchesAtRange(TileObject Object, Vector2 position, Vector2 Range)
	{
		TileObject rangedObjectAtPosition = objectsGenerator.getTileObjectByPosition(position + Range);
		if (rangedObjectAtPosition.color == Object.color) return true;
		return false;
	}
}
