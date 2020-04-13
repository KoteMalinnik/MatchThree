using UnityEngine;

/// <summary>
/// Центрирование камеры относительно игрового поля
/// </summary>
public class CameraCentering : MonoBehaviour
{
    void Start()
	{
		float newPosX = (float)TileMap.mapWidht / 2 - 0.5f;
		float newPosY = (float)TileMap.mapHeight / 2 - 0.5f;
		transform.position = new Vector3(newPosX, newPosY, -1);
	}
}
