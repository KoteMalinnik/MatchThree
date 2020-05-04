using System.Collections;
using UnityEngine;

/// <summary>
/// Перемещение тайлов.
/// </summary>
public static class TilesReplacer
{
	/// <summary>
	/// Опусткает все тайлы выше tile в столбце, а tile помещает на вершину столбца.
	/// </summary>
	/// <param name="tile">Tile.</param>
	public static void dropUpperTiles(Tile tile)
	{
		var upperTile = TilesFinder.getNearestTile(tile, 0, 1);
		while (upperTile != null)
		{
			replaceTiles(tile, upperTile);
			upperTile = TilesFinder.getNearestTile(tile, 0, 1);
		}
	}

	/// <summary>
	/// Меняет местами тайлы в сетке.
	/// </summary>
	public static void replaceTiles(Tile tile1, Tile tile2)
	{
		var newTile1Position = tile2.position;
		var newTile2Position = tile1.position;

		tile1.setTileParametrs(newTile1Position, tile1.color);
		tile2.setTileParametrs(newTile2Position, tile2.color);
	}

	static IEnumerator movementAnimation(Tile tile1, Tile tile2, bool immediately = true)
	{
		var newTile1Position = tile2.position;
		var newTile2Position = tile1.position;

		float animationSpeed = 5f;
		float T = 0;

		if(!immediately)
		{
			yield return new WaitForEndOfFrame();

			while (tile1.position != newTile1Position || T > 1)
			{
				tile1.setPosition(Vector2.Lerp(tile1.position, newTile1Position, T));
				tile2.setPosition(Vector2.Lerp(tile2.position, newTile2Position, T));

				T += animationSpeed * Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}

		tile1.setTileParametrs(newTile1Position, tile1.color);
		tile2.setTileParametrs(newTile2Position, tile2.color);

		yield return null;
	}
}
