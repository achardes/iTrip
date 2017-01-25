using System;
using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;

namespace iTrip
{
    [BsonIgnoreExtraElements]
    public class BorderCrossing : INotifyPropertyChanged, IEquatable<BorderCrossing>, ISupportInitialize
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [BsonIgnore]
        private BorderCrossing Initial { get; set; }
    
        public int VisaDuration { get; set; }
        public int VisaPrice { get; set; }

        public int VisaVehicleDuration { get; set; }
        public int VisaVehiclePrice { get; set; }

        public bool Fumigation { get; set; }
        public int FumigationPrice { get; set; }
        
        public bool VehicleInspection { get; set; }

        public bool Tramidores { get; set; }
        public int TramidoresPrice { get; set; }

        public string Comments { get; set; }

        public BorderCrossing()
        {
            EndInit();
        }

        public BorderCrossing(BorderCrossing other)
        {
            VisaDuration = other.VisaDuration;
            VisaPrice = other.VisaPrice;
            VisaVehicleDuration = other.VisaVehicleDuration;
            VisaVehiclePrice = other.VisaVehiclePrice;
            Fumigation = other.Fumigation;
            FumigationPrice = other.FumigationPrice;
            VehicleInspection = other.VehicleInspection;
            Tramidores = other.Tramidores;
            TramidoresPrice = other.TramidoresPrice;
            Comments = other.Comments;
        }

        public bool Equals(BorderCrossing other)
        {
            if (VisaDuration != other.VisaDuration) { return false; }
            if (VisaPrice != other.VisaPrice) { return false; }
            if (VisaVehicleDuration != other.VisaVehicleDuration) { return false; }
            if (VisaVehiclePrice != other.VisaVehiclePrice) { return false; }
            if (Fumigation != other.Fumigation) { return false; }
            if (FumigationPrice != other.FumigationPrice) { return false; }
            if (VehicleInspection != other.VehicleInspection) { return false; }
            if (Tramidores != other.Tramidores) { return false; }
            if (TramidoresPrice != other.TramidoresPrice) { return false; }
            if (Comments != other.Comments) { return false; }

            return true;
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            Initial = new BorderCrossing(this);
        }

        [BsonIgnore]
        public bool HasBeenChanged { get { return !this.Equals(Initial); } }
    }
}
