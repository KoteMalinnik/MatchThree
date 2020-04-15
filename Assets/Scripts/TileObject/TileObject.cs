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
	public void setTileObjectParametrs(Vector2 newPosition, Color newColor)
	{
		color = newColor;
		if (spriteRenderer != null) spriteRenderer.color = color;

		setPosition(newPosition);

		gameObject.name = position.ToString();
	}

	void setPosition(Vector2 newPosition)
	{
		position = newPosition;
		transform.position = newPosition;
	}
}
