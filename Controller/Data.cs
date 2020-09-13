using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition CompetitionData { get; set; }

        public static void Initialize()
        {
            CompetitionData = new Competition();
            addParticipants();
        }

        public static void addParticipants()
        {
            CompetitionData.Participants.Add(new Driver("Jaap", 0, new Car(10, 10, 10, false), TeamColors.Red));
            CompetitionData.Participants.Add(new Driver("Sjaak", 0, new Car(10, 10, 10, false), TeamColors.Green));
            CompetitionData.Participants.Add(new Driver("Manus", 0, new Car(10, 10, 10, false), TeamColors.Blue));
        }
    }
}
