using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripManager
{
    public class Spending : AObservableObject
    {
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(nameof(Type)); }
        }

        private double _quantity;
        public double Quantity
        {
            get { return _quantity; }
            set { _quantity = value; NotifyPropertyChanged(nameof(Quantity)); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = value; NotifyPropertyChanged(nameof(Price)); }
        }

        private string _comments;
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; NotifyPropertyChanged(nameof(Comments)); }
        }
 
        public Spending(AObservableObject parent)
        {
            Parent = parent;
            ConstantManager constantManager = new ConstantManager();
            Type = constantManager.BivouacTypes.First();
        }

        public string TopType
        {
            get { return (string.IsNullOrWhiteSpace(Type)) ? "" : Type.Split(':').First().Trim(); }
        }
    }
}
