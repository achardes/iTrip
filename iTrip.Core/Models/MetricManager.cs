using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripManager
{
    public class MetricManager : AObservableObject
    {
        public JourneyManager JourneyManager { get; set; }

        private Chart<Event> _eventSpendingPerType;
        public Chart<Event> EventSpendingPerType
        {
            get { return _eventSpendingPerType; }
            set { _eventSpendingPerType = value; OnlyNotifyPropertyChanged(nameof(EventSpendingPerType)); }
        }

        private Chart<Spending> _spendingPerType;
        public Chart<Spending> SpendingPerType
        {
            get { return _spendingPerType; }
            set { _spendingPerType = value; OnlyNotifyPropertyChanged(nameof(SpendingPerType)); }
        }

        public MetricManager(JourneyManager journeyManager)
        {
            JourneyManager = journeyManager;
        }

        public void BuildMetrics(List<Journey> journeys)
        {
            EventSpendingPerType = new Chart<Event>();
            SpendingPerType = new Chart<Spending>();

            var eventGroups = journeys.SelectMany(x => x.Events).GroupBy(x => x.TopType);
            foreach (var group in eventGroups)
            {
                ChartPoint<Event> chartPoint = new ChartPoint<Event>();
                chartPoint.Category = group.First().TopType;
                chartPoint.Number = group.Sum(x => x.Price);
                chartPoint.Items = group.ToList();
                EventSpendingPerType.Points.Add(chartPoint);
            }

            var spendingGroups = journeys.SelectMany(x => x.Spendings).GroupBy(x => x.TopType);
            foreach (var group in spendingGroups)
            {
                ChartPoint<Spending> chartPoint = new ChartPoint<Spending>();
                chartPoint.Category = group.First().TopType;
                chartPoint.Number = group.Sum(x => x.Price);
                chartPoint.Items = group.ToList();
                SpendingPerType.Points.Add(chartPoint);
            }
        }

    }
}
