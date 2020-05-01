using UnityEngine;

/// <summary>
/// Объект сетки
/// </summary>
public class Tile : MonoBehaviour
{
	/// <summary>
	/// Идентификатор тайла в сетке.
	/// </summary>
	/// <value>The grid identifier.</value>
	public Vector2 gridID { get; private set; } = new Vector2 (0, 0);

	/// <summary>
	/// Позиция объекта в сетке tileMap
	/// </summary>
	/// <value>The position.</value>
	public Vector2 position { get; private set; } = new Vector2 (0, 0);

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
	public void setTileParametrs(Vector2 newPosition, Color newColor)
	{
		setPosition(newPosition);
		setGridID(newPosition);
		setColor(newColor);

		gameObject.name = $"{(int)gridID.x}-{(int)gridID.y}";

		TileMap.setTileInGrid(this);
	}

	/// <summary>
	/// Установить позицию.
	/// </summary>
	/// <param name="newPosition">New position.</param>
	public void setPosition(Vector2 newPosition)
	{
		position = newPosition;
		transform.position = newPosition;
	}

	/// <summary>
	/// Установить идентификатор в сетке.
	/// </summary>
	public void setGridID(Vector2 newPosition)
	{
		gridID = new Vector2(newPosition.x, newPosition.y) / TileMap.tileDeltaPosition;
	}

	/// <summary>
	/// Установить цвет.
	/// </summary>
	/// <param name="newColor">New color.</param>
	public void setColor(Color newColor)
	{
		color = newColor;
		if (spriteRenderer != null) spriteRenderer.color = color;
	}
}
