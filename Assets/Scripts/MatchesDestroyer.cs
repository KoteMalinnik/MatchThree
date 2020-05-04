using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Уничтожение совпавших тайлов.
/// </summary>
public static class MatchesDestroyer
{
	static List<Tile> generatedTiles = new List<Tile>(); //вынести в другой класс

	/// <summary>
	/// Уничтожает совпадения.
	/// </summary>
	/// <param name="tilesToDestroy">Tiles to destroy.</param>
	public static void destroyMatches(Tile[] tilesToDestroy)
	{
		Debug.Log("Удаление совпавших тайлов");
		generatedTiles.Clear();

		for (int i = 0; i < tilesToDestroy.Length; i++)
		{
			TilesReplacer.dropUpperTiles(tilesToDestroy[i]);
			var positionToCreateTile = tilesToDestroy[i].position;

			MonoBehaviour.Destroy(tilesToDestroy[i].gameObject);

			//вынести в другой класс
			var newTile = TilesGenerator.createTileAtPosition(positionToCreateTile);
			generatedTiles.Add(newTile);
		}
	}


}
