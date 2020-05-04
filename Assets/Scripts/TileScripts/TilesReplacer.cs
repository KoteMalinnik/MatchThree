using System.Collections;
using UnityEngine;

/// <summary>
/// Перемещение тайлов.
/// </summary>
public class TilesReplacer
{
	/// <summary>
	/// Перемещает тайл в сетке на указанную позицию с анимацией, если immediately установлен в true.
	/// </summary>
	public void replaceTiles(Tile tile, Vector2 targetPosition, bool immediately = false, float animationSpeed = 5f)
	{
		routine = CoroutinePlayer.Instance.StartCoroutine(movementAnimation(tile, targetPosition, immediately, animationSpeed));
	}

	public Coroutine routine { get; private set; } = null;
	IEnumerator movementAnimation(Tile tile, Vector2 targetPosition, bool immediately = false, float animationSpeed = 5f)
	{
		float T = 0;

		if (!immediately)
		{
			yield return new WaitForEndOfFrame();

			while (tile.position != targetPosition && T < 1)
			{
				tile.setPosition(Vector2.Lerp(tile.position, targetPosition, T));

				T += animationSpeed * Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}

		tile.setTileParametrs(targetPosition, tile.color);

		routine = null;
	}
}
