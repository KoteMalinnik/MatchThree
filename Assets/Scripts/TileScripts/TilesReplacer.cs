using System.Collections;
using UnityEngine;

/// <summary>
/// Перемещение тайлов.
/// </summary>
public class TilesReplacer
{
	/// <summary>
	/// Меняет местами тайлы в сетке с анимацией, если immediately установлен в true.
	/// </summary>
	public void replaceTiles(Tile tile1, Tile tile2, bool immediately = true, float animationSpeed = 5f)
	{
		routine = CoroutinePlayer.Instance.StartCoroutine(movementAnimation(tile1, tile2, immediately, animationSpeed));
	}

	public Coroutine routine { get; private set; } = null;
	IEnumerator movementAnimation(Tile tile1, Tile tile2, bool immediately = true, float animationSpeed = 5f)
	{
		var newTile1Position = tile2.position;
		var newTile2Position = tile1.position;

		float T = 0;

		if(!immediately)
		{
			yield return new WaitForEndOfFrame();

			while (tile1.position != newTile1Position && T < 1)
			{
				tile1.setPosition(Vector2.Lerp(tile1.position, newTile1Position, T));
				tile2.setPosition(Vector2.Lerp(tile2.position, newTile2Position, T));

				T += animationSpeed * Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}

		tile1.setTileParametrs(newTile1Position, tile1.color);
		tile2.setTileParametrs(newTile2Position, tile2.color);

		routine = null;
	}
}
