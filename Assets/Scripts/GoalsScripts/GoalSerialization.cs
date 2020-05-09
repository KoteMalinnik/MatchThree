using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace GoalsManagment
{
	public static class GoalSerialization
	{
		static public void SaveGoal(Goal goal)
		{
			var serializer = new XmlSerializer(typeof(Goal), nameof(GoalsManagment));

			string datapath = Application.dataPath + "/Goals/Goal" + goal.ID + ".xml";
			var fs = new FileStream(datapath, FileMode.Create);

			serializer.Serialize(fs, goal);
			fs.Close();
		}

		static public Goal LoadGoal(int goalID)
		{
			var serializer = new XmlSerializer(typeof(Goal), nameof(GoalsManagment));

			string datapath = Application.dataPath + "/Goals/Goal" + goalID + ".xml";
			var fs = new FileStream(datapath, FileMode.Open);

			var goal = (Goal)serializer.Deserialize(fs);
			fs.Close();

			return goal;
		}

		static public void RemoveGoal(Goal goal)
		{
			string datapath = Application.dataPath + "/Goals/Goal" + goal.ID + ".xml";
			File.Delete(datapath);
			datapath += ".meta";
			File.Delete(datapath);
		}
	}
}
