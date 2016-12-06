using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripManager
{
    [BsonIgnoreExtraElements]
    public class Period : AObservableObject
    {
        private DateTime _startDateTime;
        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set
            {
                _startDateTime = value;
                NotifyPropertyChanged(nameof(StartDateTime));
                NotifyPropertyChanged(nameof(DisplayName));
                NotifyPropertyChanged(nameof(ShortDisplayName));
            }
        }

        private int _numberOfDays;
        public int NumberOfDays
        {
            get { return _numberOfDays; }
            set
            {
                _numberOfDays = value;
                NotifyPropertyChanged(nameof(NumberOfDays));
                NotifyPropertyChanged(nameof(DisplayName));
                NotifyPropertyChanged(nameof(ShortDisplayName));
            }
        }

        public Period(AObservableObject parent)
        {
            Parent = parent;
            StartDateTime = DateTime.Now;
            NumberOfDays = 1;
        }

        public string DisplayName
        {
            get
            {
                if (NumberOfDays > 1) { return "Journey from " + StartDateTime.ToShortDateString() + " to " + StartDateTime.AddDays(NumberOfDays).ToShortDateString(); }
                else { return "Journey of " + StartDateTime.ToShortDateString(); }
            }
        }

        public string ShortDisplayName
        {
            get { return StartDateTime.DayOfWeek.ToString().Substring(0, 3) + " " + StartDateTime.ToShortDateString() + " (" + NumberOfDays + " day" + ((NumberOfDays > 1) ? "s" : "") + ")"; } 
        }
    }
}
