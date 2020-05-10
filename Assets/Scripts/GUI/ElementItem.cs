using UnityEngine.UI;
using UnityEngine;

namespace GUI
{
	public class ElementItem : MonoBehaviour
	{
		public Image icon { get; private set; } = null;
		Text count = null;

		void Awake()
		{
			icon = transform.GetComponentInChildren<Image>();
			if (icon == null) Debug.LogError("[ElementItem] Иконка не найдена.");

			count = transform.GetComponentInChildren<Text>();
			if (count == null) Debug.LogError("[ElementItem] Текст не найден.");
		}

		public void setIconColor(Color newColor)
		{
			icon.color = newColor;
		}

		public void setCount(int newCount)
		{
			count.text = newCount.ToString();
		}
	}
}
