using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ParticipantLapTime : IStorageConstraint
    {
        public string Name { get; set; }
        public int Lap { get; set; }
        public TimeSpan Time { get; set; }
        public void Add<T>(List<T> list) where T : class, IStorageConstraint
        {
            list.Add(this as T);
        }

        public string BestParticipant<T>(List<T> list) where T : class, IStorageConstraint
        {
            ParticipantLapTime bestTime = null;
            foreach (T storageConstraint in list)
            {
                var currentTime = storageConstraint as ParticipantLapTime;
                if (bestTime == null) bestTime = currentTime; // first
                if (currentTime.Time < bestTime.Time)
                    bestTime = currentTime;
            }

            return bestTime.Name;
        }
    }
}
