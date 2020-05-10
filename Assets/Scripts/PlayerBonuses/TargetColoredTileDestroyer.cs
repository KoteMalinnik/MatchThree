using TilesCore;
using UnityEngine;

namespace Bonuses
{
	public class TargetColoredTileDestroyer : BonusAction
	{
		public void ExecuteBonus()
		{
			Debug.Log("[TargetColoredTileDestroyer] Действие бонуса.");
		}

		Tile targetTile = null;

		public void setTargetTile(Tile tile)
		{
			Debug.Log("[TargetColoredTileDestroyer] Установка целевого тайла: " + tile.name);

			ExecuteBonus();
		}
	}
}
