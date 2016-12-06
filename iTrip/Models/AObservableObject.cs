using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripManager
{
    public abstract class  AObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                IsChanging();
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnlyNotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void IsChanging()
        {
            HasBeenChanged = true;
            if (Parent != null) { Parent.IsChanging(); }
        }

        private bool _hasBeenChanged;
        [BsonIgnore]
        public bool HasBeenChanged
        {
            get { return _hasBeenChanged; }
            set
            {
                _hasBeenChanged = value;
                OnlyNotifyPropertyChanged(nameof(HasBeenChanged));
            }
        }

        [BsonIgnore]
        public AObservableObject Parent { get; set; }
    }
}
