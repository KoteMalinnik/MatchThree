using UnityEngine;

public class CoroutinePlayer : MonoBehaviour
{
	static CoroutinePlayer _instance = null;

	public static CoroutinePlayer Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = Camera.main.gameObject.AddComponent<CoroutinePlayer>();
			}

			return _instance;
		}
	}
}