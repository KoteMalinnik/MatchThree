using System.Collections;
using UnityEngine;

/// <summary>
/// Опускание тайлов на свободные места.
/// </summary>
public class TilesDropper
{
	/// <summary>
	/// Инициализация объекта класса TilesDropper. Рассчет дырок и запуск опускания тайлов.
	/// </summary>
	public TilesDropper()
	{
		calculate();
	}

	/// <summary>
	/// Рассчитывает дырки и запускает их обработку.
	/// </summary>
	public void calculate()
	{
		Debug.Log("[TilesDropper] Рассчет дырок.");
		for (int i = 0; i < TilesMap.gridWidth; i++)
		{
			var line = TilesFinder.getLine(i, "C");
			//Пропускаем полный столбец.
			if (line.Length == TilesMap.gridHeight) continue;

			var count = line.Length;
			Debug.Log($"[TilesDropper] Столбец {i}. Количество элементов: {count}");

			var higtherTileIDy = (int)line[count-1].gridID.y;
			var lowerTileIDy = (int)line[0].gridID.y;

			//Поиск дырки наверху столбца
			if (higtherTileIDy < TilesMap.gridHeight - 1) holeProcessing(TilesMap.gridHeight - 1, higtherTileIDy+1);

			//Поиск дырок в середине столбца
			for (int j = count - 1; j > 0; j--)
			{
				higtherTileIDy = (int)line[j].gridID.y;
				lowerTileIDy = (int)line[j-1].gridID.y;

				if(higtherTileIDy - lowerTileIDy > 1)
				{
					holeProcessing(higtherTileIDy-1, lowerTileIDy+1);
				}
			}

			//Поиск дырки внизу столбца
			if (lowerTileIDy > 0) holeProcessing(lowerTileIDy-1, 0);
		}
	}

	public void holeProcessing(int higtherTileIDy, int lowerTileIDy)
	{
		Debug.Log($"[TilesDropper] Обнаружена дырка: {higtherTileIDy} - {lowerTileIDy}. " +
							  $"Длина дырки: {higtherTileIDy - lowerTileIDy + 1}");
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
