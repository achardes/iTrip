using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTrip
{
    [BsonIgnoreExtraElements]
    public class Event : INotifyPropertyChanged, IEquatable<Event>, ISupportInitialize
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [BsonIgnore]
        private Event Initial { get; set; }

        public int Order { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
        public string Note { get; set; }
        public string Duration { get; set; }
        public string Comments { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Elevation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Event(int order)
        {
            Order = order;
            Name = "New Event";
            Duration = "0";
            Price = "0";
            Comments = "";
            Type = ConstantManager.Instance.BivouacTypes.First();
            Note = "2";

            Longitude = 0;
            Latitude = 0;
            Elevation = 0;
            Address = "";
            City = "";
            Country = ConstantManager.Instance.Countries.First();

            EndInit();
        }

        public Event(Event other)
        {
            Order = other.Order;
            Name = other.Name;
            Duration = other.Duration;
            Price = other.Price;
            Comments = other.Comments;
            Type = other.Type;
            Note = other.Note;

            Longitude = other.Longitude;
            Latitude = other.Latitude;
            Elevation = other.Elevation;
            Address = other.Address;
            City = other.City;
            Country = other.Country;
        }

        public bool Equals(Event other)
        {
            if (Order != other.Order) { return false; }
            if (Name != other.Name) { return false; }
            if (Type != other.Type) { return false; }
            if (Price != other.Price) { return false; }
            if (Note != other.Note) { return false; }
            if (Duration != other.Duration) { return false; }
            if (Comments != other.Comments) { return false; }

            if (Longitude != other.Longitude) { return false; }
            if (Latitude != other.Latitude) { return false; }
            if (Elevation != other.Elevation) { return false; }
            if (Address != other.Address) { return false; }
            if (City != other.City) { return false; }
            if (Country != other.Country) { return false; }

            return true;
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            Initial = new Event(this);
        }

        [BsonIgnore]
        public bool HasBeenChanged { get { return !this.Equals(Initial); } }


        public string GoogleCoordinates
        {
            get { return Latitude + "," + Longitude; }
        }

        public bool HasValidCoordinates
        {
            get { return (Longitude != 0 && Latitude != 0); }
        }
    }
}
