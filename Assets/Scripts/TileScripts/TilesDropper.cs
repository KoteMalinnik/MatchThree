using System.Collections;
using UnityEngine;

/// <summary>
/// Спуск тайлов на свободные места.
/// </summary>
public class TilesDropper
{
	/// <summary>
	/// Инициализация объекта класса TilesDropper. Рассчет дырок и спуск тайлов.
	/// </summary>
	public TilesDropper(int coloumn)
	{
		calculateHoles(coloumn);
	}

	/// <summary>
	/// Рассчитывает дырки и спускает тайлы выше дырок.
	/// </summary>
	public void calculateHoles(int coloumn)
	{
		//Debug.Log($"[TilesDropper] Рассчет дырок в столбце {coloumn}.");

		var line = TilesFinder.getLine(coloumn, "C");
		var count = line.Length;

		//Пропускаем полный столбец.
		if (count == TilesMap.gridHeight) 
		{
			//Debug.Log($"[TilesDropper] <color=green>В столбце {coloumn} дырок не обнаружено.</color>");
			return;
		}

		//пропускаем пустой столбец
		if(count == 0)
		{
			return;
		}

		//Поиск дырки внизу столбца
		if ((int)line[0].gridID.y > 0)
		{
			holeProcessing(coloumn, 0, (int)line[0].gridID.y - 1);
			return;
		}
		    
		//Поиск дырок в середине столбца
		for (int raw = 0; raw < count - 1; raw++)
		{
			var highterTileIDy = (int)line[raw+1].gridID.y;
			var lowerTileIDy = (int)line[raw].gridID.y;

			if(highterTileIDy - lowerTileIDy > 1)
			{
				holeProcessing(coloumn, lowerTileIDy + 1, highterTileIDy - 1);
				return;
			}
		}

		//Debug.Log($"[TilesDropper] <color=green>В столбце {coloumn} дырок не обнаружено.</color>");
	}

	public void holeProcessing(int coloumn, int holeLowerIDy, int holeHighterIDy)
	{
		var holeLenth = holeHighterIDy - holeLowerIDy + 1;

		Debug.Log($"[TilesDropper] Обнаружена дырка: {holeHighterIDy} - {holeLowerIDy} " +
		          $"в столбце {coloumn}. " +
		          $"Длина дырки: {holeLenth}");

		var tileOverTheHole = TilesFinder.getTileAtID(coloumn, holeHighterIDy + 1);
		var holeLowerPosition = new Vector2(coloumn, holeLowerIDy);

		CoroutinePlayer.Instance.StartCoroutine(droppingTiles(coloumn, tileOverTheHole, holeLowerPosition, holeLenth));
	}

	IEnumerator droppingTiles(int coloumn, Tile tileOverTheHole, Vector2 holeLowerPosition, int holeLength)
	{
		Debug.Log($"[TilesDropper] <color=yellow>Спуск тайлов в столбце {coloumn}.</color>");

		var replacer = new TilesReplacer();

		while (tileOverTheHole != null)
		{
			replacer.replaceTiles(tileOverTheHole, holeLowerPosition, animationSpeed: 10);
			yield return new WaitWhile(() => replacer.routine != null);

			tileOverTheHole = TilesFinder.getNearestTile(tileOverTheHole, 0, holeLength+1);
			holeLowerPosition.y++;
		}

		Debug.Log($"[TilesDropper] <color=green>Спуск тайлов в столбце {coloumn} завершен.</color>");
		calculateHoles(coloumn);
	}
}
