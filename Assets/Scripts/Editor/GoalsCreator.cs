using UnityEditor;
using UnityEngine;
using GoalsManagment;

public class GoalsCreator : EditorWindow
{
	[MenuItem("Window/GoalsCreator")]
	public static void showWindow()
	{
		Debug.Log("[GoalsCreator] Окно GoalsCreator открыто.");
		GoalsCreator window = GetWindow<GoalsCreator>(false, "GoalsCreator", true);
	}

	Vector2 scrollViewPosition = Vector2.zero;

	void OnEnable()
	{
		string datapath = Application.dataPath + "/Goals";

		var goalsFiles = new System.IO.DirectoryInfo(datapath).GetFiles("*.xml");

		for (int i = 0; i < goalsFiles.Length; i++)
		{
			int goalID = -1;
			int.TryParse(goalsFiles[i].Name.Substring(4, 1), out goalID);

			var loadedGoal = GoalSerialization.LoadGoal(goalID);
			GoalsManager.AddGoal(loadedGoal);
		}

		ID = getNewID();
	}

	void OnGUI()
	{
		DisplayGoalCreationData();

		scrollViewPosition = GUILayout.BeginScrollView(scrollViewPosition);

		GUILayout.Space(5);
		GUILayout.Label($"Уровней создано: {GoalsManager.goals.Count}.");
		GUILayout.Space(5);

		for (int i = 0; i < GoalsManager.goals.Count; i++) DisplayGoal(GoalsManager.goals[i]);

		GUILayout.EndScrollView();
	}

	int ID;
	int getNewID()
	{
		for (int i = 0; ; i++)
		{
			if (GoalsManager.CheckIDforExistence(i)) continue;
			return i;
		}
	}

	int Moves;
	int elementCount;
	Color elementColor = new Color(0,0,0,1);

	Goal tempGoal = new Goal();

	void DisplayGoalCreationData()
	{
		GUILayout.Box("Создание уровня.");

		ID = EditorGUILayout.IntField("ID", ID);
		Moves = EditorGUILayout.IntField("Ходы", Moves);

		if (tempGoal.Elements.Count == 0) GUILayout.Label("Элементы отсутствуют.");
		else
		{
			GUILayout.Label("Элементы");
			foreach (Element e in tempGoal.Elements) DisplayGoalElement(e, tempGoal, true);
		}

		GUILayout.BeginHorizontal();

		if (GUILayout.Button("+", GUILayout.Width(30))) CreateNewElement();

		elementColor = EditorGUILayout.ColorField(elementColor, GUILayout.Width(50));

		GUILayout.Label("Количество");
		elementCount = EditorGUILayout.IntField(elementCount);

		GUILayout.EndHorizontal();

		if (GUILayout.Button("Создать уровень")) CreateNewGoal();
	}

	void CreateNewElement()
	{
		if (elementCount <= 0) return;

		for (int i = 0; i < tempGoal.Elements.Count; i++)
			if (elementColor == tempGoal.Elements[i].color)
				return;

		tempGoal.Elements.Add(new Element(elementColor, elementCount));

		elementCount = 0;
		elementColor = new Color(0, 0, 0, 1);
	}

	void CreateNewGoal()
	{
		if (tempGoal.Elements.Count == 0) return;

		tempGoal.ID = ID;
		tempGoal.Moves = Moves;

		GoalsManager.AddGoal(tempGoal);

		ID = getNewID();
		Moves = 0;
		elementCount = 0;
		elementColor = new Color(0, 0, 0, 1);

		tempGoal = new Goal();
	}


	void DisplayGoal(Goal goal)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Box($"Уровень {goal.ID}");
		if (GUILayout.Button("X", GUILayout.Width(30))) GoalsManager.RemoveGoal(goal);
		GUILayout.EndHorizontal();

		GUILayout.Label($"Ходы: {goal.Moves}.");
		GUILayout.Label("Элементы:");
		foreach (Element e in goal.Elements) DisplayGoalElement(e, goal, false);

		GUILayout.Label("-------------");
	}

	void DisplayGoalElement(Element element, Goal parentGoal, bool displayButton)
	{
		GUILayout.BeginHorizontal();

		if (displayButton && GUILayout.Button("X", GUILayout.Width(30))) parentGoal.RemoveElement(element);

		EditorGUILayout.ColorField(element.color, GUILayout.Width(50));
		GUILayout.Label($"Количество: {element.count}");

		GUILayout.EndHorizontal();
	}
}