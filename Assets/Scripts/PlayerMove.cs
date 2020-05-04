using UnityEngine;
using System.Collections;

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
	/// Первый вызов метода - установка первого тайла.
	/// Второй вызов метода - установка второго тайла и запуск обработки. 
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

		Debug.Log("Установка второго объекта завершена");
		firstTile.transform.localScale /= 0.8f;

		moveTiles(firstTile, tile);
		firstTile = null;
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
	/// Перемещает tile1 на место tile2.
	/// Если есть совпадения в столбце или строке после перемещения, то оставляет тайлы на этих местах.
	/// В противном случае снова перемещает тайлы.
	/// </summary>
	static void moveTiles(Tile tile1, Tile tile2)
	{
		Debug.Log("[PlayerMove] <color=yellow>Перемещение объектов</color>");

		TilesReplacer.replaceTiles(tile1, tile2);

		bool canReplaceTileObjects = MatchesValidator.couldReplaceTiles(tile1, tile2);
		if (canReplaceTileObjects)
		{
			Debug.Log("[PlayerMove] <color=green>Перемещение объектов разрешено</color>");
			MatchesDestroyer.destroyMatches(MatchesValidator.getMatchedTiles().ToArray());
			return;
		}

		Debug.Log("[PlayerMove] <color=red>Перемещение объектов невозможно. Возвращение в исходное состояние</color>");
		TilesReplacer.replaceTiles(tile1, tile2);
	}
}