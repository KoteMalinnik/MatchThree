using UnityEngine;

public static class TileObjectMovement
{
	static TileObject targetObject;
	static TileObject currentObject;

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
		MoveTileObjects();
	}

	//Нажатие на второй узел
	static void setTargetTileObject(TileObject obj)
	{
		Debug.Log("Установка целевого узла. ID = " + obj.getParentNode().ID);
		targetObject = obj;
	}

	//Нажатие на первый узел
	static void setCurrentTileObject(TileObject obj)
	{
		Debug.Log("Установка текущего узла. ID = "+ obj.getParentNode().ID);
		currentObject = obj;
		currentObject.transform.localScale *= 0.8f;
	}

	static void MoveTileObjects()
	{
		bool canMoveTileObjects = ValidateObjectsInRawAndColumn.canMoveTileObjects(currentObject, targetObject);
		if (canMoveTileObjects)
		{
			Debug.Log("<color=green>Перемещение объектов разрешено</color>");

			var newTargetParentTileNode = currentObject.getParentNode();

			currentObject.setTileObjectParametrs(targetObject.getParentNode(), currentObject.color);
			targetObject.setTileObjectParametrs(newTargetParentTileNode, targetObject.color);

			//Вызываю функцию проверки совпадений по всему ряду/столбцу
		}
		else
		{
			Debug.Log("<color=red>Перемещение объектов невозможно</color>");
		}

		targetObject = null;
		currentObject = null;
	}
}
