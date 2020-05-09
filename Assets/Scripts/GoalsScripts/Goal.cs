using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoalsManagment
{
	public class Goal
	{
		/// <summary>
		/// Количество ходов за уровень.
		/// </summary>
		int moves = -1;
		public int Moves
		{
			get
			{
				return moves;
			}
			set
			{
				if (value <= 0) throw new ArgumentException("Неверное количество ходов.");
				moves = value;
			}
		}

		/// <summary>
		/// Идентификатор уровня.
		/// </summary>
		int id = -1;
		public int ID
		{
			get
			{
				return id;
			}
			set
			{
				if (GoalsManager.CheckIDforExistence(value))
					throw new ArgumentException($"Цель с таким ID({value}) уже существует.");

				id = value;
			}
		}

		/// <summary>
		/// Элементы цели.
		/// </summary>
		[ExecuteInEditMode]
		public List<Element> Elements { get; private set; } = new List<Element>();

		/// <summary>
		/// Добавляет элемент цели.
		/// </summary>
		/// <returns>The element.</returns>
		/// <param name="elementColor">Element color.</param>
		/// <param name="count">Default count.</param>
		[ExecuteInEditMode]
		public Element AddElement(UnityEngine.Color elementColor, int count)
		{
			var newElement = new Element(elementColor, count);

			if (!Elements.Contains(newElement)) Elements.Add(newElement);
			return newElement;
		}

		[ExecuteInEditMode]
		public void RemoveElement(Element element)
		{
			if (Elements.Contains(element)) Elements.Remove(element);
		}

		/// <summary>
		/// Возвращает элемент цели.
		/// </summary>
		/// <param name="elementColor">Element color.</param>
		[ExecuteInEditMode]
		public Element? GetElement(UnityEngine.Color elementColor)
		{
			foreach (Element e in Elements)
				if (e.color == elementColor)
					return e;

			return null;
		}

		public Goal(int Moves, int ID)
		{
			this.Moves = Moves;
			this.ID = ID;
		}

		public Goal() { }
	}
}