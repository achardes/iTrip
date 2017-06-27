//using Microsoft.Maps.MapControl.WPF;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System;
using Eto.Forms;
using System.Runtime.CompilerServices;

namespace iTrip 
{
    [BsonIgnoreExtraElements]
    public class Journey : INotifyPropertyChanged, ISupportInitialize, IEquatable<Journey>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [BsonIgnore]
        private Journey Initial { get; set; }

        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public int Duration { get { return (ToDateTime.Date - FromDateTime.Date).Days + 1; } }
        public string Weather { get; set; }
        public string Note { get; set; }
        public Bivouac Bivouac { get; set; }
        public bool IncludeBorderCrossing { get; set; }
        public BorderCrossing BorderCrossing { get; set; }
        public ItemObservableCollection<Event> Events { get; set; }
        public ItemObservableCollection<Spending> Spendings { get; set; }

        public Journey(DateTime fromDateTime, DateTime toDateTime)
        {
            FromDateTime = fromDateTime.Date;
            ToDateTime = toDateTime.Date;
            Weather = ConstantManager.Instance.WeatherKinds.First();
            Note = "1";

            Bivouac = new Bivouac();
            BorderCrossing = new BorderCrossing();
            Events = new ItemObservableCollection<Event>();
            Spendings = new ItemObservableCollection<Spending>();

            EndInit();
        }

        public Journey(Journey other)
        {
            FromDateTime = other.FromDateTime;
            ToDateTime = other.ToDateTime;
            Weather = other.Weather;
            Note = other.Note;
            Bivouac = new Bivouac(other.Bivouac);
            BorderCrossing = new BorderCrossing(other.BorderCrossing);
            IncludeBorderCrossing = other.IncludeBorderCrossing;

            Events = other.Events.Duplicate();
            Spendings = other.Spendings.Duplicate();
        }

        public bool Equals(Journey other)
        {
            if (FromDateTime != other.FromDateTime) { return false; }
            if (ToDateTime != other.ToDateTime) { return false; }
            if (Weather != other.Weather) { return false; }
            if (Note != other.Note) { return false; }
            if (IncludeBorderCrossing != other.IncludeBorderCrossing) { return false; }
            if (Bivouac.HasBeenChanged) { return false; }

            if (BorderCrossing.HasBeenChanged) { return false; }

            if (Events.Count != other.Events.Count) { return false; }
            if (Spendings.Count != other.Spendings.Count) { return false; }

            if (Events.ToList().Exists(x => x.HasBeenChanged)) { return false; }
            if (Spendings.ToList().Exists(x => x.HasBeenChanged)) { return false; }

            return true;
        }

        [BsonIgnore]
        public bool HasBeenChanged { get { return !this.Equals(Initial); } }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        public string DisplayName
        {
            get
            {
                if (Duration > 1) { return "Journey from " + FromDateTime.ToShortDateString() + " to " + ToDateTime.ToShortDateString(); }
                return "Journey of " + FromDateTime.ToShortDateString();
            }
        }

        public string ShortDisplayName
        {
            get { return (HasBeenChanged? "*" : "") + ToDateTime.ToString("dd/MM/yy") + " (" + Duration + " day" + ((Duration > 1) ? "s" : "") + ")"; }
        }

        public async void Save()
        {
            IMongoDatabase database = DataContext.GetMongoDatabase(DataContext.DatabaseName);
            IMongoCollection<Journey> collection = database.GetCollection<Journey>("journeys");

            if (Id == ObjectId.Empty) { await collection.InsertOneAsync(this); }
            else { await collection.ReplaceOneAsync<Journey>(x => x.Id == Id, this); }

            Initial = new Journey(this);
            Bivouac.EndInit();
            BorderCrossing.EndInit();
            Events.ToList().ForEach(x => x.EndInit());
            Spendings.ToList().ForEach(x => x.EndInit());

            OnPropertyChanged("HasBeenChanged");
        }

        public void Delete()
        {
            IMongoDatabase database = DataContext.GetMongoDatabase(DataContext.DatabaseName);

            IMongoCollection<Journey> collection = database.GetCollection<Journey>("journeys");
            collection.DeleteOneAsync(x => x.Id == Id);
        }

        public void BeginInit() { }

        public void EndInit()
        {
            Events.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(HasBeenChanged));
            Events.ItemPropertyChanged += (sender, e) => OnPropertyChanged(nameof(HasBeenChanged));
            Spendings.ItemPropertyChanged += (sender, e) => OnPropertyChanged(nameof(HasBeenChanged));
            Bivouac.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(HasBeenChanged));
            Bivouac.Tags.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(HasBeenChanged));
            BorderCrossing.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(HasBeenChanged));
            Initial = new Journey(this);
        }
    }
}
