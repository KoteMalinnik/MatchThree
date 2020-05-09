/// <summary>
/// Элемент цели.
/// </summary>
public struct Element
{
	public Element(UnityEngine.Color color, int count)
	{
		this.color = color;
		this.count = count;
	}

	/// <summary>
	/// Цвет.
	/// </summary>
	public UnityEngine.Color color { get; }

	/// <summary>
	/// Количество.
	/// </summary>
	public int count { get; private set; }

	/// <summary>
	/// Декремент количества.
	/// </summary>
	public void reduceElementCount(int delta = 1)
	{
		count -= delta;
		if (count < 0) count = 0;
	}
}