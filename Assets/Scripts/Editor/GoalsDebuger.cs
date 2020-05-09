using GoalsManagment;
using UnityEditor;
using UnityEngine;

public class GoalsDebuger : EditorWindow
{
	[MenuItem("Window/GoalsDebuger")]
	public static void showWindow()
	{
		Debug.Log("[GoalsDebuger] Окно GoalsDebuger открыто.");
		GoalsDebuger window = GetWindow<GoalsDebuger>(false, "GoalsDebuger", true);
	}

	void OnGUI()
	{
		GUILayout.Label($"Уровень: {GoalsManager.PlayerLevel}.");
		GUILayout.Space(5);

		DisplayGoalAtPlayerLevel(GoalsManager.GoalAtPlayerLevel);
	}

	void DisplayGoalAtPlayerLevel(Goal goal)
	{
		GUILayout.Box($"ID: {goal.ID}");
		GUILayout.Label($"Ходы: {goal.Moves}.");

		GUILayout.Label("Элементы:");
		foreach (Element e in goal.Elements) DisplayGoalElement(e);
	}

	void DisplayGoalElement(Element element)
	{
		GUILayout.BeginHorizontal();

		EditorGUILayout.ColorField(element.color, GUILayout.Width(50));
		GUILayout.Label($"Количество: {element.count}");

		GUILayout.EndHorizontal();
	}
}