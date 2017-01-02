using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace iTrip
{
    [BsonIgnoreExtraElements]
    public class Bivouac : INotifyPropertyChanged, IEquatable<Bivouac>, ISupportInitialize
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [BsonIgnore] 
        private Bivouac Initial { get; set; }

        public string Type { get; set; }
        public string Note { get; set; }
        public int Distance { get; set; }
        public int DistanceTrack { get; set; }
        public string Comments { get; set; }
        public ObservableCollection<string> Tags { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Elevation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Bivouac()
        {
            Tags = new ObservableCollection<string>();
            Note = "Default";
            Type = ConstantManager.Instance.BivouacTypes.First();
            Comments = string.Empty;
            Longitude = 0;
            Latitude = 0;
            Elevation = 0;
            Address = string.Empty;
            City = string.Empty;
            Country = ConstantManager.Instance.Countries.First();

            EndInit();
        }

        public Bivouac(Bivouac other)
        {
            Tags = new ObservableCollection<string>(other.Tags);
            Note = other.Note;
            Type = other.Type;
            Comments = other.Comments;
            Longitude = other.Longitude;
            Latitude = other.Latitude;
            Elevation = other.Elevation;
            Address = other.Address;
            City = other.City;
            Country = other.Country;
        }

        public bool Equals(Bivouac other)
        {
            if (Type != other.Type) { return false; }
            if (Note != other.Note) { return false; }
            if (Distance != other.Distance) { return false; }
            if (DistanceTrack != other.DistanceTrack) { return false; }
            if (Comments != other.Comments) { return false; }
            if (Longitude != other.Longitude) { return false; }
            if (Latitude != other.Latitude) { return false; }
            if (Elevation != other.Elevation) { return false; }
            if (Address != other.Address) { return false; }
            if (City != other.City) { return false; }
            if (Country != other.Country) { return false; }
            if (!Tags.OrderBy(i => i).SequenceEqual(other.Tags.OrderBy(i => i))) { return false; } 

            return true;
        }

        public void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasBeenChanged));
        }

        [BsonIgnore]
        public bool HasBeenChanged { get { return !this.Equals(Initial); } }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        public void BeginInit()
        {
            
        }

        public void EndInit()
        {
            Initial = new Bivouac(this);
        }



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
