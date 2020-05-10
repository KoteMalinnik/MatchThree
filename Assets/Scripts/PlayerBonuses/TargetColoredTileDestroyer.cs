using Tiles;
using UnityEngine;

namespace Bonuses
{
	public class TargetColoredTileDestroyer : IBonusAction
	{
		public void ExecuteBonus()
		{
			if (targetTile == null) return;

			Debug.Log("[TargetColoredTileDestroyer] Действие бонуса.");

			var tilesOfTargetColor = Finder.getTilesOfColor(targetTile.color);
			Matches.Destroyer.destroyMatches(tilesOfTargetColor);
		}

		Tile targetTile = null;

		public void setTargetTile(Tile tile)
		{
			Debug.Log("[TargetColoredTileDestroyer] Установка целевого тайла: " + tile.name);

			ExecuteBonus();
		}
	}
}
