using UnityEngine;

/// <summary>
/// Уничтожение совпавших тайлов.
/// </summary>
public class MatchesDestroyer : MonoBehaviour
{
	/// <summary>
	/// Уничтожает совпадения.
	/// </summary>
	/// <param name="tilesToDestroy">Tiles to destroy.</param>
	public static void destroyMatches(Tile[] tilesToDestroy)
	{
		Debug.Log("Удаление совпавших тайлов");

		for (int i = 0; i < tilesToDestroy.Length; i++)
		{
			dropUpperTiles(tilesToDestroy[i]);

			var positionToCreateTile = tilesToDestroy[i].position;

			Destroy(tilesToDestroy[i].gameObject);

			ObjectsGenerator.createTileAtPosition(positionToCreateTile);
		}
	}

	/// <summary>
	/// Опусткает все тайлы выше tile в столбце, а tile помещает на вершину столбца.
	/// </summary>
	/// <param name="tile">Tile.</param>
	static void dropUpperTiles(Tile tile)
	{
		var upperTile = TilesFinder.getNearestTile(tile, 0, 1);
		while(upperTile != null && !Input.GetKeyDown(KeyCode.Space))
		{
			TilesMovement.replaceTiles(tile, upperTile);
			upperTile = TilesFinder.getNearestTile(tile, 0, 1);
		}
	}
}
