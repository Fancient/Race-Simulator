using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ParticipantPoints : IStorageConstraint
    {
        public string Name { get; set; }
        public int Points { get; set; }

        void IStorageConstraint.Add<T>(List<T> list)
        {
            foreach (var TParticipantPoints in list)
            {
                var participantPoints = TParticipantPoints as ParticipantPoints;
                if (participantPoints.Name == Name)
                {
                    participantPoints.Points += Points;
                    return;
                }
            }
            list.Add(this as T);
        }
    }
}
