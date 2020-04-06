using UnityEngine;

public class TileObject : MonoBehaviour
{
	public Color color { get; private set; }
	SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		gameObject.AddComponent<BoxCollider2D>();
	}

	TileNode parentTileNode;
	public TileNode getParentNode()
	{
		return parentTileNode;
	}

	public void setTileObjectParametrs(TileNode newParentTileNode, Color newColor)
	{
		parentTileNode = newParentTileNode;

		color = newColor;
		if (spriteRenderer != null) spriteRenderer.color = color;

		transform.position = parentTileNode.getPosition();
		transform.localScale = Vector3.one * TileMap.tileSize;
	}
}
