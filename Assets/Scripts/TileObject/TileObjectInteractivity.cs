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
		var position = tileObject.getParentNode().getPosition();
		Debug.Log($"Нажатие на объект {position}. Цвет: {tileObject.color.ToString()}");

		TileObjectMovement.setObject(tileObject);
	}
}
