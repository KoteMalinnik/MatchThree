using UnityEngine;

public class MathesDestroyer : MonoBehaviour
{
	public static void destroyMatches(TileObject[] tilesToDestroy)
	{
		Debug.Log("Удаление совпавших тайлов");

		for (int i = 0; i < tilesToDestroy.Length; i++)
			Destroy(tilesToDestroy[i].gameObject, 0.5f);
	}
}
