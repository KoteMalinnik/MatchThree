using UnityEngine;

[RequireComponent(typeof(TileObject))]
/// <summary>
/// Реакция тайла на нажатие мышью.
/// </summary>
public class TileObjectInteractivity : MonoBehaviour
{
	TileObject tileObject;

	void Awake()
	{
		tileObject = GetComponent<TileObject>();
	}

	void OnMouseDown()
	{
		TileObjectMovement.setTiles(tileObject);
	}
}
