using System;
using System.Collections.Generic;
using System.Linq;

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
            foreach (var currentTime in list.Select(storageConstraint => storageConstraint as ParticipantLapTime))
            {
                bestTime ??= currentTime;
                if (currentTime.Time < bestTime.Time)
                    bestTime = currentTime;
            }

            return bestTime.Name;
        }
    }
}