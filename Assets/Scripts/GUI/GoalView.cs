using UnityEngine;
using GoalsManagment;
using UnityEngine.UI;
using System.Collections.Generic;

namespace GUI
{
	public class GoalView : MonoBehaviour
	{
		[SerializeField]
		ElementItem elementItem = null;

		[SerializeField]
		RectTransform Content = null;

		[SerializeField]
		Text PlayerLevel = null;

		[SerializeField]
		Text _Moves = null;
		static Text Moves = null;

		static List<ElementItem> items = new List<ElementItem>();

		void Awake()
		{
			if (PlayerLevel == null) Debug.LogError("[GoalView] Отсутствует PlayerLevel.");
			if (elementItem == null) Debug.LogError("[GoalView] Отсутствует ElementItem.");
			if (Content == null) Debug.LogError("[GoalView] Отсутствует Content.");
			if (_Moves == null) Debug.LogError("[GoalView] Отсутствует Moves.");

			Moves = _Moves;
		}

		void Start()
		{
			PlayerLevel.text = "Level " + GoalsManager.PlayerLevel;
			updateMoves(GoalsManager.GoalAtPlayerLevel.Moves);

			var elements = GoalsManager.GoalAtPlayerLevel.Elements;

			for (int i = 0; i < elements.Count; i++)
			{
				var item = Instantiate(elementItem, Content);

				item.setCount(elements[i].count);
				item.setIconColor(elements[i].color);

				items.Add(item);
			}
		}

		public static void updateMoves(int newMoves)
		{
			Moves.text = "Moves: " + newMoves;
		}

		public static void updateItem(Color color, int newCount)
		{
			var item = getItemByColor(color);
			if (item == null) return;

			item.setCount(newCount);
		}

		public static void removeItem(Color color)
		{
			var item = getItemByColor(color);
			if (item == null) return;

			items.Remove(item);
			Debug.Log("[GoalView] Анимация достижения одной из целей");
		}

		static ElementItem getItemByColor(Color color)
		{
			foreach (ElementItem item in items)
			{
				if (item.icon.color == color) return item;
			}

			return null;
		}
	}
}
