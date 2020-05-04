using UnityEngine;
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

		for (int i = 0; i < tilesToDestroy.Length; i++)
		{
			var positionToCreateTile = tilesToDestroy[i].position;

			Debug.Log("[MatchesDestroyer] <color=red>Тут должна быть анимация уничтожения тайла.</color>");
			MonoBehaviour.Destroy(tilesToDestroy[i].gameObject);

			var newTile = TilesGenerator.createTileAtPosition(positionToCreateTile);
			TilesDropper.dropUpperTiles(newTile);
		}

		MatchesValidator.checkAllTileMapForMatches();
	}
}
