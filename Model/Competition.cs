using System;
using System.Collections.Generic;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }
        public Storage<ParticipantPoints> PointsStorage { get; }
        public Storage<ParticipantSpeed> SpeedStorage { get; }

        public List<RaceLength> RaceLengthStorage { get; }

        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
            PointsStorage = new Storage<ParticipantPoints>();
            RaceLengthStorage = new List<RaceLength>();
            SpeedStorage = new Storage<ParticipantSpeed>();
        }

        public Track NextTrack()
        {
            // check if no tracks left, return null. otherwise return next track in queue
            return Tracks.Count > 0 ? Tracks.Dequeue() : null;
        }

        public void DeterminePoints(List<IParticipant> finishOrder)
        {
            int[] points = { 15, 10, 8, 6, 4, 2, 1, 0 };
            for (int i = 0; i < finishOrder.Count; i++)
            {
                int pointIndex = i;
                if (i >= finishOrder.Count)
                    pointIndex = finishOrder.Count - 1; // index out of bounds afhandelen.

                PointsStorage.AddToList(new ParticipantPoints() { Name = finishOrder[i].Name, Points = points[pointIndex] });
            }
        }

        public void StoreRaceLength(string name, TimeSpan time)
        {
            RaceLengthStorage.Add(new RaceLength() { TrackName = name, Time = time });
        }

        public void StoreParticipantsSpeed(List<ParticipantSpeed> psl)
        {
            foreach (ParticipantSpeed ps in psl)
            {
                SpeedStorage.AddToList(ps);
            }
        }
    }
}