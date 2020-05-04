using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Настройка тайла после создания при совпадении.
/// </summary>
public class DropperPreparing
{
	public void prepare(List<Vector2> positions)
	{
		var dropper = new TilesDropper();
		dropper.dropUpperTiles(tilesToDestroy[i]);
		yield return new WaitWhile(() => dropper.routine != null);
	}
}
