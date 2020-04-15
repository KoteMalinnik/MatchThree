using UnityEngine;

/// <summary>
/// Объект сетки
/// </summary>
public class TileObject : MonoBehaviour
{
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
		setGridID();
		setColor(newColor);

		gameObject.name = $"{(int)gridID.x}-{(int)gridID.y}";

		TileMap.setTileInGrid(this);
	}

	public void setPosition(Vector2 newPosition)
	{
		position = newPosition;
		transform.position = newPosition;
	}

	public void setGridID()
	{
		gridID = new Vector2(position.x, position.y) / TileMap.tileDeltaPosition;
	}

	public void setColor(Color newColor)
	{
		color = newColor;
		if (spriteRenderer != null) spriteRenderer.color = color;
	}
}
