using System.Collections.Generic;

namespace PlaziusTest
{
	/// <summary>
	///     Класс карточки.
	/// </summary>
	public class TravelCard<T>
	{
		/// <summary>
		///     Конструктор.
		/// </summary>
		/// <param name="from">Пункт отправления.</param>
		/// <param name="to">Пункт назначения.</param>
		public TravelCard(T from, T to)
		{
			From = from;
			To = to;
		}

		/// <summary>
		///     Пункт отправления.
		/// </summary>
		public T From { get; }

		/// <summary>
		///     Пункт назначения.
		/// </summary>
		public T To { get; }

		/// <summary>
		///     Является ли текущая карточка источником для другой.
		/// </summary>
		/// <param name="card">Карточка для которой нужно проверить правило.</param>
		/// <returns>Ture если является. False в противном случае.</returns>
		public bool IsTargetForCard(TravelCard<T> card)
		{
			return EqualityComparer<T>.Default.Equals(To, card.From);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((TravelCard<T>) obj);
		}

		/// <summary>
		///     Проверяет эквивалентность двух типизированных объектов.
		/// </summary>
		/// <param name="other">Объект с которым нужно сравнить.</param>
		/// <returns>Результат сравнения.</returns>
		protected bool Equals(TravelCard<T> other)
		{
			return EqualityComparer<T>.Default.Equals(From, other.From) && EqualityComparer<T>.Default.Equals(To, other.To);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				return (EqualityComparer<T>.Default.GetHashCode(From) * 397) ^ EqualityComparer<T>.Default.GetHashCode(To);
			}
		}
	}
}