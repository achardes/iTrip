using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripManager
{
    public class Location : AObservableObject
    {
        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; NotifyPropertyChanged(nameof(Longitude)); }
        }

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; NotifyPropertyChanged(nameof(Latitude)); }
        }

        private double _elevation;
        public double Elevation
        {
            get { return _elevation; }
            set { _elevation = value; NotifyPropertyChanged(nameof(Elevation)); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; NotifyPropertyChanged(nameof(Address)); }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; NotifyPropertyChanged(nameof(City)); }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; NotifyPropertyChanged(nameof(Country)); }
        }

        public Location(AObservableObject parent)
        {
            Parent = parent;
            _longitude = 0;
            _latitude = 0;
            _elevation = 0;
            ConstantManager constantManager = new ConstantManager();
            Country = constantManager.Countries.First();
        }

        public string Coordinates
        {
            get { return Latitude + " " + Longitude; }
        }

        public string KlmCoordinates
        {
            get { return Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + Elevation.ToString(System.Globalization.CultureInfo.InvariantCulture); }
        }

        public bool HasValidCoordinates
        {
            get { return (Longitude != 0 && Latitude != 0); }
        }
    }
}
