using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadTripManager
{
    public class Chart<T> : AObservableObject
    {
        private ChartPoint<T> _selected;
        public ChartPoint<T> Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                NotifyPropertyChanged(nameof(Selected));
            }
        }

        private ObservableCollection<ChartPoint<T>> _points;
        public ObservableCollection<ChartPoint<T>> Points
        {
            get { return _points; }
            set { _points = value; OnlyNotifyPropertyChanged(nameof(Points)); }
        }

        public Chart()
        {
            Points = new ObservableCollection<ChartPoint<T>>();
        }
    }
}
