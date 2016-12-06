using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoadTripManager
{
    public class JourneyFilter : AObservableObject
    {
        private DateTime? _startDateTime;
        public DateTime? StartDateTime
        {
            get { return _startDateTime; }
            set { _startDateTime = value; OnlyNotifyPropertyChanged(nameof(StartDateTime)); }
        }

        private DateTime? _endDateTime;
        public DateTime? EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; OnlyNotifyPropertyChanged(nameof(EndDateTime)); }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; OnlyNotifyPropertyChanged(nameof(Country)); }
        }

        private bool _isFilterOpen;
        public bool IsFilterOpen
        {
            get { return _isFilterOpen; }
            set { _isFilterOpen = value; OnlyNotifyPropertyChanged(nameof(IsFilterOpen)); }
        }

        RelayCommand _openFilterCommand;
        public ICommand OpenFilterCommand
        {
            get
            {
                if (_openFilterCommand == null)
                {
                    _openFilterCommand = new RelayCommand(param => OpenFilter(), param => true);
                }
                return _openFilterCommand;
            }
        }

        private void OpenFilter()
        {
            IsFilterOpen = !IsFilterOpen;
        }
    }
}
