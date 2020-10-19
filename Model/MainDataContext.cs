using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model
{
    public class MainDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainDataContext(EventHandler<DriversChangedEventArgs> DriversChangedEventHandler)
        {
            DriversChangedEventHandler += OnDriversChanged;
        }

        public MainDataContext()
        {

        }

        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
