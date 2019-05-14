using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaziusTest
{
	/// <summary>
	///     Вспомагательные методы для работы с карточками.
	/// </summary>
	public static class TravelExtensions
	{
		/// <summary>
		///     Метод формирует маршрут следования на основе набора карточек.
		/// </summary>        
		/// <typeparam name="T">Тип, с помощью которого в карточке путешествия задается пункт назначения.</typeparam>
		/// <param name="cards">Произвольный набор карточек путешествия. В наборе не должно содержаться циклов.</param>
		public static IEnumerable<TravelCard<T>> GetTripPath<T>(this IEnumerable<TravelCard<T>> cards)
		{
			if (cards == null)
			{
				throw new ArgumentNullException(nameof(cards));
			}

			if (cards.Count() < 2)
			{
				return cards;
			}

			const int UNDEFINED = -1;

			var orderedByTo = cards.OrderBy(x => x.To).ToArray();
			var orderedByFrom = cards.OrderBy(x => x.From).ToArray();

			var tailIndex = UNDEFINED;
			for (int i = 0; i < orderedByTo.Length; i++)
			{
				if (!orderedByTo[i].IsTargetForCard(orderedByFrom[i]) &&
				    (i + 1 < orderedByTo.Length && !orderedByTo[i].IsTargetForCard(orderedByFrom[i + 1])))
				{
					tailIndex = i;
					break;
				}
			}

			var incompleteSequenceIndex = orderedByTo.Length - 1;
			if (tailIndex != UNDEFINED)
			{
				orderedByTo.Swap(incompleteSequenceIndex, tailIndex);
			}
			for (int i = incompleteSequenceIndex; i >= 0; i--)
			{
				var curElement = orderedByTo[i];
				for (int j = i - 1; j >= 0; j--)
				{
					if (Equals(curElement.From, orderedByTo[j].To))
					{
						orderedByTo.Swap(i - 1, j);
						break;
					}
				}
			}

			return orderedByTo;
		}

		/// <summary>
		///     Метод формирует маршрут следования на основе набора карточек.
		///		Реализован с помощью хештаблиц.
		/// </summary>        
		/// <typeparam name="T">Тип, с помощью которого в карточке путешествия задается пункт назначения.</typeparam>
		/// <param name="cards">Произвольный набор карточек путешествия. В наборе не должно содержаться циклов.</param>
		public static IEnumerable<TravelCard<T>> GetTripPathFast<T>(this IList<TravelCard<T>> cards)
		{
			if (cards == null)
			{
				throw new ArgumentNullException(nameof(cards));
			}

			if (cards.Count < 2)
			{
				return cards;
			}
			Dictionary<T, int> fromDictionary = new Dictionary<T, int>();
			Dictionary<T, int> toDictionary = new Dictionary<T, int>();

			for (int i = 0; i < cards.Count; i++)
			{
				var curCard = cards[i];
				fromDictionary[curCard.From] = i;
				toDictionary[curCard.To] = i;
			}

			var tailIndex = 0;
			for (int i = 0; i < cards.Count; i++)
			{
				tailIndex = i;
				var curCard = cards[i];
				if (!fromDictionary.ContainsKey(curCard.To))
				{
					break;
				}
			}

			var result = new TravelCard<T>[cards.Count];
			
			var currentIndex = tailIndex;
			for (int i = result.Length - 1; i >= 0; i--)
			{
				var currentCard = cards[currentIndex];
				result[i] = currentCard;
				if (i != 0)
				{
					currentIndex = toDictionary[currentCard.From];
				}
			}

			return result;
		}

		/// <summary>
		/// Меняет местами элементы массива.
		/// </summary>
		/// <typeparam name="T">Тип, с помощью которого в карточке путешествия задается пункт назначения.</typeparam>
		/// <param name="initialArray">Массив с карточками.</param>
		/// <param name="firstIndex">Индекс первого элемента.</param>
		/// <param name="secondIndex">Индекс второго элемента.</param>
		public static void Swap<T>(this TravelCard<T>[] initialArray, int firstIndex, int secondIndex)
		{
			if (firstIndex == secondIndex)
			{
				return;
			}
			TravelCard<T> t = initialArray[firstIndex];
			initialArray[firstIndex] = initialArray[secondIndex];
			initialArray[secondIndex] = t;
		}
	}
}