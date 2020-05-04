using UnityEngine;

/// <summary>
/// Ход игрока для перемещения двух тайлов.
/// </summary>
public static class PlayerMove
{
	/// <summary>
	/// Первый нажатый тайл.
	/// </summary>
	static Tile firstTile;
	/// <summary>
	/// Второй нажатый тайл.
	/// </summary>
	static Tile secondTile;

	/// <summary>
	/// Первый вызов метода - установка sourceTile.
	/// Второй вызов метода - установка targetTile. 
	/// </summary>
	/// <param name="tile">Тайл.</param>
	public static void setTilesForMove(Tile tile)
	{
		if (firstTile == null)
		{
			setFirstTile(tile);
			return;
		}

		if (tile == firstTile)
		{
			Debug.Log("[PlayerMove] Один и тот же объект");
			return;
		}

		setSecondTile(tile);

		firstTile.transform.localScale /= 0.8f;
		TilesReplacer.replaceTiles(firstTile, secondTile);
	}

	/// <summary>
	/// Установка первого тайла.
	/// </summary>
	/// <param name="tile">Тайл.</param>
	static void setFirstTile(Tile tile)
	{
		firstTile = tile;
		firstTile.transform.localScale *= 0.8f;
	}


	/// <summary>
	/// Установка второго тайла.
	/// </summary>
	/// <param name="tile">Тайл.</param>
	static void setSecondTile(Tile tile)
	{
		secondTile = tile;
	}

	/// <summary>
	/// Перемещает tile1 на место tile2.
	/// Если есть совпадения в столбце или строке после перемещения, то оставляет тайлы на этих местах.
	/// В противном случае снова перемещает тайлы.
	/// </summary>
	static void moveTiles(Tile tile1, Tile tile2)
	{
		bool canReplaceTileObjects = MatchesValidator.couldReplaceTiles(firstTile, secondTile);
		if (canReplaceTileObjects)
		{
			Debug.Log("[PlayerMove] <color=green>Перемещение объектов разрешено</color>");
			MatchesDestroyer.destroyMatches(MatchesValidator.getMatchedTiles());
			//Вызываю функцию проверки совпадений по всему ряду/столбцу для уничтожения объектов совпадения
		}
		else
		{
			Debug.Log("[PlayerMove] <color=red>Перемещение объектов невозможно. Возвращение в исходное состояние</color>");
			TilesReplacer.replaceTiles(tile1, tile2);
		}

		tile1 = null;
		tile2 = null;
	}
}