using UnityEngine;

[RequireComponent(typeof(TileObject))]
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
