using UnityEngine;

/// <summary>
/// Синглтон для запуска корутин из статических классов.
/// </summary>
public class CoroutinePlayer : MonoBehaviour
{
	/// <summary>
	/// Экзмемпляр класса.
	/// </summary>
	static CoroutinePlayer _instance = null;

	public static CoroutinePlayer Instance
	{ get { return _instance ?? new GameObject("CoroutinePlayer").AddComponent<CoroutinePlayer>(); } }

	void Awake()
	{
		_instance = this;
	}

	void OnDestroy()
	{
		_instance = null;
	}
}