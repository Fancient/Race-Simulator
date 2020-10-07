using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }
        public Storage<ParticipantPoints> PointsStorage { get; set; }
        
        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
            PointsStorage = new Storage<ParticipantPoints>();
        }

        public Track NextTrack()
        {
            // check if no tracks left, return null. otherwise return next track in queue
            return Tracks.Count > 0 ? Tracks.Dequeue() : null;
        }

        public void DeterminePoints(List<IParticipant> finishOrder)
        {
            int[] points = {15, 10, 8, 6, 4, 2 ,1 ,0};
            for (int i = 0; i < finishOrder.Count; i++)
            {
                int pointIndex = i;
                if (i >= finishOrder.Count)
                    pointIndex = finishOrder.Count - 1; // index out of bounds afhandelen.

                PointsStorage.AddToList(new ParticipantPoints(){Name = finishOrder[i].Name, Points = points[pointIndex]});
            }
        }
    }
}