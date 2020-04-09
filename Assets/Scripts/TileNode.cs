using UnityEngine;

/// <summary>
/// Узел сетки. Содержит позицию.
/// </summary>
public class TileNode
{
	readonly int idX;
	readonly int idY;

	public TileNode(int idX, int idY)
	{
		this.idX = idX;
		this.idY = idY;
	}

	/// <summary>
	/// Возвращает позицию узла в сетке
	/// </summary>
	/// <returns>The position.</returns>
	public Vector2 getPosition()
	{
		return new Vector2(idX, idY);
	}
}
