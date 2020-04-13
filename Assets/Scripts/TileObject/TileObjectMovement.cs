using UnityEngine;

/// <summary>
/// Класс перемещения двух объектов TileObject
/// </summary>
public static class TileObjectMovement
{
	/// <summary>
	/// Объект, который надо поменять местами
	/// </summary>
	static TileObject currentObject;
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
		if (currentObject == null)
		{
			setCurrentTileObject(obj);
			return;
		}

		if (obj == currentObject)
		{
			Debug.Log("Один и тот же объект");
			return;
		}

		setTargetTileObject(obj);

		currentObject.transform.localScale /= 0.8f;
		ReplaceTileObjects();
	}

	/// <summary>
	/// Установка перемещаемого объекта
	/// </summary>
	/// <param name="obj">Object.</param>
	static void setCurrentTileObject(TileObject obj)
	{
		Debug.Log($"Установка текущего узла. Позиция: ({obj.position.x}, {obj.position.y})");
		currentObject = obj;
		currentObject.transform.localScale *= 0.8f;
	}


	/// <summary>
	/// Установка объекта, с которым требуется поменять местами перемещаемый объект
	/// </summary>
	/// <param name="obj">Object.</param>
	static void setTargetTileObject(TileObject obj)
	{
		Debug.Log($"Установка целевого узла. Позиция: ({obj.position.x}, {obj.position.y})");
		targetObject = obj;
	}

	/// <summary>
	/// Перемещает currentObject на место targetObject и наоборот, если есть совпадения в столбце или строке
	/// </summary>
	static void ReplaceTileObjects()
	{
		Debug.Log("<color=yellow>Перемещение объектов</color>");

		replaceObjects();

		bool canReplaceTileObjects = ValidateObjectsInRawAndColumn.canReplaceTileObjects(currentObject, targetObject);
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
		currentObject = null;
	}

	/// <summary>
	/// Меняет местами в сетке targetObject и currentObject
	/// </summary>
	static void replaceObjects()
	{
		var newTargetPosition = currentObject.position;
		currentObject.setTileObjectParametrs(targetObject.position, currentObject.color);
		targetObject.setTileObjectParametrs(newTargetPosition, targetObject.color);
	}
}
