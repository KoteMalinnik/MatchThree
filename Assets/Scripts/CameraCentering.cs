using UnityEngine;

/// <summary>
/// Центрирование камеры относительно игрового поля
/// </summary>
public class CameraCentering : MonoBehaviour
{
	[SerializeField]
	float deltaPositionYForGUI = 0f;

	float width;
	float height;

    void Start()
	{
		width = TileMap.mapWidht;
		height = TileMap.mapHeight;

		setCenteredPosition();
		setOrthograficSize();
	}

	void setCenteredPosition()
	{
		float newPosX = width / 2 - 0.5f;
		float newPosY = height / 2 - 0.5f + deltaPositionYForGUI;
		transform.position = new Vector3(newPosX, newPosY, -1);
	}

	void setOrthograficSize()
	{
		Camera.main.orthographicSize = width >= height ? width / 2 : height / 2;
	}
}
