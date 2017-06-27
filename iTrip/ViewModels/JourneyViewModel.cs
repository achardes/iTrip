using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Eto.Forms;

namespace iTrip
{
    public class JourneyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Journey Journey { get; set; }

        public string Text { get { return Journey.ShortDisplayName; } }

        public string TextDescription { get { return Journey.ShortDisplayName + " " + Journey.Bivouac.Country + " " + Journey.Bivouac.City; } }

        public Control Content { get; set; }

        public JourneyViewModel(Journey journey)
        {
            Journey = journey;
            Content = JourneyView.GetView(this);

            Journey.PropertyChanged += (sender, e) => { OnPropertyChanged(nameof(Text)); };
        }

        public List<string> GetWayPoints()
        {
            List<KeyValuePair<int, string>> waypoints = new List<KeyValuePair<int, string>>();
            foreach (var item in Journey.Events.Where(x => MapHelper.IsValidCoordinates(x.Coordinates)))
            {
                waypoints.Add(new KeyValuePair<int, string>(item.Order, item.Coordinates));
            }
            foreach (var item in Journey.Spendings.Where(x => MapHelper.IsValidCoordinates(x.Coordinates)))
            {
                waypoints.Add(new KeyValuePair<int, string>(item.Order, item.Coordinates));
            }

            List<string> orderedWaypoints = waypoints.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            if (MapHelper.IsValidCoordinates(Journey.Bivouac.Coordinates)) { orderedWaypoints.Add(Journey.Bivouac.Coordinates); }

            return orderedWaypoints;
        }

        public string GoogleMapParameters
        {
            get { return MapHelper.GetGoogleMapParameters(GetWayPoints()); }
        }

        public Event SelectedEvent { get; set; }
        public Spending SelectedSpending { get; set; }

        public bool HasSelectedEvent { get { return SelectedEvent != null; } }
        public bool HasSelectedSpending { get { return SelectedSpending != null; } }

        public void AddEvent()
        {
            Journey.Events.Add(new Event(Journey.Events.Count() + 1, Journey?.Bivouac?.Country));
        }

        public void DeleteEvent()
        {
            if (SelectedEvent != null)
            {
                Journey.Events.Remove(SelectedEvent);
            }
        }

        public void AddSpending()
        {
            Journey.Spendings.Add(new Spending(Journey.Spendings.Count() + 1));
        }

        public void DeleteSpending()
        {
            if (SelectedSpending != null)
            {
                Journey.Spendings.Remove(SelectedSpending);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }
   }
}
