using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tile))]
public class TileRecolorer : Editor
{
	Tile tile = null;

	void OnEnable()
	{
		tile = (Tile)serializedObject.targetObject;
	}

	readonly Color redColor = Color.red;
	readonly Color blueColor = Color.blue;
	readonly Color greenColor = Color.green;
	readonly Color yellowColor = new Color(1, 1, 0, 1);

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		GUILayout.Box("Цвет");

		setColor(redColor);
		setColor(blueColor);
		setColor(greenColor);
		setColor(yellowColor);
	}

	void setColor(Color clr)
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.ColorField(clr);
		if (GUILayout.Button("Задать")) tile.setTileParametrs(tile.position, clr);
		GUILayout.EndHorizontal();
	}
}
