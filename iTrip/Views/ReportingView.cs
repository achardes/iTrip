using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Forms;

namespace iTrip
{
    public static class ReportingView
    {
        public static Control GetView(List<JourneyViewModel> journeys)
        {
            TabControl tabControl = new TabControl();

            TabPage mainPage = new TabPage(GetMainPage(journeys));
            mainPage.Text = "General";

            TabPage weatherPage = new TabPage(GetWeatherPage(journeys));
            weatherPage.Text = "Weather";

			TabPage bivouacPage = new TabPage(GetBivouacPage(journeys));
			bivouacPage.Text = "Bivouac";

            TabPage eventPage = new TabPage(GetEventPage(journeys));
            eventPage.Text = "Event";

			TabPage globalEventPage = new TabPage(GetGlobalEventPage(journeys));
			globalEventPage.Text = "Global Event";

            TabPage spendingPage = new TabPage(GetSpendingPage(journeys));
            spendingPage.Text = "Expenses";

            TabPage globalSpendingPage = new TabPage(GetGlobalSpendingPage(journeys));
			globalSpendingPage.Text = "Global Expenses";

            tabControl.Pages.Add(mainPage);
			tabControl.Pages.Add(weatherPage);
			tabControl.Pages.Add(bivouacPage);
			tabControl.Pages.Add(eventPage);
			tabControl.Pages.Add(globalEventPage);
			tabControl.Pages.Add(spendingPage);
			tabControl.Pages.Add(globalSpendingPage);

            return tabControl;
        }

