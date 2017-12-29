using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace iTrip
{
    [BsonIgnoreExtraElements]
    public class Spending : INotifyPropertyChanged, IEquatable<Spending>, ISupportInitialize
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [BsonIgnore]
        private Spending Initial { get; set; }

        public int Order { get; set; }
        public string Type { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Euro { get; set; }
        public double UnitaryPrice { get; set; }
        public string Coordinates { get; set; }
        public string Comments { get; set; }
 
        public Spending(int order)
        {
            Order = order;
            Type = ConstantManager.Instance.BivouacTypes.First();
            Quantity = 0;
            Price = 0;
            Euro = 0;
            UnitaryPrice = 0;
            Comments = string.Empty;
            Coordinates = string.Empty;

            EndInit();
        }

        public Spending(Spending other)
        {
            Order = other.Order;
            Type = other.Type;
            Quantity = other.Quantity;
            Price = other.Price;
            Euro = other.Euro;
            UnitaryPrice = other.UnitaryPrice;
            Comments = other.Comments;
            Coordinates = other.Coordinates;
        }

        public bool Equals(Spending other)
        {
            if (Type != other.Type) { return false; }
            if (Order != other.Order) { return false; }
            if (Quantity != other.Quantity) { return false; }
            if (Price != other.Price) { return false; }
            if (Euro != other.Euro) { return false; }
            if (UnitaryPrice != other.UnitaryPrice) { return false; }
            if (Comments != other.Comments) { return false; }
            if (Coordinates != other.Coordinates) { return false; }

            return true;
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            Initial = new Spending(this);
        }

        [BsonIgnore]
        public bool HasBeenChanged { get { return !this.Equals(Initial); } }
    }
}
