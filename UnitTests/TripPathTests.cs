﻿using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using PlaziusTest;

namespace UnitTests
{
	/// <summary>
	///     Тесты на метод получения маршрута путешествия.
	/// </summary>
	[TestFixture]
	public class TripPathTests
	{
		/// <summary>
		///     Тест на обработку пустой последовательности.
		/// </summary>
		[Test]
		public void GetTripPath_EmptyList_Success()
		{
			//arrange
			var cards = new TravelCard<string>[]
			{
			};

			var expectedResult = new TravelCard<string>[]
			{
			};

			//act
			var result = cards.GetTripPath();

			//assert
			CollectionAssert.AreEqual(expectedResult, result);
		}


		/// <summary>
		///     Тест на обработку последовательности с одним элементом.
		/// </summary>
		[Test]
		public void GetTripPath_ListWithOneElement_Success()
		{
			//arrange
			var cards = new[]
			{
				new TravelCard<string>("Барселона", "Магадан")
			};

			var expectedResult = new[]
			{
				new TravelCard<string>("Барселона", "Магадан")
			};

			//act
			var result = cards.GetTripPath();

			//assert
			CollectionAssert.AreEqual(expectedResult, result);
		}


		/// <summary>
		///     Тест на обработку последовательности с двумя элементами.
		/// </summary>
		[Test]
		public void GetTripPath_ListWithTwoElements_Success()
		{
			//arrange
			var cards = new[]
			{
				new TravelCard<string>("Барселона", "Магадан"),
				new TravelCard<string>("Липецк", "Барселона")
			};

			var expectedResult = new[]
			{
				new TravelCard<string>("Липецк", "Барселона"),
				new TravelCard<string>("Барселона", "Магадан")
			};

			//act
			var result = cards.GetTripPath();

			//assert
			CollectionAssert.AreEqual(expectedResult, result);
		}


		/// <summary>
		///     Проверка, что правильно обрабатывается null параметр.
		/// </summary>
		[Test]
		public void GetTripPath_NullArgumentException()
		{
			//arrange
			TravelCard<string>[] cards = null;

			//act		    
			ActualValueDelegate<object> testDelegate = () => cards.GetTripPath();

			//assert			
			Assert.That(testDelegate, Throws.TypeOf<ArgumentNullException>());
		}

		/// <summary>
		///     Успешный тест.
		/// </summary>
		[Test]
		public void GetTripPath_Success()
		{
			//arrange
			var cards = new[]
			{
				new TravelCard<string>("Владивосток", "Токио"),
				new TravelCard<string>("Магадан", "Владивосток"),
				new TravelCard<string>("Токио", "Лондон"),
				new TravelCard<string>("Якутск", "Магадан")
			};

			var expectedResult = new[]
			{
				new TravelCard<string>("Якутск", "Магадан"),
				new TravelCard<string>("Магадан", "Владивосток"),
				new TravelCard<string>("Владивосток", "Токио"),
				new TravelCard<string>("Токио", "Лондон")
			};

			//act
			var result = cards.GetTripPath();

			//assert
			CollectionAssert.AreEqual(expectedResult, result);
		}		
	}
}