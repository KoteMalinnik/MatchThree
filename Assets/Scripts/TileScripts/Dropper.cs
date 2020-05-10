using System.Collections;
using UnityEngine;

namespace Tiles
{
	/// <summary>
	/// Спуск тайлов на свободные места.
	/// </summary>
	public class Dropper
	{
		public static int CoroutinesInProcessCount { get; private set; } = 0;

		/// <summary>
		/// Инициализация объекта класса TilesDropper. Рассчет дырок и спуск тайлов.
		/// </summary>
		public Dropper(int coloumn)
		{
			calculateHoles(coloumn);
		}

		/// <summary>
		/// Ищет дырки в столбце.
		/// </summary>
		public void calculateHoles(int coloumn)
		{
			var line = Finder.getLine(coloumn, "C");
			var count = line.Length;

			//Пропускаем полный столбец.
			if (count == Map.gridHeight) return;

			//пропускаем пустой столбец
			if (count == 0) return;

			//Debug.Log($"[TilesDropper] Поиск дырок в столбце {coloumn}.");

			//Поиск дырки внизу столбца
			if ((int)line[0].gridID.y > 0)
			{
				holeProcessing(coloumn, 0, (int)line[0].gridID.y - 1);
				return;
			}

			//Поиск дырок в середине столбца
			for (int raw = 0; raw < count - 1; raw++)
			{
				var highterTileIDy = (int)line[raw + 1].gridID.y;
				var lowerTileIDy = (int)line[raw].gridID.y;

				if (highterTileIDy - lowerTileIDy > 1)
				{
					holeProcessing(coloumn, lowerTileIDy + 1, highterTileIDy - 1);
					return;
				}
			}

			//Debug.Log($"[TilesDropper] <color=green>В столбце {coloumn} дырок не обнаружено.</color>");

			MapGenerator.Instance.spawnTilesInColoumn(coloumn, Map.getTilesGrid());
		}

		/// <summary>
		/// Обработка дырки.
		/// </summary>
		public void holeProcessing(int coloumn, int holeLowerIDy, int holeHighterIDy)
		{
			var holeLenth = holeHighterIDy - holeLowerIDy + 1;

			//Debug.Log($"[TilesDropper] Обнаружена дырка: {holeHighterIDy} - {holeLowerIDy} " +
			//          $"в столбце {coloumn}. " +
			//          $"Длина дырки: {holeLenth}");

			var tileOverTheHole = Finder.getTileAtID(coloumn, holeHighterIDy + 1);
			var holeLowerPosition = new Vector2(coloumn, holeLowerIDy);

			CoroutinePlayer.Instance.StartCoroutine(droppingTiles(coloumn, tileOverTheHole, holeLowerPosition, holeLenth));
		}

		/// <summary>
		/// Спуск тайлов над дыркой.
		/// </summary>
		IEnumerator droppingTiles(int coloumn, Tile tileOverTheHole, Vector2 holeLowerPosition, int holeLength)
		{
			CoroutinesInProcessCount++;
			//Debug.Log($"[TilesDropper] <color=yellow>Спуск тайлов в столбце {coloumn}.</color>");

			var replacer = new Replacer();

			while (tileOverTheHole != null)
			{
				replacer.replaceTiles(tileOverTheHole, holeLowerPosition, animationSpeed: 10);
				yield return new WaitWhile(() => replacer.routine != null);

				tileOverTheHole = Finder.getNearestTile(tileOverTheHole, 0, holeLength + 1);
				holeLowerPosition.y++;
			}

			//Debug.Log($"[TilesDropper] <color=green>Спуск тайлов в столбце {coloumn} завершен.</color>");
			calculateHoles(coloumn);
			CoroutinesInProcessCount--;
		}
	}

}