using Microsoft.Maps.MapControl.WPF;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RoadTripManager 
{
    [BsonIgnoreExtraElements]
    public class Journey : AObservableObject, ISupportInitialize
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public Period Period { get; set; }

        public Bivouac Bivouac { get; set; }

        public ObservableCollection<Event> Events { get; set; }

        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                OnlyNotifyPropertyChanged(nameof(SelectedEvent));
            }
        }

        public ObservableCollection<Spending> Spendings { get; set; }

        private LocationCollection _locationCollection;
        public LocationCollection LocationCollection
        {
            get { return _locationCollection; }
            set { _locationCollection = value; NotifyPropertyChanged(nameof(LocationCollection)); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnlyNotifyPropertyChanged(nameof(IsSelected)); }
        }

        private string _weather;
        public string Weather
        {
            get { return _weather; }
            set { _weather = value; NotifyPropertyChanged(nameof(Weather)); }
        }

        private string _note;
        public string Note
        {
            get { return _note; }
            set { _note = value; NotifyPropertyChanged(nameof(Note)); }
        }

        public Journey()
        {
            Period = new Period(this);
            Bivouac = new Bivouac(this);
            Events = new ObservableCollection<Event>();
            Spendings = new ObservableCollection<Spending>();

            ConstantManager constantManager = new ConstantManager();
            Weather = constantManager.WeatherKinds.First();
            Note = "Default";
        }

        public async void Save()
        {
            IMongoDatabase database = DataContext.GetMongoDatabase(DataContext.DatabaseName);
            IMongoCollection<Journey> collection = database.GetCollection<Journey>("journeys");

            if (Id == ObjectId.Empty) { await collection.InsertOneAsync(this); }
            else { await collection.ReplaceOneAsync<Journey>(x => x.Id == Id, this); }

            HasBeenChanged = false;
            Bivouac.HasBeenChanged = false;
            Period.HasBeenChanged = false;
            Events.ToList().ForEach(x => x.HasBeenChanged = false);
            Spendings.ToList().ForEach(x => x.HasBeenChanged = false);
        }

        public void Delete()
        {
            IMongoDatabase database = DataContext.GetMongoDatabase(DataContext.DatabaseName);

            IMongoCollection<Journey> collection = database.GetCollection<Journey>("journeys");
            collection.DeleteOneAsync(x => x.Id == Id);
        }

        RelayCommand _addEventCommand;
        public ICommand AddEventCommand
        {
            get
            {
                if (_addEventCommand == null)
                {
                    _addEventCommand = new RelayCommand(param => AddEvent(), param => true);
                }
                return _addEventCommand;
            }
        }

        public void AddEvent()
        {
            Event newEvent = new Event(this);
            newEvent.Location.City = Bivouac.Location.City;
            newEvent.Location.Country = Bivouac.Location.Country;
            Events.Add(newEvent);
        }

        RelayCommand _addSpendingCommand;
        public ICommand AddSpendingCommand
        {
            get
            {
                if (_addSpendingCommand == null)
                {
                    _addSpendingCommand = new RelayCommand(param => AddSpending(), param => true);
                }
                return _addSpendingCommand;
            }
        }

        public void AddSpending()
        {
            Spendings.Add(new Spending(this));
        }

        public void BeginInit() { }

        public void EndInit()
        {
            IsSelected = false;
            Period.Parent = this;
            Bivouac.Parent = this;
            Events.ToList().ForEach(x => x.Parent = this);
            Spendings.ToList().ForEach(x => x.Parent = this);
        }


        RelayCommand _buildWaypointsCommand;
        public ICommand BuildWaypointsCommand
        {
            get
            {
                if (_buildWaypointsCommand == null)
                {
                    _buildWaypointsCommand = new RelayCommand(param => BuildWaypoints(), param => true);
                }
                return _buildWaypointsCommand;
            }
        }

        public void BuildWaypoints()
        {
            Journey previousJourney = (Parent == null)? null : (Parent as JourneyManager).GetPreviousJourney(this);

            List<Location> locations = new List<Location>();

            if (previousJourney != null)
            {
                if (previousJourney.Events.Where(x => x.Location.HasValidCoordinates).Any())
                {
                    locations.Add(previousJourney.Events.Where(x => x.Location.HasValidCoordinates).Last().Location);
                }
                else if (previousJourney.Bivouac.Location.HasValidCoordinates)
                {
                    locations.Add(previousJourney.Bivouac.Location);
                }
            }

            locations.AddRange(Events.Where(x => x.Location.HasValidCoordinates).Select(x => x.Location));

            if (Bivouac.Location.HasValidCoordinates)
            {
                locations.Add(Bivouac.Location);
            }

            LocationCollection = MapHelper.CallBingRestApi(locations);
        }

        public void TraceWaypoints(Map map)
        {
            if (map == null) return;

            MapPolyline routeLine = new MapPolyline()
            {
                Locations = LocationCollection,
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00B3FD")),
                StrokeThickness = 5
            };

            map.Children.Add(routeLine);
            map.SetView(LocationCollection, new Thickness(25), 0);
        }
    }
}
