using UnityEngine;

/// <summary>
/// Класс перемещения двух объектов TileObject
/// </summary>
public static class TileObjectMovement
{
	/// <summary>
	/// Объект, который надо поменять местами
	/// </summary>
	static TileObject sourceTile;
	/// <summary>
	/// Объект, с которым надо поменять местами
	/// </summary>
	static TileObject targetTile;

	/// <summary>
	/// Первый вызов метода - установка currentObject.
	/// Второй вызов метода - установка targetObject. 
	/// </summary>
	/// <param name="tile">Object.</param>
	public static void setTiles(TileObject tile)
	{
		if (sourceTile == null)
		{
			setSourceTile(tile);
			return;
		}

		if (tile == sourceTile)
		{
			Debug.Log("Один и тот же объект");
			return;
		}

		setTargetTile(tile);

		sourceTile.transform.localScale /= 0.8f;
		replaceTilesIfPossible();
	}

	/// <summary>
	/// Установка перемещаемого объекта
	/// </summary>
	/// <param name="tile">Object.</param>
	static void setSourceTile(TileObject tile)
	{
		Debug.Log($"Исходный объект. Позиция: {tile.position}. Цвет: {colorToString(tile.color)}");
		sourceTile = tile;
		sourceTile.transform.localScale *= 0.8f;
	}


	/// <summary>
	/// Установка объекта, с которым требуется поменять местами перемещаемый объект
	/// </summary>
	/// <param name="tile">Object.</param>
	static void setTargetTile(TileObject tile)
	{
		Debug.Log($"Целевой объект. Позиция: {tile.position}. Цвет: {colorToString(tile.color)}");
		targetTile = tile;
	}

	/// <summary>
	/// Перемещает currentObject на место targetObject и наоборот, если есть совпадения в столбце или строке
	/// </summary>
	static void replaceTilesIfPossible()
	{
		Debug.Log("<color=yellow>Перемещение объектов</color>");

		replaceTiles();

		bool canReplaceTileObjects = ValidateMatches.couldReplaceTiles(sourceTile, targetTile);
		if (canReplaceTileObjects)
		{
			Debug.Log("<color=green>Перемещение объектов разрешено</color>");
			//Вызываю функцию проверки совпадений по всему ряду/столбцу для уничтожения объектов совпадения
		}
		else
		{
			Debug.Log("<color=red>Перемещение объектов невозможно. Возвращение в исходное состояние</color>");
			replaceTiles();
		}

		targetTile = null;
		sourceTile = null;
	}

	/// <summary>
	/// Меняет местами в сетке targetObject и currentObject
	/// </summary>
	static void replaceTiles()
	{
		var newTargetPosition = sourceTile.position;
		var newSourcePosition = targetTile.position;

		sourceTile.setTileParametrs(newSourcePosition, sourceTile.color);
		targetTile.setTileParametrs(newTargetPosition, targetTile.color);
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
