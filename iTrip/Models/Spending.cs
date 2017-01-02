using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace iTrip
{
    public class Spending : INotifyPropertyChanged, IEquatable<Spending>, ISupportInitialize
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [BsonIgnore]
        private Spending Initial { get; set; }

        public string Type { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string Comments { get; set; }
 
        public Spending()
        {
            Type = ConstantManager.Instance.BivouacTypes.First();
            Quantity = 0;
            Price = 0;
            Comments = string.Empty;

            EndInit();
        }

        public Spending(Spending other)
        {
            Type = other.Type;
            Quantity = other.Quantity;
            Price = other.Price;
            Comments = other.Comments;
        }

        public bool Equals(Spending other)
        {
            if (Type != other.Type) { return false; }
            if (Quantity != other.Quantity) { return false; }
            if (Price != other.Price) { return false; }
            if (Comments != other.Comments) { return false; }

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
