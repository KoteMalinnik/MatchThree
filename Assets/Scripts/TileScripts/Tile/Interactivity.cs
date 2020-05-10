using UnityEngine;

namespace Tiles
{
	[RequireComponent(typeof(Tile))]
	/// <summary>
	/// Реакция тайла на нажатие мышью.
	/// </summary>
	public class Interactivity : MonoBehaviour
	{
		/// <summary>
		/// Прикрепленный компонент Tile.
		/// </summary>
		Tile tileObject;

		void Awake()
		{
			tileObject = GetComponent<Tile>();
		}

		void OnMouseDown()
		{
			if (Replacer.replacePremission) PlayerMove.setTilesForMove(tileObject);
		}
	}
}