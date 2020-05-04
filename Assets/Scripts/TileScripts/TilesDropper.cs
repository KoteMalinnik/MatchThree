using System.Collections;
using UnityEngine;

/// <summary>
/// Перемещение тайлов.
/// </summary>
public class TilesDropper
{
	/// <summary>
	/// Опускает все тайлы выше tile в столбце, а tile помещает на вершину столбца.
	/// </summary>
	/// <param name="tile">Tile.</param>
	public void dropUpperTiles(Tile tile)
	{
		routine = CoroutinePlayer.Instance.StartCoroutine(droppingTiles(tile));
	}

	public Coroutine routine { get; private set; } = null;
	IEnumerator droppingTiles(Tile tile)
	{
		Debug.Log("[TilesDropper] <color=red>Падение тайлов.</color>");

		var upperTile = TilesFinder.getNearestTile(tile, 0, 1);
		var replacer = new TilesReplacer();
		while (upperTile != null)
		{
			replacer.replaceTiles(upperTile, tile, false, 10f);
			yield return new WaitWhile(() => replacer.routine != null);

			upperTile = TilesFinder.getNearestTile(tile, 0, 1);
		}
		Debug.Log("[TilesDropper] <color=red>Падение тайлов завершено.</color>");
		routine = null;
		yield return null;
	}
}
