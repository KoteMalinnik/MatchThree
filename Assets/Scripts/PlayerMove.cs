using UnityEngine;
using System.Collections;
using Tiles;

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
		if (GoalsManagment.GoalsManager.GoalAtPlayerLevel.Moves <= 0) return;

		if (firstTile == null)
		{
			setFirstTile(tile);
			return;
		}

		if (tile == firstTile)
		{
			//Debug.Log("[PlayerMove] Один и тот же объект");
			return;
		}

		//Debug.Log("[PlayerMove] Установка второго объекта завершена");
		firstTile.transform.localScale /= 0.8f;

		CoroutinePlayer.Instance.StartCoroutine(moveTiles(firstTile, tile));
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
	static IEnumerator moveTiles(Tile tile1, Tile tile2)
	{
		//Debug.Log("[PlayerMove] <color=yellow>Перемещение объектов</color>");

		bool result = false;
		//Нет необходимости что-либо проверять, если второй объект не в пределах одного объекта по горизонтали или вертикали
		result = (tile2.position - tile1.position).magnitude < Map.tileDeltaPosition + 0.1f;
		if (!result) yield break;
		//Debug.Log("[PlayerMove] В пределах одного тайла");

		//Нет необходимости проверять, если они одного цвета
		result = tile2.color != tile1.color;
		if (!result) yield break;
		//Debug.Log("[PlayerMove] Разные цвета тайлов");


		var tile1Position = tile1.position;
		var tile2Position = tile2.position;

		var replacer1 = new Replacer();
		var replacer2 = new Replacer();

		replacer1.replaceTiles(tile1, tile2Position);
		replacer2.replaceTiles(tile2, tile1Position);

		yield return new WaitWhile(() => replacer1.routine != null);
		yield return new WaitWhile(() => replacer2.routine != null);

		bool canReplaceTileObjects = Matches.Validator.couldReplaceTiles(tile1, tile2);

		if (canReplaceTileObjects)
		{
			//Debug.Log("[PlayerMove] <color=green>Перемещение объектов разрешено</color>");
			GoalsManagment.GoalsManager.ProcessMoves();
			Matches.Destroyer.destroyMatches(Matches.Validator.getMatchedTiles().ToArray());
			yield break;
		}

		replacer1.replaceTiles(tile1, tile1Position);
		replacer2.replaceTiles(tile2, tile2Position);

		yield return new WaitWhile(() => replacer1.routine != null);
		yield return new WaitWhile(() => replacer2.routine != null);

		//Debug.Log("[PlayerMove] <color=red>Перемещение объектов невозможно.</color>");
		Replacer.setReplacePremission(true);
	}
}