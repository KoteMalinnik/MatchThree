using UnityEngine;

public class TileObject : MonoBehaviour
{
	public Color color { get; private set; }
	TileNode parentTileNode;
	SpriteRenderer spriteRenderer;


	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		gameObject.AddComponent<BoxCollider2D>();
	}

	public void setParentTileNode(TileNode newParentNode)
	{
		parentTileNode = newParentNode;
	}

	public TileNode getParentNode()
	{
		return parentTileNode;
	}

	public void setColor(Color color)
	{
		this.color = color;
		if (spriteRenderer != null) spriteRenderer.color = color;
	}

	public void setSizeAndPosition(float newSize, float posX, float posY)
	{
		transform.position = new Vector2(posX, posY);
		transform.localScale = transform.localScale * newSize;
	}
}
