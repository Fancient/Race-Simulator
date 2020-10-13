using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using Model;
using NSubstitute;
using NUnit.Framework;

namespace Model.Test
{
    [TestFixture]
    class Model_Competition_Storage
    {
        [Test]
        public void DeterminePoints_FinishOrder_ShouldDeterminePointsCorrectly()
        {
            // Arrange
            var competition = new Competition();
            var participants = new List<IParticipant>();
            participants.Add(Substitute.For<IParticipant>());
            participants[0].Name = "a";
            participants.Add(Substitute.For<IParticipant>());
            participants[1].Name = "b";
            participants.Add(Substitute.For<IParticipant>());
            participants[2].Name = "c";

            // Act
            competition.DeterminePoints(participants);

            // Assert
            Assert.That(competition.PointsStorage.GetList()[0].Points == 15 && competition.PointsStorage.GetList()[0].Name == "a");
            Assert.That(competition.PointsStorage.GetList()[1].Points == 10 && competition.PointsStorage.GetList()[1].Name == "b");
            Assert.That(competition.PointsStorage.GetList()[2].Points == 8  && competition.PointsStorage.GetList()[2].Name == "c");
        }

        [Test]
        public void StoreRaceLength_RaceLength_ShouldStoreRaceLength()
        {
            // Arrange
            var competition = new Competition();
            var raceLength = new RaceLength() { Time = TimeSpan.FromSeconds(95), TrackName = "Track"};
            
            // Act
            competition.StoreRaceLength(raceLength.TrackName, raceLength.Time);

            var result = competition.RaceLengthStorage[0];

            // Assert
            Assert.AreEqual(raceLength.Time, result.Time);
            Assert.AreEqual(raceLength.TrackName, result.TrackName);
        }

        [Test]
        public void StoreParticipantsSpeed_Speeds_ShouldStore()
        {
            // Arrange
            var competition = new Competition();
            var speeds = new List<ParticipantSpeed>();
            speeds.Add(new ParticipantSpeed() { Name = "a", Speed = 5, TrackName = "a" });
            speeds.Add(new ParticipantSpeed() { Name = "b", Speed = 6, TrackName = "a" });
            speeds.Add(new ParticipantSpeed() { Name = "c", Speed = 7, TrackName = "a" });

            // Act
            competition.StoreParticipantsSpeed(speeds);

            var result = competition.SpeedStorage.GetList();

            // Assert
            Assert.AreEqual(speeds, result);
        }
    }
}
