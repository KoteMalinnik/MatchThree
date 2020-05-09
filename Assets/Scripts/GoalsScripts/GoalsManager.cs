using System.Collections.Generic;
using UnityEngine;

namespace GoalsManagment
{
	public class GoalsManager : MonoBehaviour
	{
		[ExecuteInEditMode]
		public static List<Goal> goals { get; private set; } = new List<Goal>();

		/// <summary>
		/// Проверит ID на существование в списке. Вернет <c>true</c>, если цель с таким ID уже существует.
		/// </summary>
		/// <param name="ID">Goal Identifier.</param>
		[ExecuteInEditMode]
		public static bool CheckIDforExistence(int ID)
		{
			foreach (Goal goal in goals)
				if (goal.ID == ID)
					return true;

			return false;
		}

		[ExecuteInEditMode]
		public static void AddGoal(Goal goal)
		{
			if (goals.Contains(goal)) return;

			goals.Add(goal);
			GoalSerialization.SaveGoal(goal);
		}

		[ExecuteInEditMode]
		public static void RemoveGoal(Goal goal)
		{
			if (goals.Contains(goal))
			{
				goals.Remove(goal);
				GoalSerialization.RemoveGoal(goal);
			}
		}


		public static int PlayerLevel { get; private set; } = 0;
		public static void PlayerLevelUp() { PlayerLevel++; }
		public static Goal GoalAtPlayerLevel { get; private set; } = new Goal();

		void Awake()
		{
			if (goals.Count < PlayerLevel) return;

			GoalAtPlayerLevel.ID = goals[PlayerLevel].ID;
			GoalAtPlayerLevel.Moves = goals[PlayerLevel].Moves;

			foreach (Element e in goals[PlayerLevel].Elements) GoalAtPlayerLevel.AddElement(e.color, e.count);
		}

		public static void ProcessGoal(Tile tile)
		{
			var element = GoalAtPlayerLevel.GetElement(tile.color);
			if (element == null) return;
			if (element.count == 0) return;

			element.reduceElementCount();

			if (element.count == 0)
			{
				Debug.Log("[GoalsManager] Одна из целей достигнута.");
				GoalAtPlayerLevel.RemoveElement(element);
			}

			if (GoalAtPlayerLevel.Elements.Count == 0)
			{
				Debug.Log("[GoalsManager] Уровень пройден.");
			}
		}

		public static void ProcessMoves()
		{
			GoalAtPlayerLevel.Moves--;

			if (GoalAtPlayerLevel.Moves == 0)
			{
				Debug.Log("[GoalsManager] <color=red>Ходы закончились.</color>");
			}
		}
	}
}