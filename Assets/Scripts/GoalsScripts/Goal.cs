using System;
using System.Collections.Generic;

public class Goal
{
	/// <summary>
	/// Количество ходов за уровень.
	/// </summary>
	public int Moves { get; } = -1;

	/// <summary>
	/// Идентификатор уровня.
	/// </summary>
	public int ID { get; } = -1;

	/// <summary>
	/// Элементы цели.
	/// </summary>
	List<Element> Elements = new List<Element>();

	/// <summary>
	/// Добавляет элемент цели.
	/// </summary>
	/// <returns>The element.</returns>
	/// <param name="elementColor">Element color.</param>
	/// <param name="count">Default count.</param>
	public Element AddElement(UnityEngine.Color elementColor, int count)
	{
		var newElement = new Element(elementColor, count);

		if (!Elements.Contains(newElement)) Elements.Add(newElement);
		return newElement;
	}

	/// <summary>
	/// Возвращает элемент цели.
	/// </summary>
	/// <param name="elementColor">Element color.</param>
	public Element? GetElement(UnityEngine.Color elementColor)
	{
		foreach (Element e in Elements)
			if (e.color == elementColor)
				return e;

		return null;
	}

	public Goal(int Moves, int ID)
	{
		if (Moves <= 0) throw new ArgumentException("Неверное количество ходов.");
		if (GoalsManager.CheckIDforExistence(ID)) throw new ArgumentException($"Цель с таким ID({ID}) уже существует.");

		this.Moves = Moves;
		this.ID = ID;
	}
}