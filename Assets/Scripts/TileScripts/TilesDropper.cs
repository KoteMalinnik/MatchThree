using System.Collections;
using UnityEngine;

/// <summary>
/// Перемещение тайлов.
/// </summary>
public class TilesDropper
{
	public TilesDropper()
	{
		calculate();
		run();
	}

	public void calculate()
	{
		Debug.Log("[TilesDropper] Рассчет.");
		for (int i = 0; i < TilesMap.gridWidth; i++)
		{
			var downerTile = TilesFinder.getTileAtID(i, 0);
			var line = TilesFinder.getLine(downerTile, "C");
			var count = line.Length;
			Debug.Log($"Столбец {i}. Количество элементов: {count}");

			for (int j = 0; j < count; j++)
			{
				
			}
				Debug.Log(line[j].gridID);
		}
	}

	public void run()
	{
		Debug.Log("[TilesDropper] Запуск.");
	}

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

		var upperTile = findUpperTileInColoumn(tile);
		var replacer = new TilesReplacer();

		while (upperTile != null)
		{
			replacer.replaceTiles(upperTile, tile, false, 30f);
			yield return new WaitWhile(() => replacer.routine != null);

			upperTile = TilesFinder.getNearestTile(tile, 0, 1);
		}
		Debug.Log("[TilesDropper] <color=red>Падение тайлов завершено.</color>");
		routine = null;
		yield break;
	}

	Tile findUpperTileInColoumn(Tile downerTile)
	{
		var tilesInColoumn = TilesMap.gridHeight;

		var upperTile = new Tile();
		for (int i = 1; i < tilesInColoumn - (int)downerTile.gridID.y; i++)
		{
			upperTile = TilesFinder.getNearestTile(downerTile, 0, i);
			if (upperTile != null) break;
		}

		return upperTile;
	}
}
