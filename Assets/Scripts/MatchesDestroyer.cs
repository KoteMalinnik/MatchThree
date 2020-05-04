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

		CoroutinePlayer.Instance.StartCoroutine(delayForDestroyingAllMatches());
	}

	/// <summary>
	/// Анимация уничтожения тайла.
	/// </summary>
	/// <param name="tileToDestroy">Tile to destroy.</param>
	static IEnumerator destroyTileAnimation(Tile tileToDestroy)
	{
		string tileName = tileToDestroy.name;
		//Debug.Log($"[MatchesDestroyer] Начало анимации уничтожения тайла {tileName}");

		var transform = tileToDestroy.transform;
		var animationSpeed = 30f;

		float debugTime = 0;

		var newScale = transform.localScale;

		for (; transform.localScale.x > 0.1f; debugTime += Time.deltaTime)
		{
			var deltaScale = animationSpeed * Time.deltaTime;
			newScale.x -= deltaScale;
			newScale.y -= deltaScale;

			transform.localScale = newScale;
			yield return new WaitForEndOfFrame();
		}

		MonoBehaviour.Destroy(tileToDestroy.gameObject);
		//Debug.Log($"[MatchesDestroyer] Конец анимации уничтожения. Тайл {tileName} уничтожен за время {debugTime}");
	}

	/// <summary>
	/// Задержка перед вызовом методов TilesDropper.
	/// </summary>
	static IEnumerator delayForDestroyingAllMatches()
	{
		//Debug.Log("[MatchesDestroyer] Начало ожидания спуска.");

		yield return new WaitForSeconds(0.4f);

		for (int coloumn = 0; coloumn < TilesMap.gridWidth; coloumn++)
		{
			var dropper = new TilesDropper(coloumn);
		}

		CoroutinePlayer.Instance.StartCoroutine(delayForAppearingAllTiles());
	}

	/// <summary>
	/// Задержка перед вызовом метода MatchesValidator.checkAllTileMapForMatches().
	/// </summary>
	static IEnumerator delayForAppearingAllTiles()
	{
		//Debug.Log("[MatchesDestroyer] Начало ожидания проверки всей карты.");

		yield return new WaitWhile(() => TilesDropper.CoroutinesInProcessCount > 0);

		MatchesValidator.checkAllTileMapForMatches();
		//Debug.Log("[MatchesDestroyer] Конец ожидания проверки всей карты.");
	}
}
