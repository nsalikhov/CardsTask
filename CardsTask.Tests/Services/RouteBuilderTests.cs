using System.Linq;

using CardsTask.Exceptions;
using CardsTask.Models;
using CardsTask.Services;
using CardsTask.Tests.Helpers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace CardsTask.Tests.Services
{
	[TestClass]
	public class RouteBuilderTests
	{
		[TestInitialize]
		public void TestInitialize()
		{
			_target = new RouteBuilder();
		}

		[TestMethod]
		[ExpectedException(typeof(CardsTaskException))]
		public void BuildRoute_EmptyRouteTest()
		{
			_target.BuildRoute(new RouteItem[0]);
		}

		[TestMethod]
		[ExpectedException(typeof(CardsTaskException))]
		public void BuildRoute_DuplicateDepartureTest()
		{
			_target.BuildRoute(
				new[]
				{
					new RouteItem("Moscow", "London"),
					new RouteItem("Moscow", "Beijing")
				});
		}

		[TestMethod]
		[ExpectedException(typeof(CardsTaskException))]
		public void BuildRoute_DuplicateDestinationTest()
		{
			_target.BuildRoute(
				new[]
				{
					new RouteItem("Moscow", "London"),
					new RouteItem("Beijing", "London")
				});
		}

		[TestMethod]
		[ExpectedException(typeof(CardsTaskException))]
		public void BuildRoute_CycleTest()
		{
			_target.BuildRoute(
				new[]
				{
					new RouteItem("Moscow", "London"),
					new RouteItem("London", "Beijing"),
					new RouteItem("Beijing", "Moscow")
				});
		}

		[TestMethod]
		public void BuildRoute_Test()
		{
			var route = RouteGenerator.GetRandomRoute().ToArray();
			var unorderedRoute = RouteGenerator.ShuffleRoute(route);

			var result = _target.BuildRoute(unorderedRoute);

			result.ShouldAllBeEquivalentTo(route, o => o.WithStrictOrdering());
		}

		private RouteBuilder _target;
	}
}
