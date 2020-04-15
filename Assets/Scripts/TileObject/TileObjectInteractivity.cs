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
		var position = tileObject.position;
		TileObjectMovement.setTiles(tileObject);
	}
}
