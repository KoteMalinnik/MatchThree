using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summary>
/// Создание объекта тайла.
/// </summary>
public static class TileCreator
{
	/// <summary>
	/// Создает тайл в позиции.
	/// </summary>
	/// <param name="position">Position.</param>
	public static Tile createTileAtPosition(Tile tilePrefab, Vector2 position, Transform parentTransform, Color[] colors)
	{
		Tile tile = MonoBehaviour.Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
		tile.transform.parent = parentTransform;

		var leftTileID = TilesFinder.getIDByPosition(position) + Vector2.left;
		Tile leftTile = TilesFinder.getTileAtID(leftTileID.x, leftTileID.y);

		var bottomTileID = TilesFinder.getIDByPosition(position) + Vector2.down;
		Tile bottomTile = TilesFinder.getTileAtID(bottomTileID.x, bottomTileID.y);

		var color = getUniqueColor(leftTile, bottomTile, colors);

		tile.setTileParametrs(position, color);

		CoroutinePlayer.Instance.StartCoroutine(appearTileAnimation(tile));

		return tile;
	}

	/// <summary>
	/// Анимация появления тайла.
	/// </summary>
	/// <param name="tileToAppear">Tile to appear.</param>
	static IEnumerator appearTileAnimation(Tile tileToAppear)
	{
		string tileName = tileToAppear.name;
		//Debug.Log($"[TileCreator] Начало анимации появления тайла {tileName}");

		var transform = tileToAppear.transform;
		var animationSpeed = 30f;

		float debugTime = 0;

		var newScale = Vector2.zero;
		transform.localScale = newScale;

		for (; transform.localScale.x < TilesMap.tileSize && debugTime < 0.4f; debugTime += Time.deltaTime)
		{
			var deltaScale = animationSpeed * Time.deltaTime;
			newScale.x += deltaScale;
			newScale.y += deltaScale;

			transform.localScale = newScale;

			yield return new WaitForEndOfFrame();
		}

		newScale.x = TilesMap.tileSize;
		newScale.y = TilesMap.tileSize;

		transform.localScale = newScale;
		//Debug.Log($"[TileCreator] Конец анимации появления. Тайл {tileName} появился за время {debugTime}");
	}

	/// <summary>
	/// Возвращает цвет, который не имеют соседние тайлы
	/// </summary>
	static Color getUniqueColor(Tile leftTile, Tile bottomTile, Color[] colors)
	{
		var resultColor = new Color();

		var availableColors = new List<Color>(colors.Length);
		for (int i = 0; i < colors.Length; i++) availableColors.Add(colors[i]);

		if (leftTile != null)
		{
			Color leftTileColor = leftTile.color;
			if (availableColors.Contains(leftTileColor)) availableColors.Remove(leftTileColor);
		}

		if (bottomTile != null)
		{
			Color bottomTileColor = bottomTile.color;
			if (availableColors.Contains(bottomTileColor)) availableColors.Remove(bottomTileColor);
		}

		resultColor = availableColors[Random.Range(0, availableColors.Count)];
		return resultColor;
	}

}
