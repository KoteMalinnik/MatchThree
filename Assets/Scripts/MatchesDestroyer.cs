using UnityEngine;

/// <summary>
/// Уничтожение совпавших тайлов.
/// </summary>
public class MatchesDestroyer : MonoBehaviour
{
	public static void destroyMatches(TileObject[] tilesToDestroy)
	{
		Debug.Log("Удаление совпавших тайлов");

		for (int i = 0; i < tilesToDestroy.Length; i++)
		{
			var positionToCreateTile = tilesToDestroy[i].position;
			Destroy(tilesToDestroy[i].gameObject, 0.5f);
			ObjectsGenerator.createTileAtPosition(positionToCreateTile);
		}
	}
}
