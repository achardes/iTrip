using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripManager
{
    [BsonIgnoreExtraElements]
    public class Event : AObservableObject, ISupportInitialize
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(nameof(Type)); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = value; NotifyPropertyChanged(nameof(Price)); }
        }

        private string _note;
        public string Note
        {
            get { return _note; }
            set { _note = value; NotifyPropertyChanged(nameof(Note)); }
        }

        private double _duration;
        public double Duration
        {
            get { return _duration; }
            set { _duration = value; NotifyPropertyChanged(nameof(Duration)); }
        }

        private Location _location;
        public Location Location
        {
            get { return _location; }
            set { _location = value; NotifyPropertyChanged(nameof(Location)); }
        }

        private string _comments;
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; NotifyPropertyChanged(nameof(Comments)); }
        }

        public Event(AObservableObject parent)
        {
            Parent = parent;
            Location = new Location(this);
            ConstantManager constantManager = new ConstantManager();
            Type = constantManager.BivouacTypes.First();
            Note = "Default";
        }

        public string TopType
        {
            get { return (string.IsNullOrWhiteSpace(Type))? "" : Type.Split(':').First().Trim(); }
        }

        public void BeginInit(){ }

        public void EndInit()
        {
            Location.Parent = this;
        }
    }
}
