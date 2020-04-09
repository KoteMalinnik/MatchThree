using UnityEngine;

public class TileMap : MonoBehaviour
{
	[SerializeField]
	/// <summary>
	/// Ширина сетки тайлов
	/// </summary>
	int mapWidht = 10;
	public int getWidth() { return mapWidht; }

	[SerializeField]
	/// <summary>
	/// Высота сетки тайлов
	/// </summary>
	int mapHeight = 10;
	public int getHeight() { return mapHeight; }

	[SerializeField]
	/// <summary>
	/// Размер тайла
	/// </summary>
	float _tileSize = 6f;
	public static float tileSize { get; private set;} = 6f;

	/// <summary>
	/// Узлы сетки
	/// </summary>
	TileNode[,] nodes;

	void OnValidate()
	{
		if (mapWidht < 3) mapWidht = 3;
		if (mapHeight < 3) mapHeight = 3;

		if (_tileSize < 1f) _tileSize = 1f;
	}

	void Awake()
	{
		tileSize = _tileSize;

		InstanciateTileNodes();
	}

	/// <summary>
	/// Создание постоянных узлов сетки
	/// </summary>
	void InstanciateTileNodes()
	{
		nodes = new TileNode[mapWidht, mapHeight];

		for (int posX = 0; posX < mapWidht; posX++)
		{
			for (int posY = 0; posY < mapHeight; posY++)
			{
				var temp = new TileNode(posX, posY);
				nodes[posX, posY] = temp;
			}
		}
	}

	/// <summary>
	/// Возвращает TileNode по позиции в сетке карты. Левый нижний угол карты - (0, 0), правый верхний - (mapWidth, mapHeight)
	/// </summary>
	/// <returns>The tile node.</returns>
	public TileNode getTileNodeByPosition(int index_X, int index_Y)
	{
		try
		{
			return nodes[index_X, index_Y];
		}
		catch
		{
			return null;
		}
	}
}
