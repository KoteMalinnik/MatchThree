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
		replaceTilesIfPossible(ref sourceTile, ref targetTile);
	}

	/// <summary>
	/// Установка перемещаемого объекта
	/// </summary>
	/// <param name="tile">Object.</param>
	static void setSourceTile(TileObject tile)
	{
		Debug.Log($"Исходный объект. ID: {tile.gridID}. Цвет: {colorToString(tile.color)}");
		sourceTile = tile;
		sourceTile.transform.localScale *= 0.8f;
	}


	/// <summary>
	/// Установка объекта, с которым требуется поменять местами перемещаемый объект
	/// </summary>
	/// <param name="tile">Object.</param>
	static void setTargetTile(TileObject tile)
	{
		Debug.Log($"Целевой объект. ID: {tile.gridID}. Цвет: {colorToString(tile.color)}");
		targetTile = tile;
	}

	/// <summary>
	/// Перемещает currentObject на место targetObject и наоборот, если есть совпадения в столбце или строке
	/// </summary>
	static void replaceTilesIfPossible(ref TileObject tile1, ref TileObject tile2)
	{
		//Debug.Log("<color=yellow>Перемещение объектов</color>");

		replaceTiles(tile1, tile2);

		bool canReplaceTileObjects = ValidateMatches.couldReplaceTiles(sourceTile, targetTile);
		if (canReplaceTileObjects)
		{
			Debug.Log("<color=green>Перемещение объектов разрешено</color>");
			MatchesDestroyer.destroyMatches(ValidateMatches.getMatchedTiles());
			//Вызываю функцию проверки совпадений по всему ряду/столбцу для уничтожения объектов совпадения
		}
		else
		{
			Debug.Log("<color=red>Перемещение объектов невозможно. Возвращение в исходное состояние</color>");
			replaceTiles(tile1, tile2);
		}

		tile1 = null;
		tile2 = null;
	}

	/// <summary>
	/// Меняет местами в сетке.
	/// </summary>
	public static void replaceTiles(TileObject tile1, TileObject tile2)
	{
		var newTile2Position = tile1.position;
		var newTile1Position = tile2.position;

		tile1.setTileParametrs(newTile1Position, tile1.color);
		tile2.setTileParametrs(newTile2Position, tile2.color);
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
