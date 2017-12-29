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
using MongoDB.Bson;
using Eto.Forms;
using System.ComponentModel;
using Eto.Drawing;

namespace iTrip
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ItemObservableCollection<JourneyViewModel> Journeys { get; set; }

        private JourneyViewModel _selectedJourney;
        public JourneyViewModel SelectedJourney
        {
            get { return _selectedJourney; }
            set { _selectedJourney = value; }
        }

        public Control SelectedJourneyControl
        {
            get 
            {
                if (SelectedJourney != null) { return SelectedJourney.Content; }
                return GetDefaultView();
            }
        }

        public bool HasSelectedJourney { get { return SelectedJourney != null; } }


        public MainViewModel()
        {
            Journeys = new ItemObservableCollection<JourneyViewModel>();

            IMongoDatabase database = DataContext.GetMongoDatabase(DataContext.DatabaseName);
            IMongoCollection<Journey> collection = database.GetCollection<Journey>("journeys");
            List<Journey> journeysFromDatabase = collection.Find(x => true).ToListAsync().Result;

            List<Journey> journeysFromDatabaseOrdered = journeysFromDatabase.OrderByDescending(x => x.FromDateTime).ToList();

            foreach (var journey in journeysFromDatabaseOrdered)
            {
                Journeys.Add(new JourneyViewModel(journey));
            }

            Journeys.CollectionChanged += (sender, e) => UpdateApplicationBadge();
            Journeys.ItemPropertyChanged += (sender, e) => UpdateApplicationBadge();

        }


        public void Add(DateTime fromDateTime, DateTime toDateTime)
        {
            Journey journey = new Journey(fromDateTime.Date.AddHours(12).ToUniversalTime(), toDateTime.Date.AddHours(12).ToUniversalTime());
            JourneyViewModel journeyViewModel = new JourneyViewModel(journey);
            Journeys.Insert(0, journeyViewModel);
            journey.Save();
            UpdateApplicationBadge();
            SelectedJourney = journeyViewModel;
        }

        public void Save()
        {
            if (SelectedJourney != null)
            {
                SelectedJourney.Journey.Save();
            }
        }

        public void Delete()
        {
            if (SelectedJourney != null)
            {
                var journeyToDelete = SelectedJourney;
                SelectedJourney = null;
                Journeys.Remove(journeyToDelete);
                journeyToDelete.Journey.Delete();
            }
        }

        public bool IsAnythingToSave()
        {
            return Journeys.Any(x => x.Journey.HasBeenChanged);
        }

        public void UpdateApplicationBadge()
        {
            int numberOfUnsavedJourneys = Journeys.Count(x => x.Journey.HasBeenChanged);
            Application.Instance.BadgeLabel = (numberOfUnsavedJourneys > 0) ? numberOfUnsavedJourneys.ToString() : string.Empty;
        }

        public Control GetDefaultView()
        {
            var layout = new TableLayout();
            layout.Rows.Add(null);
            layout.Rows.Add(new TableRow(null, Bitmap.FromResource("iTrip.Images.iTripImage.png"), null));
            layout.Rows.Add(null);

            return layout;
        }
    }
}
