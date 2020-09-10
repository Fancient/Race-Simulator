using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        // TODO: Constructor?

        public Track NextTrack()
        {
            // TODO: Implement NextTrack()
            return null;
        }
    }
}
