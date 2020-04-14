using UnityEngine;

/// <summary>
/// Класс перемещения двух объектов TileObject
/// </summary>
public static class TileObjectMovement
{
	/// <summary>
	/// Объект, который надо поменять местами
	/// </summary>
	static TileObject sourceObject;
	/// <summary>
	/// Объект, с которым надо поменять местами
	/// </summary>
	static TileObject targetObject;

	/// <summary>
	/// Первый вызов метода - установка currentObject.
	/// Второй вызов метода - установка targetObject. 
	/// </summary>
	/// <param name="obj">Object.</param>
	public static void setObject(TileObject obj)
	{
		if (sourceObject == null)
		{
			setSourceTileObject(obj);
			return;
		}

		if (obj == sourceObject)
		{
			Debug.Log("Один и тот же объект");
			return;
		}

		setTargetTileObject(obj);

		sourceObject.transform.localScale /= 0.8f;
		ReplaceTileObjects();
	}

	/// <summary>
	/// Установка перемещаемого объекта
	/// </summary>
	/// <param name="obj">Object.</param>
	static void setSourceTileObject(TileObject obj)
	{
		Debug.Log($"Исходный объект. Позиция: {obj.position}. Цвет: {colorToString(obj.color)}");
		sourceObject = obj;
		sourceObject.transform.localScale *= 0.8f;
	}


	/// <summary>
	/// Установка объекта, с которым требуется поменять местами перемещаемый объект
	/// </summary>
	/// <param name="obj">Object.</param>
	static void setTargetTileObject(TileObject obj)
	{
		Debug.Log($"Целевой объект. Позиция: {obj.position}. Цвет: {colorToString(obj.color)}");
		targetObject = obj;
	}

	/// <summary>
	/// Перемещает currentObject на место targetObject и наоборот, если есть совпадения в столбце или строке
	/// </summary>
	static void ReplaceTileObjects()
	{
		Debug.Log("<color=yellow>Перемещение объектов</color>");

		replaceObjects();

		bool canReplaceTileObjects = ValidateObjectsInRawAndColumn.canReplaceTileObjects(sourceObject, targetObject);
		if (canReplaceTileObjects)
		{
			Debug.Log("<color=green>Перемещение объектов разрешено</color>");
			//Вызываю функцию проверки совпадений по всему ряду/столбцу для уничтожения объектов совпадения
		}
		else
		{
			Debug.Log("<color=red>Перемещение объектов невозможно. Возвращение в исходное состояние</color>");
			replaceObjects();
		}

		targetObject = null;
		sourceObject = null;
	}

	/// <summary>
	/// Меняет местами в сетке targetObject и currentObject
	/// </summary>
	static void replaceObjects()
	{
		var newTargetPosition = sourceObject.position;
		sourceObject.setTileObjectParametrs(targetObject.position, sourceObject.color);
		targetObject.setTileObjectParametrs(newTargetPosition, targetObject.color);
	}

	static string colorToString(Color color)
	{
		if (color == Color.red) return "red";
		if (color == Color.green) return "green";
		if (color == Color.blue) return "blue";
		if (color == new Color(1, 1, 0, 1)) return "yellow";

		return "null";
	}
}
