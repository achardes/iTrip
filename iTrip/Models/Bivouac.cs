using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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
		public int Walk { get; set; }
        public bool Photo { get; set; }
        public int WakeUpTemperature { get; set; }
        public string Comments { get; set; }
        public ObservableCollection<string> Tags { get; set; }

        public string Coordinates { get; set; }
        public double Elevation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool FromIOverLander { get; set; }
        public bool ToIOverLander { get; set; }

        public Bivouac()
        {
            Tags = new ObservableCollection<string>();
            Note = "Default";
            Type = ConstantManager.Instance.BivouacTypes.First();
            Comments = string.Empty;
            Coordinates = string.Empty;
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
            Distance = other.Distance;
			DistanceTrack = other.DistanceTrack;
            Walk = other.Walk;
            WakeUpTemperature = other.WakeUpTemperature;
            Photo = other.Photo;

            Coordinates = other.Coordinates;
            Elevation = other.Elevation;
            Address = other.Address;
            City = other.City;
            Country = other.Country;
            FromIOverLander = other.FromIOverLander;
            ToIOverLander = other.ToIOverLander;
        }

        public bool Equals(Bivouac other)
        {
            if (Type != other.Type) { return false; }
            if (Note != other.Note) { return false; }
            if (Distance != other.Distance) { return false; }
			if (DistanceTrack != other.DistanceTrack) { return false; }
			if (Walk != other.Walk) { return false; }
            if (Comments != other.Comments) { return false; }
            if (Coordinates != other.Coordinates) { return false; }
            if (Elevation != other.Elevation) { return false; }
            if (Address != other.Address) { return false; }
            if (City != other.City) { return false; }
            if (Country != other.Country) { return false; }
            if (FromIOverLander != other.FromIOverLander) { return false; }
            if (ToIOverLander != other.ToIOverLander) { return false; }
            if (WakeUpTemperature != other.WakeUpTemperature) { return false; }
            if (Photo != other.Photo) { return false; }
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

        public List<Tag> GetBivouacTags()
        {
            var tags = new List<Tag>();
            ConstantManager.Instance.BivouacTags.ToList().ForEach(x => tags.Add(new Tag(Tags, x, Tags.Contains(x))));
            return tags;
        }
    }

    public class Tag
    {
        private ObservableCollection<string> Tags { get; set; }
        public string Name { get; set; }

        private bool _isChecked;
        public bool IsChecked 
        { 
            get { return _isChecked; }
            set 
            {
                if (value) { if (!Tags.Contains(Name)) { Tags.Add(Name); } }
                else { if (Tags.Contains(Name)) { Tags.Remove(Name); } }
                _isChecked = value;
            }
        }

        public Tag(ObservableCollection<string> tags, string name, bool isChecked)
        {
            Tags = tags;
            Name = name;
            IsChecked = isChecked;
        }
    }
}
