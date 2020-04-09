using UnityEngine;

/// <summary>
/// Объект сетки
/// </summary>
public class TileObject : MonoBehaviour
{
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
	}

	/// <summary>
	/// Узел сетки, к которому привязан объект
	/// </summary>
	TileNode parentTileNode;
	/// <summary>
	/// Возвращает узел сетки, к которому привязан объект
	/// </summary>
	/// <returns>The parent node.</returns>
	public TileNode getParentNode()
	{
		return parentTileNode;
	}

	/// <summary>
	/// Установка значений объекта
	/// </summary>
	/// <param name="newParentTileNode">New parent tile node.</param>
	/// <param name="newColor">New color.</param>
	public void setTileObjectParametrs(TileNode newParentTileNode, Color newColor)
	{
		parentTileNode = newParentTileNode;

		color = newColor;
		if (spriteRenderer != null) spriteRenderer.color = color;

		transform.position = parentTileNode.getPosition();
		transform.localScale = Vector3.one * TileMap.tileSize;
	}
}
