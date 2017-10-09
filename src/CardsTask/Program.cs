using System;

using CardsTask.Models;
using CardsTask.Services;



namespace CardsTask
{
	class Program
	{
		static void Main(string[] args)
		{
			var route = new RouteBuilder().BuildRoute(
				new[]
				{
					new RouteItem("Moscow", "Paris"),
					new RouteItem("Keln", "Moscow"),
					new RouteItem("Melburn", "Keln")
				});

			foreach (var routeItem in route)
			{
				Console.WriteLine($"{routeItem.Departure} => {routeItem.Destination}");
			}
		}
	}
}
