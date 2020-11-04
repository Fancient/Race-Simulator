using Model;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Model.Test
{
    [TestFixture]
    public class ParticipantSpeedTests
    {


        [SetUp]
        public void SetUp()
        {

        }


        [Test]
        public void Add_MultipleSpeeds_ShouldAddAll()
        {
            // Arrange
            var participantSpeed1 = new ParticipantSpeed() { Name = "Test1", Speed = 32, TrackName = "a"};
            var participantSpeed2 = new ParticipantSpeed() { Name = "Test2", Speed = 30, TrackName = "a" };
            var participantSpeed3 = new ParticipantSpeed() { Name = "Test1", Speed = 34, TrackName = "b" };
            var participantSpeed4 = new ParticipantSpeed() { Name = "Test2", Speed = 31, TrackName = "b" };
            var list = new List<ParticipantSpeed>();

            // Act
            participantSpeed1.Add(list);
            participantSpeed2.Add(list);
            participantSpeed3.Add(list);
            participantSpeed4.Add(list);

            // Assert
            Assert.True(list.Count == 4);
        }

        [Test]
        public void BestParticipant_Input4Speeds_ShouldReturnHighestSpeed()
        {
            // Arrange
            var participantSpeed1 = new ParticipantSpeed() { Name = "Test1", Speed = 32, TrackName = "a" };
            var participantSpeed2 = new ParticipantSpeed() { Name = "Test2", Speed = 30, TrackName = "a" };
            var participantSpeed3 = new ParticipantSpeed() { Name = "Test1", Speed = 34, TrackName = "b" }; // highest speed
            var participantSpeed4 = new ParticipantSpeed() { Name = "Test2", Speed = 31, TrackName = "b" };
            var list = new List<ParticipantSpeed>();

            // Act
            participantSpeed1.Add(list);
            participantSpeed2.Add(list);
            participantSpeed3.Add(list);
            participantSpeed4.Add(list);
            var result = list[0].BestParticipant(list);

            // Assert
            Assert.AreEqual("Test1", result);
        }

        [Test]
        public void ToString_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var participantSpeed = new ParticipantSpeed() { Name = "Test", Speed = 32, TrackName = "track" };

            // Act
            var result = participantSpeed.ToString();

            // Assert $"{Name}, {Speed} on {TrackName}"
            Assert.AreEqual("Test, 32 on track", result);
        }
    }
}
