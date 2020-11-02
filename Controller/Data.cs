﻿using Model;
using System;

namespace Controller
{
    public static class Data
    {
        public static Competition CompetitionData { get; set; }

        public static Race CurrentRace { get; set; }

        public static event EventHandler<NextRaceEventArgs> NextRaceEvent;

        public static void Initialize()
        {
            CompetitionData = new Competition();
            addParticipants();
            addTracks();
        }

        public static void addParticipants()
        {
            CompetitionData.Participants.Add(new Driver("Jaap", 0, new Car(8, 10, 16, false), TeamColors.Red));
            CompetitionData.Participants.Add(new Driver("Sjaak", 0, new Car(12, 10, 20, false), TeamColors.Green));
            CompetitionData.Participants.Add(new Driver("Manus", 0, new Car(20, 10, 18, false), TeamColors.Blue));
            CompetitionData.Participants.Add(new Driver("Arie", 0, new Car(14, 10, 20, false), TeamColors.Orange));
            CompetitionData.Participants.Add(new Driver("Lars", 0, new Car(20, 10, 24, false), TeamColors.Yellow));
            CompetitionData.Participants.Add(new Driver("Elsa", 0, new Car(16, 10, 30, false), TeamColors.Pink));
        }

        public static void addTracks()
        {
            CompetitionData.Tracks.Enqueue(new Track("Rivendell", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner
            }));

            CompetitionData.Tracks.Enqueue(new Track("El Norte", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight
            }));

            CompetitionData.Tracks.Enqueue(new Track("The Oval", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner
            }));
        }

        public static void NextRace()
        {
            // cleanup previous race
            CurrentRace?.CleanUp();

            // get next track from competitionData, then perform null check. when not null create a new race.
            Track currentTrack = CompetitionData.NextTrack();
            if (currentTrack != null)
            {
                CurrentRace = new Race(currentTrack, CompetitionData.Participants);
                CurrentRace.RaceFinished += OnRaceFinished;
                NextRaceEvent?.Invoke(null, new NextRaceEventArgs() { Race = CurrentRace });
                CurrentRace.Start();
            }
        }

        public static void OnRaceFinished(object sender, EventArgs e)
        {
            CompetitionData.DeterminePoints(CurrentRace.GetFinishOrderParticipants());
            CompetitionData.StoreRaceLength(CurrentRace.Track.Name, CurrentRace.GetRaceLength());
            CompetitionData.StoreParticipantsSpeed(CurrentRace.GetParticipantSpeeds());
            NextRace();
        }
    }
}