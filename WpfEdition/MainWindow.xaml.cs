﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using Model;
using DispatcherPriority = System.Windows.Threading.DispatcherPriority;

namespace WpfEdition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Start of application
            ImageCache.Initialize();
            Data.Initialize();
            Data.NextRaceEventHandler += OnNextRaceEvent; // tell data about visualization event.
            Data.NextRace(); // start first race
        }

        private void OnNextRaceEvent(object sender, RaceStartEventArgs e)
        {
            // reinitialize
            ImageCache.ClearCache();
            Visualization.Initialize(e.Race);

            // link event
            e.Race.DriversChanged += OnDriversChanged;
        }

        private void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            this.TrackScreen.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.TrackScreen.Source = null;
                    this.TrackScreen.Source = Visualization.DrawTrack(e.Track);
                    Trace.WriteLine($"Track ({e.Track.Name}) drawn");
                }));
        }
    }
}
