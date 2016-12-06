using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using Microsoft.Maps.MapControl.WPF;
using System.Windows.Input;

namespace RoadTripManager
{
    public class JourneyManager : AObservableObject
    {
        public ObservableCollection<Journey> Journeys { get; set; }

        private ObservableCollection<Journey> _filteredJourneys;
        public ObservableCollection<Journey> FilteredJourneys
        {
            get { return _filteredJourneys; }
            set { _filteredJourneys = value; OnlyNotifyPropertyChanged(nameof(FilteredJourneys)); }
        }
        //public Visibility DetailsPaneVisibility
        //{
        //    get { return (SelectedJourney != null) ? Visibility.Visible : Visibility.Hidden; }
        //}

        private Journey _selectedJourney;
        public Journey SelectedJourney
        {
            get
            {
                return _selectedJourney;
            }
            set
            {
                if (_selectedJourney != null) { _selectedJourney.IsSelected = false; }
                _selectedJourney = value;
                if (_selectedJourney != null) { _selectedJourney.IsSelected = true; }

                NotifyPropertyChanged(nameof(SelectedJourney));
                //NotifyPropertyChanged(nameof(DetailsPaneVisibility));

                //_selectedJourney.TraceWaypoints(Map);
            }
        }

        private JourneyFilter _journeyFilter;
        public JourneyFilter JourneyFilter
        {
            get { return _journeyFilter; }
            set { _journeyFilter = value; OnlyNotifyPropertyChanged(nameof(JourneyFilter)); }
        }

        private MetricManager _metricManager;
        public MetricManager MetricManager
        {
            get { return _metricManager; }
            set { _metricManager = value; OnlyNotifyPropertyChanged(nameof(MetricManager)); }
        }

        //public Map Map { get; set; }

        private int _tabControlSelectedIndex;
        public int TabControlSelectedIndex
        {
            get { return _tabControlSelectedIndex; }
            set { _tabControlSelectedIndex = value; OnlyNotifyPropertyChanged(nameof(TabControlSelectedIndex)); }
        }

        public JourneyManager()
        {
            IMongoDatabase database = DataContext.GetMongoDatabase(DataContext.DatabaseName);
            IMongoCollection<Journey> collection = database.GetCollection<Journey>("journeys");

            Journeys = new ObservableCollection<Journey>(collection.Find(x => true).ToListAsync().Result);
            Journeys.ToList().ForEach(x => x.Parent = this);

            FilteredJourneys = new ObservableCollection<Journey>(Journeys.OrderBy(x => x.Period.StartDateTime));

            if (FilteredJourneys.Any()) { SelectedJourney = FilteredJourneys.Last(); }
            TabControlSelectedIndex = 0;

            JourneyFilter = new JourneyFilter();
            MetricManager = new MetricManager(this);
            MetricManager.BuildMetrics(FilteredJourneys.ToList());
        }

        //RelayCommand _addCommand;
        //public ICommand AddCommand
        //{
        //    get
        //    {
        //        if (_addCommand == null)
        //        {
        //            _addCommand = new RelayCommand(param => this.Add(),
        //                param => true);
        //        }
        //        return _addCommand;
        //    }
        //}

        //RelayCommand _saveCommand;
        //public ICommand SaveCommand
        //{
        //    get
        //    {
        //        if (_saveCommand == null)
        //        {
        //            _saveCommand = new RelayCommand(param => Save(),
        //                param => CanSave());
        //        }
        //        return _saveCommand;
        //    }
        //}

        //RelayCommand _applyFilterCommand;
        //public ICommand ApplyFilterCommand
        //{
        //    get
        //    {
        //        if (_applyFilterCommand == null)
        //        {
        //            _applyFilterCommand = new RelayCommand(param => ApplyFilter(), param => true);
        //        }
        //        return _applyFilterCommand;
        //    }
        //}

        //RelayCommand _resetFilterCommand;
        //public ICommand ResetFilterCommand
        //{
        //    get
        //    {
        //        if (_resetFilterCommand == null)
        //        {
        //            _resetFilterCommand = new RelayCommand(param => ResetFilter(), param => true);
        //        }
        //        return _resetFilterCommand;
        //    }
        //}

        private bool CanSave()
        {
            return (SelectedJourney != null && SelectedJourney.HasBeenChanged);
        }

        private void Save()
        {
            SelectedJourney.Save();
        }

        private void Add()
        {
            Journey journey = new Journey();
            journey.HasBeenChanged = true;

            Journeys.Add(journey);
            FilteredJourneys.Add(journey);
        }

        private void ApplyFilter()
        {
            List<Journey> journeys = Journeys.ToList();

            if (JourneyFilter.StartDateTime.HasValue)
            { journeys = journeys.Where(x => x.Period.StartDateTime >= JourneyFilter.StartDateTime).ToList(); }

            if (JourneyFilter.EndDateTime.HasValue)
            { journeys = journeys.Where(x => x.Period.StartDateTime <= JourneyFilter.EndDateTime).ToList(); }

            if(!string.IsNullOrWhiteSpace(JourneyFilter.Country))
            {
                journeys = journeys.Where(x => x.Bivouac.Location.Country.ToLower().Contains(JourneyFilter.Country.ToLower())).ToList();
            }

            FilteredJourneys = new ObservableCollection<Journey>(journeys.OrderBy(x => x.Period.StartDateTime));
            MetricManager.BuildMetrics(journeys);
        }

        private void ResetFilter()
        {
            JourneyFilter = new JourneyFilter();
            FilteredJourneys = Journeys;
            MetricManager.BuildMetrics(Journeys.ToList());
        }

        public Journey GetPreviousJourney(Journey journey)
        {
            List<Journey> journeys = Journeys.OrderBy(x => x.Period.StartDateTime).ToList();
            int index = journeys.IndexOf(journey);
            return journeys.ElementAtOrDefault(index + 1);
        }
    }
}
