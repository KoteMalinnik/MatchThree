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

		bool haveMatches =
			checkLineForMatches(first.color, secondPos, true)  ||
			checkLineForMatches(second.color, firstPos, true)  ||
			checkLineForMatches(first.color, secondPos, false) ||
			checkLineForMatches(second.color, firstPos, false);
		if (!haveMatches) return false;

		return true;
	}

	static int delta = -2;
	static int lineCounter;

	static bool checkLineForMatches(Color targetObjectColor, Vector2 anotherObjectPosition, bool horizontalLineCheck)
	{
		Debug.Log("<color=yellow>Проверка совпадений на линии</color>");

		delta = -2;
		lineCounter = 0;

		while (delta <= 2)
		{
			var newPosition = horizontalLineCheck ?
				new Vector2(anotherObjectPosition.x + delta, anotherObjectPosition.y) :
				new Vector2(anotherObjectPosition.x, anotherObjectPosition.y + delta);

			var checkingObject = objectsGenerator.getTileObjectByPosition(newPosition);

			if(checkingObject == null)
			{
				delta++;
				if (delta == 0) delta++;
				continue;
			}

			if (checkingObject.color == targetObjectColor) lineCounter++;
			else lineCounter = 0;

			if (lineCounter == 2)
			{
				Debug.Log("Совпадение есть!");
				return true;
			}

			delta++;
			if (delta == 0) delta++;
		}

		Debug.Log("Совпадений нет");
		return false;
	}
}
