using System.Collections;
using UnityEngine;

/// <summary>
/// Перемещение тайлов.
/// </summary>
public static class TilesDropper
{
	/// <summary>
	/// Опусткает все тайлы выше tile в столбце, а tile помещает на вершину столбца.
	/// </summary>
	/// <param name="tile">Tile.</param>
	public static void dropUpperTiles(Tile tile)
	{
		CoroutinePlayer.Instance.StartCoroutine(droppingTiles(tile));
	}

	static IEnumerator droppingTiles(Tile tile)
	{
		var tileColor = tile.color;
		tileColor.a = 0;

		var upperTile = TilesFinder.getNearestTile(tile, 0, 1);
		while (upperTile != null)
		{
			TilesReplacer.replaceTiles(tile, upperTile);
			//yield return new WaitWhile(() => TilesReplacer.routine != null);

			upperTile = TilesFinder.getNearestTile(tile, 0, 1);
		}

		Debug.Log("[TileRaplacer] <color=red>Тут должна быть анимация появления тайла.</color>");
		tileColor.a = 1;

		yield return null;
	}
}
