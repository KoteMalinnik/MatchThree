using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		CoroutinePlayer.Instance.StartCoroutine(destroyTiles(tilesToDestroy));
	}

	static IEnumerator destroyTiles(Tile[] tilesToDestroy)
	{
		var generatedTiles = new List<Tile>();

		for (int i = 0; i < tilesToDestroy.Length; i++)
		{
			Debug.Log("[MatchesDestroyer] <color=red>Анимация уничтожения тайла.</color>");

			var transform = tilesToDestroy[i].transform;
			while(transform.localScale.x > 0.1f)
			{
				var k = 100 * Time.deltaTime;
				if (k >= 1) k = 0.9f;
				transform.localScale *= k;

				yield return new WaitForEndOfFrame();
			}

			var dropper = new TilesDropper();
			dropper.dropUpperTiles(tilesToDestroy[i]);
			yield return new WaitWhile(() => dropper.routine != null);

			var destroyedTilePosition = transform.position;

			MonoBehaviour.Destroy(tilesToDestroy[i].gameObject);
			Debug.Log("[MatchesDestroyer] <color=red>Тайл уничтожен.</color>");

			var newTile = TilesGenerator.createTileAtPosition(destroyedTilePosition);
		}

		MatchesValidator.checkAllTileMapForMatches();
	}
}
