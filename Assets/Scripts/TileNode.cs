using UnityEngine;

public class TileNode
{
	readonly int ID;
	readonly int posX;
	readonly int posY;
	readonly float size;

	public TileNode(int posX, int posY, float size, int ID)
	{
		this.posX = posX;
		this.posY = posY;
		this.size = size;
		this.ID = ID;
		Debug.Log($"ID: {this.ID}. Position: ({this.posX}, {this.posY}). Size: {this.size}");
	}

	public void setTileObject(TileObject tileObject)
	{
		tileObject.setSizeAndPosition(size, posX, posY);
		tileObject.setParentTileNode(this);
	}

	public Vector2 getPosition()
	{
		return new Vector2(posX, posY);
	}
}
