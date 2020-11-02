using System.Collections.Generic;

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

        public string BestParticipant<T>(List<T> list) where T : class, IStorageConstraint
        {
            ParticipantPoints bestParticipant = null;
            foreach (var storageConstraint in list)
            {
                var currentParticipant = storageConstraint as ParticipantPoints;
                if (bestParticipant == null) bestParticipant = currentParticipant;
                if (currentParticipant.Points > bestParticipant.Points)
                {
                    bestParticipant = currentParticipant;
                }
            }

            return bestParticipant.Name;
        }

        public override string ToString()
        {
            return $"{Name}: {Points}";
        }
    }
}