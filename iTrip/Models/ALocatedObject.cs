using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTrip
{
    public abstract class ALocatedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Elevation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string Coordinates
        {
            get { return Latitude + " " + Longitude; }
        }
        public string KlmCoordinates
        {
            get { return Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + Elevation.ToString(System.Globalization.CultureInfo.InvariantCulture); }
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
