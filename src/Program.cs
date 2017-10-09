using System;
using System.Collections.Generic;



namespace CardsTask
{
	class Program
	{
		static IEnumerable<RouteItem> BuildRoute(IEnumerable<RouteItem> unorderedRoute)
		{
			var result = new List<RouteItem>();

			var routes = new Dictionary<string, string>();
			var destinations = new HashSet<string>();

			foreach (var routeItem in unorderedRoute)
			{
				routes.Add(routeItem.Departure, routeItem.Destination);
				destinations.Add(routeItem.Destination);
			}

			string currentCity = null;
			foreach (var departure in routes.Keys)
			{
				if (!destinations.Contains(departure))
				{
					currentCity = departure;

					break;
				}
			}

			if (currentCity != null)
			{
				string tmp;
				while (routes.TryGetValue(currentCity, out tmp))
				{
					result.Add(new RouteItem(currentCity, tmp));

					currentCity = tmp;
				}
			}

			return result;
		}

		static void Main(string[] args)
		{
			var route = BuildRoute(
				new[]
				{
					new RouteItem("Moscow", "Paris"),
					new RouteItem("Keln", "Moscow"),
					new RouteItem("Melburn", "Keln")
				});

			foreach (var routeItem in route)
			{
				Console.WriteLine("{0} => {1}", routeItem.Departure, routeItem.Destination);
			}
		}

		#region Nested type: RouteItem

		class RouteItem
		{
			public RouteItem(string departure, string destination)
			{
				Departure = departure;
				Destination = destination;
			}

			public string Departure { get; private set; }

			public string Destination { get; private set; }
		}

		#endregion
	}
}
