using Controller;
using Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WpfEdition
{
    public class RaceStatisticsDataContext : INotifyPropertyChanged
    {
        public Race CurrentRace { get; set; }
        private Storage<ParticipantSectionTime> _sectionTimeStorage;
        private Storage<ParticipantLapTime> _lapTimeStorage;
        public List<ParticipantLapTime> LapTimes { get; private set; }
        public List<ParticipantSectionTime> SectionTimes { get; private set; }
        public List<IParticipant> Participants { get; set; }
        public string BestSectionTime { get; set; }
        public string BestLapTime { get; set; }

        public RaceStatisticsDataContext()
        {
            LapTimes = new List<ParticipantLapTime>();
            SectionTimes = new List<ParticipantSectionTime>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNextRace(object sender, NextRaceEventArgs e)
        {
            CurrentRace = e.Race;
            _sectionTimeStorage = CurrentRace.ParticipantSectionTimeStorage;
            _lapTimeStorage = CurrentRace.LapTimeStorage;
            e.Race.DriversChanged += OnDriversChanged;
        }

        private void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            LapTimes = _lapTimeStorage.GetList().ToList();
            SectionTimes = _sectionTimeStorage.GetList().ToList();
            BestLapTime = _lapTimeStorage.BestParticipant();
            BestSectionTime = _sectionTimeStorage.BestParticipant();
            Participants = CurrentRace.Participants.ToList();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}