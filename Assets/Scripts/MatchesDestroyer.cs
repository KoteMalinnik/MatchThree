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
			TilesReplacer.dropUpperTiles(tilesToDestroy[i]);
			var positionToCreateTile = tilesToDestroy[i].position;

			MonoBehaviour.Destroy(tilesToDestroy[i].gameObject);

			TilesGenerator.createTileAtPosition(positionToCreateTile);
		}

		MatchesValidator.checkAllTileMapForMatches();
	}
}
