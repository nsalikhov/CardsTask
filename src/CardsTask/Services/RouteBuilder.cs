using System;
using System.Collections.Generic;

using CardsTask.Exceptions;
using CardsTask.Models;



namespace CardsTask.Services
{
	public class RouteBuilder
	{
		public IEnumerable<RouteItem> BuildRoute(IEnumerable<RouteItem> unorderedRoute)
		{
			if (unorderedRoute == null)
			{
				throw new ArgumentNullException(nameof(unorderedRoute));
			}

			var result = new List<RouteItem>();

			var routes = new Dictionary<string, string>();
			var destinations = new HashSet<string>();

			// Building a route map for fast determination of destination by departure
			foreach (var routeItem in unorderedRoute)
			{
				// Avoiding route cycles and duplicates 
				if (routes.ContainsKey(routeItem.Departure) || destinations.Contains(routeItem.Destination))
				{
					throw new CardsTaskException();
				}

				routes.Add(routeItem.Departure, routeItem.Destination);
				destinations.Add(routeItem.Destination);
			}

			// Finding start destination point
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

			// Route building
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
