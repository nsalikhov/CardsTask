using System;
using System.Collections.Generic;
using System.Linq;

using CardsTask.Models;

using Ploeh.AutoFixture;



namespace CardsTask.Tests.Helpers
{
	public static class RouteGenerator
	{
		public static IEnumerable<RouteItem> GetRandomRoute()
		{
			var routeItems = Enumerable
				.Range(0, _random.Next(5, 30))
				.Select(x => _fixture.Create<string>())
				.ToArray();

			var result = new List<RouteItem>();

			for (int i = 0; i < routeItems.Length - 1; i++)
			{
				result.Add(new RouteItem(routeItems[i], routeItems[i + 1]));
			}

			return result;
		}

		public static IEnumerable<RouteItem> ShuffleRoute(IEnumerable<RouteItem> route)
		{
			return route.OrderBy(x => _random.Next());
		}

		private static readonly Random _random = new Random();
		private static readonly Fixture _fixture = new Fixture();
	}
}
