using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Controller;
using Model;

namespace WpfEdition
{
    public class CompetitionStatisticsDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<ParticipantPoints> ParticipantPoints { get; set; }
        public List<ParticipantSpeed> ParticipantSpeeds { get; set; }

        public List<RaceLength> RaceLengths { get; set; }

        public ParticipantPoints BestParticipantPoints { get; set; }
        public ParticipantSpeed BestParticipantSpeed { get; set; }

        public void OnNextRace(object sender, NextRaceEventArgs e)
        {
            ParticipantPoints = Data.Competition.PointsStorage.GetList().OrderByDescending(x => x.Points).ToList();
            ParticipantSpeeds = Data.Competition.SpeedStorage.GetList().ToList();

            // determine best participants
            BestParticipantPoints = ParticipantPoints.FirstOrDefault();
            BestParticipantSpeed = ParticipantSpeeds.OrderByDescending(x => x.Speed).FirstOrDefault();

            RaceLengths = Data.Competition.RaceLengthStorage.ToList();

            OnPropertyChanged();
        }

        protected virtual void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
