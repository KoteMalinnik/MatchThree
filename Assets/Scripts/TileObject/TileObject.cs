using UnityEngine;

/// <summary>
/// Объект сетки
/// </summary>
public class TileObject : MonoBehaviour
{
	/// <summary>
	/// Позиция объекта в сетке tileMap
	/// </summary>
	/// <value>The position.</value>
	public Vector2 position { get; private set; } = new Vector2 (-1, -1);

	/// <summary>
	/// Цвет объекта
	/// </summary>
	/// <value>The color.</value>
	public Color color { get; private set; }

	SpriteRenderer spriteRenderer;

	public TileObject nearTile_Left { get; private set; } = null;
	public void setNearTile_Left(TileObject newNearTile_Left) { nearTile_Left = newNearTile_Left; }

	public TileObject nearTile_Bottom { get; private set; } = null;
	public void setNearTile_Bottom(TileObject newNearTile_Bottom) { nearTile_Bottom = newNearTile_Bottom; }

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		gameObject.AddComponent<BoxCollider2D>();

		transform.localScale = Vector3.one * TileMap.tileSize;
	}

	/// <summary>
	/// Установка значений объекта
	/// </summary>
	/// <param name="newPosition">New position.</param>
	/// <param name="newColor">New color.</param>
	public void setTileObjectParametrs(Vector2 newPosition, Color newColor, TileObject newNearTile_Left, TileObject newNearTile_Bottom)
	{
		color = newColor;
		if (spriteRenderer != null) spriteRenderer.color = color;

		setPosition(newPosition);

		setNearTile_Left(newNearTile_Left);
		setNearTile_Bottom(newNearTile_Bottom);

		gameObject.name = position.ToString();
	}

	void setPosition(Vector2 newPosition)
	{
		position = newPosition;
		transform.position = newPosition;
	}
}
