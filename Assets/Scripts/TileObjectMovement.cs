using UnityEngine;

public class TileObjectMovement : MonoBehaviour
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

		if(obj == currentObject)
		{
			Debug.Log("Один и тот же объект");
			return;
		}

		setTargetTileObject(obj);

		MoveTileObjects();
	}

	//Нажатие на второй узел
	static void setTargetTileObject(TileObject obj)
	{
		Debug.Log("Установка целевого узла");
		targetObject = obj;
	}

	//Нажатие на первый узел
	static void setCurrentTileObject(TileObject obj)
	{
		Debug.Log("Установка текущего узла");
		currentObject = obj;
	}

	static void MoveTileObjects()
	{
		bool canMoveTileObjects = ValidateObjectsInRawAndColumn.canMoveTileObjects(currentObject, targetObject);
		if(canMoveTileObjects)
		{
			Debug.Log("<color=green>Перемещение объектов разрешено</color>");
			//Меняю объект currentObject местами с объектом targetObject
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
