using System.Collections.Generic;

public static class GoalsManager
{
	static List<Goal> goals = new List<Goal>();

	/// <summary>
	/// Проверит ID на существование в списке. Вернет <c>true</c>, если цель с таким ID уже существует.
	/// </summary>
	/// <param name="ID">Goal Identifier.</param>
	public static bool CheckIDforExistence(int ID)
	{
		foreach(Goal goal in goals) 
			if (goal.ID == ID)
				return true;
		
		return false;
	}
}