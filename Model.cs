using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace iTrip
{
    public class Model : INotifyPropertyChanged
    {
        public Model()
        {
            myList = new List<string>();
            myList.Add("plop");
            myList.Add("plo1");
            myList.Add("plop2");
            myList.Add("plop3");
        }

        string myString;
        public string MyString
        {
            get { return myString; }
            set
            {
                if (myString != value)
                {
                    myString = value;
                    OnPropertyChanged();
                }
            }
        }

        List<string> myList;
        public List<string> MyList
        {
            get { return myList; }
            set
            {
                if (myList != value)
                {
                    myList = value;
                    OnPropertyChanged();
                }
            }
        }

        void OnPropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
