using UnityEngine;

[RequireComponent(typeof(Tile))]
/// <summary>
/// Реакция тайла на нажатие мышью.
/// </summary>
public class TileInteractivity : MonoBehaviour
{
	Tile tileObject;

	void Awake()
	{
		tileObject = GetComponent<Tile>();
	}

	void OnMouseDown()
	{
		TilesMovement.setTiles(tileObject);
	}
}
