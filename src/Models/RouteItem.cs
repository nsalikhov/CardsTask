namespace CardsTask.Models
{
	public class RouteItem
	{
		public RouteItem(string departure, string destination)
		{
			Departure = departure;
			Destination = destination;
		}

		public string Departure { get; private set; }

		public string Destination { get; private set; }
	}
}
