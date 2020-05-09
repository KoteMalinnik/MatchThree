using UnityEngine;

namespace GoalsManagment
{
	/// <summary>
	/// Элемент цели.
	/// </summary>
	public class Element
	{
		public Element() { }

		public Element(Color color, int count)
		{
			this.color = color;
			this.count = count;
		}

		/// <summary>
		/// Цвет.
		/// </summary>
		public Color color { get; set;}

		/// <summary>
		/// Количество.
		/// </summary>
		public int count { get; set; }

		/// <summary>
		/// Декремент количества.
		/// </summary>
		public void reduceElementCount()
		{
			count--;
			if (count < 0) count = 0;
		}
	}
}