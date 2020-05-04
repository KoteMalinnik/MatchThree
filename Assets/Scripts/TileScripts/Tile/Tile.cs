using UnityEngine;

/// <summary>
/// Объект сетки.
/// </summary>
public class Tile : MonoBehaviour
{
	/// <summary>
	/// Идентификатор тайла в сетке тайлов.
	/// </summary>
	/// <value>The grid identifier.</value>
	public Vector2 gridID { get; private set; } = new Vector2 (0, 0);

	/// <summary>
	/// Позиция объекта в сетке тайлов.
	/// </summary>
	/// <value>The position.</value>
	public Vector2 position { get; private set; } = new Vector2 (0, 0);

	/// <summary>
	/// Цвет тайла.
	/// </summary>
	/// <value>The color.</value>
	public Color color { get; private set; }

	/// <summary>
	/// Кешированный SpriteRenderer.
	/// </summary>
	SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		gameObject.AddComponent<BoxCollider2D>();

		transform.localScale = Vector3.one * TilesMap.tileSize;
	}

	/// <summary>
	/// Устанавливает и рассчитывает параметры.
	/// </summary>
	/// <param name="newPosition">New position.</param>
	/// <param name="newColor">New color.</param>
	public void setTileParametrs(Vector2 newPosition, Color newColor)
	{
		setPosition(newPosition);
		setGridID(newPosition);
		setColor(newColor);

		gameObject.name = $"{(int)gridID.x}-{(int)gridID.y}";

		TilesMap.setTileInGrid(this);
	}

	/// <summary>
	/// Устанавливает позицю.
	/// </summary>
	/// <param name="newPosition">New position.</param>
	public void setPosition(Vector2 newPosition)
	{
		position = newPosition;
		transform.position = newPosition;
	}

	/// <summary>
	/// Устанавливает идентификатор.
	/// </summary>
	void setGridID(Vector2 newPosition)
	{
		gridID = new Vector2(newPosition.x, newPosition.y) / TilesMap.tileDeltaPosition;
	}

	/// <summary>
	/// Устанавливает цвет.
	/// </summary>
	/// <param name="newColor">New color.</param>
	void setColor(Color newColor)
	{
		color = newColor;
		if (spriteRenderer != null) spriteRenderer.color = color;
	}
}
