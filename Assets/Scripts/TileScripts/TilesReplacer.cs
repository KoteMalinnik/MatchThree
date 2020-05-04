using System.Collections;
using UnityEngine;

/// <summary>
/// Перемещение тайлов.
/// </summary>
public class TilesReplacer
{
	/// <summary>
	/// Разрешение перемещения тайлов игроком. True, если разрешено.
	/// </summary>
	public static bool replacePremission { get; private set; } = true;
	public static void setReplacePremission(bool newPremission) { replacePremission = newPremission; }

	/// <summary>
	/// Перемещает тайл в сетке на указанную позицию с анимацией, если immediately установлен в true.
	/// </summary>
	public void replaceTiles(Tile tile, Vector2 targetPosition, bool immediately = false, float animationSpeed = 5f)
	{
		if(replacePremission) replacePremission = false;

		routine = CoroutinePlayer.Instance.StartCoroutine(movementAnimation(tile, targetPosition, immediately, animationSpeed));
	}

	public Coroutine routine { get; private set; } = null;
	IEnumerator movementAnimation(Tile tile, Vector2 targetPosition, bool immediately = false, float animationSpeed = 5f)
	{
		TilesMap.removeTileFromGrid(tile);

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