        public static Control GetMainPage(List<JourneyViewModel> journeys)
        {
            double totalEventExpense = journeys.Select(x => x.Journey).SelectMany(x => x.Events).Sum(x => x.Price);
            double totalSpendingExpense = journeys.Select(x => x.Journey).SelectMany(x => x.Spendings).Sum(x => x.Price);
            double totalExpense = totalEventExpense + totalSpendingExpense;

            List<object> measures = new List<object>();
            measures.Add(new KeyValuePair<string, string>("Number of journeys", journeys.Count.ToString()));
            measures.Add(new KeyValuePair<string, string>("Number of days", (journeys.Sum(x => x.Journey.Duration)).ToString()));
            measures.Add(new KeyValuePair<string, string>("Number of actual days", (journeys.Last().Journey.ToDateTime - journeys.First().Journey.FromDateTime).TotalDays.ToString()));
            measures.Add(new KeyValuePair<string, string>("Traveled range from beginning", (journeys.Last().Journey.Bivouac.Distance - ConstantManager.Instance.InitialTraveledDistance).ToString()));
            measures.Add(new KeyValuePair<string, string>("Traveled range", (journeys.Last().Journey.Bivouac.Distance - journeys.First().Journey.Bivouac.Distance).ToString()));
            measures.Add(new KeyValuePair<string, string>("Traveled range (track)", (journeys.Sum(x => x.Journey.Bivouac.DistanceTrack)).ToString()));
            measures.Add(new KeyValuePair<string, string>("Total event expense", totalEventExpense.ToString()));
            measures.Add(new KeyValuePair<string, string>("Total spending expense", totalSpendingExpense.ToString()));
			measures.Add(new KeyValuePair<string, string>("Total expense", totalExpense.ToString()));
            measures.Add(new KeyValuePair<string, string>("Total walk", journeys.Select(x => x.Journey.Bivouac).Sum(x => x.Walk).ToString()));

            var grid = new GridView { DataStore = measures, Height = 400, AllowMultipleSelection = true };
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<KeyValuePair<string, string>, string>(r => r.Key) }, HeaderText = "Name" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<KeyValuePair<string, string>, string>(r => r.Value) }, HeaderText = "Value" });
            return grid;
        }

        public static Control GetWeatherPage(List<JourneyViewModel> journeys)
        {
            List<object> measures = new List<object>();

            foreach (var weather in ConstantManager.Instance.WeatherKinds)
            {
                string number = journeys.Count(x => x.Journey.Weather == weather).ToString();
                string percentage = Math.Round(((Convert.ToDouble(journeys.Count(x => x.Journey.Weather == weather)) / Convert.ToDouble(journeys.Count)) * 100), 2) + "%";

				measures.Add(new Repartition() { Name = weather, Number = number, Percentage = percentage });
            }

            var grid = new GridView { DataStore = measures, Height = 400, AllowMultipleSelection = true };
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Name) }, HeaderText = "Name" });
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Number) }, HeaderText = "Nombre" });
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Percentage) }, HeaderText = "Percentage" });
			return grid;
        }

		public static Control GetBivouacPage(List<JourneyViewModel> journeys)
		{
			List<object> measures = new List<object>();

            foreach (var type in ConstantManager.Instance.BivouacTypes)
			{
				string number = journeys.Count(x => x.Journey.Bivouac.Type == type).ToString();
				string percentage = Math.Round(((Convert.ToDouble(journeys.Count(x => x.Journey.Bivouac.Type == type)) / Convert.ToDouble(journeys.Count)) * 100), 2) + "%";

				measures.Add(new Repartition() { Name = type, Number = number, Percentage = percentage });
            }

			var grid = new GridView { DataStore = measures, Height = 400, AllowMultipleSelection = true };
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Name) }, HeaderText = "Name" });
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Number) }, HeaderText = "Nombre" });
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Percentage) }, HeaderText = "Percentage" });
			return grid;
		}

        public static Control GetEventPage(List<JourneyViewModel> journeys)
        {
            List<Repartition> measures = new List<Repartition>();
            List<Event> events = journeys.SelectMany(x => x.Journey.Events).ToList();

            foreach (var eventType in ConstantManager.Instance.EventTypes)
            {
                List<Event> localEvents = events.Where(x => x.Type == eventType).ToList();
                string number = localEvents.Count.ToString();
                string percentage = (events.Count > 0) ? Math.Round(((Convert.ToDouble(localEvents.Count) / Convert.ToDouble(events.Count)) * 100), 2) + "%" : "0%";
                string price = localEvents.Sum(x => x.Price).ToString();
                string euro = localEvents.Sum(x => x.Euro).ToString();

                measures.Add(new Repartition (){ Name = eventType, Number = number, Percentage = percentage, Price = price, Euro = euro });
            }

            var grid = new GridView { DataStore = measures, Height = 400, AllowMultipleSelection = true };
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Name) }, HeaderText = "Name" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Number) }, HeaderText = "Nombre" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Percentage) }, HeaderText = "Percentage" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Price) }, HeaderText = "Price" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Euro) }, HeaderText = "Euro" });
            return grid;
        }

		public static Control GetGlobalEventPage(List<JourneyViewModel> journeys)
		{
			List<Repartition> measures = new List<Repartition>();
			List<Event> events = journeys.SelectMany(x => x.Journey.Events).ToList();

            List<string> types = ConstantManager.Instance.EventTypes.Select(x => GetHeader(x)).Distinct().ToList();

			foreach (var eventType in types)
			{
                List<Event> localEvents = events.Where(x => GetHeader(x.Type) == eventType).ToList();
				string number = localEvents.Count.ToString();
                string percentage = (events.Count > 0) ? Math.Round(((Convert.ToDouble(localEvents.Count) / Convert.ToDouble(events.Count)) * 100), 2) + "%" : "0%";
                string price = localEvents.Sum(x => x.Price).ToString();
                string euro = localEvents.Sum(x => x.Euro).ToString();

                measures.Add(new Repartition() { Name = eventType, Number = number, Percentage = percentage, Price = price, Euro = euro });
			}

			var grid = new GridView { DataStore = measures, Height = 400, AllowMultipleSelection = true };
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Name) }, HeaderText = "Name" });
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Number) }, HeaderText = "Nombre" });
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Percentage) }, HeaderText = "Percentage" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Price) }, HeaderText = "Price" });
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Euro) }, HeaderText = "Euro" });
			return grid;
		}

        public static Control GetSpendingPage(List<JourneyViewModel> journeys)
        {
            List<Repartition> measures = new List<Repartition>();
            List<Spending> spendings = journeys.SelectMany(x => x.Journey.Spendings).ToList();

            foreach (var spendingType in ConstantManager.Instance.SpendingTypes)
            {
                List<Spending> localSpendings = spendings.Where(x => x.Type == spendingType).ToList();
                string number = localSpendings.Count.ToString();
                string percentage = (spendings.Count > 0)? Math.Round(((Convert.ToDouble(localSpendings.Sum(x => x.Price)) / Convert.ToDouble(spendings.Sum(x => x.Price))) * 100), 2) + "%" : "0%";
                string price = localSpendings.Sum(x => x.Price).ToString();
                string euro = localSpendings.Sum(x => x.Euro).ToString();

                measures.Add(new Repartition() { Name = spendingType, Number = number, Percentage = percentage, Price = price, Euro = euro });
            }

            var grid = new GridView { DataStore = measures, Height = 400, AllowMultipleSelection = true };
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Name) }, HeaderText = "Name" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Number) }, HeaderText = "Number" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Percentage) }, HeaderText = "Percentage" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Price) }, HeaderText = "Price" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Euro) }, HeaderText = "Euro" });
            return grid;
        }

		public static Control GetGlobalSpendingPage(List<JourneyViewModel> journeys)
		{
			List<Repartition> measures = new List<Repartition>();
			List<Spending> spendings = journeys.SelectMany(x => x.Journey.Spendings).ToList();

			List<string> types = ConstantManager.Instance.SpendingTypes.Select(x => GetHeader(x)).Distinct().ToList();

			foreach (var spendingType in types)
			{
                List<Spending> localSpendings = spendings.Where(x => GetHeader(x.Type) == spendingType).ToList();
				string number = localSpendings.Count.ToString();
                string percentage = (spendings.Count > 0) ? Math.Round(((Convert.ToDouble(localSpendings.Sum(x => x.Price)) / Convert.ToDouble(spendings.Sum(x => x.Price))) * 100), 2) + "%" : "0%";
                string price = localSpendings.Sum(x => x.Price).ToString();
                string euro = localSpendings.Sum(x => x.Euro).ToString();

				measures.Add(new Repartition() { Name = spendingType, Number = number, Percentage = percentage, Price = price, Euro = euro });
			}

            var grid = new GridView { DataStore = measures, Height = 400, AllowMultipleSelection = true };
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Name) }, HeaderText = "Name" });
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Number) }, HeaderText = "Number" });
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Percentage) }, HeaderText = "Percentage" });
            grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Price) }, HeaderText = "Price" });
			grid.Columns.Add(new GridColumn { DataCell = new TextBoxCell { Binding = Binding.Property<Repartition, string>(r => r.Euro) }, HeaderText = "Euro" });
			return grid;
		}

        public static string GetHeader(string str)
        {
            return str.Split(':').First();
        }
    }

    public class Repartition
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Percentage { get; set; }
        public string Price { get; set; }
        public string Euro { get; set; }
    }
}
