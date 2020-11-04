using System;
using Model;
using NSubstitute;
using NUnit.Framework;

namespace Model.Test
{
    [TestFixture]
    public class SectionDataTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Left_SetAndGet_ShouldReturnSame()
        {
            // Arrange
            var sectionData = new SectionData();
            var participant = Substitute.For<IParticipant>();

            // Act
            sectionData.Left = participant;
            var result = sectionData.Left;

            // Assert
            Assert.AreEqual(participant, result);
        }

        [Test]
        public void Right_SetAndGet_ShouldReturnSame()
        {
            // Arrange
            var sectionData = new SectionData();
            var participant = Substitute.For<IParticipant>();

            // Act
            sectionData.Right = participant;
            var result = sectionData.Right;

            // Assert
            Assert.AreEqual(participant, result);
        }

        [Test]
        public void DistanceLeft_SetAndGet_ShouldReturnSame()
        {
            // Arrange
            var sectionData = new SectionData();
            var distance = 80;

            // Act
            sectionData.DistanceLeft = distance;
            var result = sectionData.DistanceLeft;

            // Assert
            Assert.AreEqual(distance, result);
        }

        [Test]
        public void DistanceRight_SetAndGet_ShouldReturnSame()
        {
            // Arrange
            var sectionData = new SectionData();
            var distance = 80;

            // Act
            sectionData.DistanceRight = distance;
            var result = sectionData.DistanceRight;

            // Assert
            Assert.AreEqual(distance, result);
        }

        [Test]
        public void StartTimeLeft_SetAndGet_ShouldReturnSame()
        {
            // Arrange
            var sectionData = new SectionData();
            var startTime = DateTime.Now;

            // Act
            sectionData.StartTimeLeft = startTime;
            var result = sectionData.StartTimeLeft;

            // Assert
            Assert.AreEqual(startTime, result);
        }

        [Test]
        public void StartTimeRight_SetAndGet_ShouldReturnSame()
        {
            // Arrange
            var sectionData = new SectionData();
            var startTime = DateTime.Now;

            // Act
            sectionData.StartTimeRight = startTime;
            var result = sectionData.StartTimeRight;

            // Assert
            Assert.AreEqual(startTime, result);
        }
    }
}
