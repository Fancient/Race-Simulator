using Model;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Model.Test
{
    [TestFixture]
    public class ParticipantLapTimeTests
    {


        [SetUp]
        public void SetUp()
        {

        }


        [Test]
        public void Add_2LapTimes2Participants_ShouldAdd2Times()
        {
            // Arrange
            var participantLapTime1 = new ParticipantLapTime() { Lap = 1, Name = "nummer1", Time = TimeSpan.Zero };
            var participantLapTime2 = new ParticipantLapTime() { Lap = 1, Name = "nummer2", Time = TimeSpan.Zero };
            List<ParticipantLapTime> list = new List<ParticipantLapTime>();

            // Act
            participantLapTime1.Add(list);
            participantLapTime2.Add(list);

            // Assert
            Assert.AreEqual(participantLapTime1, list[0]);
            Assert.AreEqual(participantLapTime2, list[1]);
        }

        [Test]
        public void BestParticipant_2TimesInList_ShouldReturnFastestTime()
        {
            // Arrange
            var expected = "snelle";
            var participantLapTime1 = new ParticipantLapTime() { Lap = 2, Name = "slome", Time = TimeSpan.FromSeconds(12) };
            var participantLapTime2 = new ParticipantLapTime() { Lap = 1, Name = expected, Time = TimeSpan.FromSeconds(9) }; // fastest
            List<ParticipantLapTime> list = new List<ParticipantLapTime>();
            

            // Act
            participantLapTime1.Add(list);
            participantLapTime2.Add(list);
            var result = participantLapTime1.BestParticipant(list);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
