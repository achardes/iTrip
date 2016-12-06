using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripManager
{
    [BsonIgnoreExtraElements]
    public class Bivouac : AObservableObject, ISupportInitialize
    {
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(nameof(Type)); }
        }

        private string _note;
        public string Note
        {
            get { return _note; }
            set { _note = value; NotifyPropertyChanged(nameof(Note)); }
        }

        private int _distance;
        public int Distance
        {
            get { return _distance; }
            set { _distance = value; NotifyPropertyChanged(nameof(Distance)); }
        }

        private int _distanceTrack;
        public int DistanceTrack
        {
            get { return _distanceTrack; }
            set { _distanceTrack = value; NotifyPropertyChanged(nameof(DistanceTrack)); }
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

        private ObservableCollection<string> _tags;
        public ObservableCollection<string> Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
                NotifyPropertyChanged(nameof(Tags));
            }
        }

        public Bivouac(AObservableObject parent)
        {
            Parent = parent;
            Tags = new ObservableCollection<string>();
            Note = "Default";
            Location = new Location(this);
            ConstantManager constantManager = new ConstantManager();
            Type = constantManager.BivouacTypes.First();
        }

        public void BeginInit() { }

        public void EndInit()
        {
            Tags.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(tagsHaveChanged);
            Location.Parent = this;
        }

        private void tagsHaveChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
        {
            IsChanging();
        }
    }
}
