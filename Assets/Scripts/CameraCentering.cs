using UnityEngine;

public class CameraCentering : MonoBehaviour
{
    void Start()
    {
		float tileDeltaPosition = TileMap.tileDeltaPosition;
		float width = TileMap.mapWidht * tileDeltaPosition;
		float height = TileMap.mapHeight * tileDeltaPosition;

		Camera mainCamera = Camera.main;

		setCenter(width, height, tileDeltaPosition, mainCamera);
		setOrthographicSize(width, height, tileDeltaPosition, mainCamera);
    }

	/// <summary>
	/// Установка позиции камеры по центру игрового поля
	/// </summary>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
	/// <param name="tileDeltaPosition">Tile delta position.</param>
	/// <param name="mainCamera">Main camera.</param>
	void setCenter(float width, float height, float tileDeltaPosition, Camera mainCamera)
	{
		float newCameraPosX = width / 2 - tileDeltaPosition / 2;
		float newCameraPosY = height / 2 - tileDeltaPosition / 2;

		mainCamera.transform.position = new Vector3(newCameraPosX, newCameraPosY, -1);
	}

	/// <summary>
	/// Установка ортографического размера камеры в пределах игрового поля
	/// </summary>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
	/// <param name="tileDeltaPosition">Tile delta position.</param>
	/// <param name="mainCamera">Main camera.</param>
	void setOrthographicSize(float width, float height, float tileDeltaPosition, Camera mainCamera)
	{
		float newCameraOrthographicSize = width >= height ? width : height;
		newCameraOrthographicSize -= tileDeltaPosition;

		mainCamera.orthographicSize = newCameraOrthographicSize;
	}
}
