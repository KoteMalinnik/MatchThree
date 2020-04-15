using UnityEngine;

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
