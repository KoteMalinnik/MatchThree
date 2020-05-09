using System.Collections.Generic;
using UnityEngine;

namespace GoalsManagment
{
	public static class GoalsManager
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
	}
}