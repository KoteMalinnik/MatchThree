using UnityEngine;

/// <summary>
/// Узел сетки. Содержит позицию.
/// </summary>
public class TileNode
{
	readonly int posX;
	readonly int posY;

	public TileNode(int posX, int posY)
	{
		this.posX = posX;
		this.posY = posY;
	}

	/// <summary>
	/// Возвращает позицию узла в сетке
	/// </summary>
	/// <returns>The position.</returns>
	public Vector2 getPosition()
	{
		return new Vector2(posX, posY);
	}
}
