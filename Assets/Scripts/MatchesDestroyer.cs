using UnityEngine;
using System.Collections;

/// <summary>
/// Уничтожение совпавших тайлов.
/// </summary>
public static class MatchesDestroyer
{
	/// <summary>
	/// Уничтожает совпадения.
	/// </summary>
	/// <param name="tilesToDestroy">Tiles to destroy.</param>
	public static void destroyMatches(Tile[] tilesToDestroy)
	{
		Debug.Log("[MatchesDestroyer] Удаление совпавших тайлов: " + tilesToDestroy.Length);

		for (int i = 0; i < tilesToDestroy.Length; i++)
		{
			CoroutinePlayer.Instance.StartCoroutine(destroyTileAnimation(tilesToDestroy[i]));
		}

		//var dropper = new TilesDropper();
		//dropper.startTemp();
	}

	static IEnumerator destroyTileAnimation(Tile tileToDestroy)
	{
		Debug.Log("[MatchesDestroyer] Начало анимации уничтожения.");
		var transform = tileToDestroy.transform;
		var animationSpeed = 30f;

		while(transform.localScale.x > 0.1f)
		{
			var newScale = transform.localScale;
			var deltaScale = animationSpeed * Time.deltaTime;
			newScale.x -= deltaScale;
			newScale.y -= deltaScale;

			transform.localScale = newScale;
			yield return new WaitForEndOfFrame();
		}

		MonoBehaviour.Destroy(tileToDestroy.gameObject);
		Debug.Log("[MatchesDestroyer] Конец анимации уничтожения. Тайл уничтожен");
	}
}
