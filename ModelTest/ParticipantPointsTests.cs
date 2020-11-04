using Model;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Model.Test
{
    [TestFixture]
    public class ParticipantPointsTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Add_MultiplePointsSingleParticipant_ShouldSum()
        {
            // Arrange
            var points1 = new ParticipantPoints() { Name = "Test", Points = 8 };
            var points2 = new ParticipantPoints() { Name = "Test", Points = 4 };
            var expected = points1.Points + points2.Points;
            var list = new List<ParticipantPoints>();

            // Act
            points1.Add(list);
            points2.Add(list);
            var result = list[0].Points;

            // Assert 
            Assert.True(list.Count == 1);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void BestParticipant_2ParticipantsInList_ShouldReturnNameOfBestParticipant()
        {
            // Arrange
            var points1 = new ParticipantPoints() { Name = "Test1", Points = 8 };
            var points2 = new ParticipantPoints() { Name = "Test2", Points = 4 };
            var points3 = new ParticipantPoints() { Name = "Test1", Points = 7 };
            var points4 = new ParticipantPoints() { Name = "Test2", Points = 6 };
            var list = new List<ParticipantPoints>();

            // Act
            points1.Add(list);
            points2.Add(list);
            points3.Add(list);
            points4.Add(list);
            var result = points1.BestParticipant(list);

            // Assert
            Assert.AreEqual("Test1", result);
        }

        [Test]
        public void ToString_InputSingleParticipant_ShouldReturnProperString()
        {
            // Arrange
            var participantPoints = new ParticipantPoints() { Name = "Test", Points = 12 };

            // Act
            var result = participantPoints.ToString();

            // Assert
            Assert.AreEqual("Test: 12", result);
        }
    }
}
