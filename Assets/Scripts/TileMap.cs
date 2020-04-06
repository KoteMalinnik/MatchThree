using UnityEngine;

public class TileMap : MonoBehaviour
{
	[SerializeField]
	int mapWidht = 10;
	public int getWidth() { return mapWidht; }

	[SerializeField]
	int mapHeight = 10;
	public int getHeight() { return mapHeight; }

	[SerializeField]
	float _tileSize = 6f;
	public static float tileSize { get; private set;} = 6f;

	TileNode[] nodes;

	void OnValidate()
	{
		if (mapWidht < 5) mapWidht = 5;
		if (mapHeight < 5) mapHeight = 5;

		if (_tileSize < 1f) _tileSize = 1f;
	}

	void Awake()
	{
		tileSize = _tileSize;

		InstanciateTileNodes();
	}

	void InstanciateTileNodes()
	{
		var objectsCount = mapWidht * mapHeight;
		nodes = new TileNode[objectsCount];

		for (int posX = 0, count = 0; posX < mapWidht; posX++)
		{
			for (int posY = 0; posY < mapHeight; posY++, count++)
			{
				var temp = new TileNode(posX, posY, tileSize, count);
				nodes[count] = temp;
			}
		}
	}


	/// <summary>
	/// Тайлы нумеруются снизу вверх слева направо
	/// </summary>
	/// <returns>The tile node.</returns>
	/// <param name="index">Index.</param>
	public TileNode getTileNodeByIndex(int index)
	{
		if(index<=nodes.Length-1) return nodes[index];

		return null;
	}
}
