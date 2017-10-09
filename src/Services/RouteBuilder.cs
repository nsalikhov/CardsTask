using System.Collections.Generic;

using CardsTask.Exceptions;
using CardsTask.Models;



namespace CardsTask.Services
{
	public class RouteBuilder
	{
		public IEnumerable<RouteItem> BuildRoute(IEnumerable<RouteItem> unorderedRoute)
		{
			var result = new List<RouteItem>();

			var routes = new Dictionary<string, string>();
			var destinations = new HashSet<string>();

			foreach (var routeItem in unorderedRoute)
			{
				if (routes.ContainsKey(routeItem.Departure) || destinations.Contains(routeItem.Destination))
				{
					throw new CardsTaskException();
				}

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

			if (currentCity == null)
			{
				throw new CardsTaskException();
			}

			string tmp;
			while (routes.TryGetValue(currentCity, out tmp))
			{
				result.Add(new RouteItem(currentCity, tmp));

				currentCity = tmp;
			}

			return result;
		}
	}
}
