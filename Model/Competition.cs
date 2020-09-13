using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        // TODO: Constructor?
        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }

        public Track NextTrack()
        {
            // TODO: Implement NextTrack()
            return null;
        }
    }
}
