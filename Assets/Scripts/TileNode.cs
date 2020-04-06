using UnityEngine;

public class TileNode
{
	public readonly int ID;
	readonly int posX;
	readonly int posY;
	readonly float size;

	public TileNode(int posX, int posY, float size, int ID)
	{
		this.posX = posX;
		this.posY = posY;
		this.size = size;
		this.ID = ID;
	}

	public Vector2 getPosition()
	{
		return new Vector2(posX, posY);
	}
}
