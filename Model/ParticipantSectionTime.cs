using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class ParticipantSectionTime : IStorageConstraint
    {
        public string Name { get; set; }
        public Section Section { get; set; }
        public TimeSpan Time { get; set; }

        public void Add<T>(List<T> list) where T : class, IStorageConstraint
        {
            foreach (var pstElement in list)
            {
                ParticipantSectionTime pst = pstElement as ParticipantSectionTime;
                if (pst.Name == Name && pst.Section == Section)
                {
                    pst.Time = Time;
                    return;
                }
            }
            list.Add(this as T);
        }

        public string BestParticipant<T>(List<T> list) where T : class, IStorageConstraint
        {
            ParticipantSectionTime bestTime = null;
            foreach (var currentTime in list.Select(storageConstraint => storageConstraint as ParticipantSectionTime))
            {
                bestTime ??= currentTime;
                if (currentTime.Time < bestTime.Time)
                    bestTime = currentTime;
            }

            return bestTime.Name;
        }
    }
}