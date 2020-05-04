using UnityEngine;

[RequireComponent(typeof(Tile))]
/// <summary>
/// Реакция тайла на нажатие мышью.
/// </summary>
public class TileInteractivity : MonoBehaviour
{
	/// <summary>
	/// Прикрепленный компонент Tile.
	/// </summary>
	Tile tileObject;

	void Awake()
	{
		tileObject = GetComponent<Tile>();
	}

	void OnMouseDown()
	{
		PlayerMove.setTilesForMove(tileObject);
	}
}
