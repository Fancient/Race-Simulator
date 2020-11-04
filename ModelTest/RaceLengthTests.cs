using System;
using Model;
using NSubstitute;
using NUnit.Framework;

namespace Model.Test
{
    [TestFixture]
    public class RaceLengthTests
    {
        [SetUp]
        public void SetUp()
        {

        }


        [Test]
        public void RaceLength_Instantiation_ShouldReturnRaceLengthObject()
        {
            // Arrange

            // Act
            var result = new RaceLength();


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(RaceLength), result);
        }

        [Test]
        public void RaceLength_TrackName_ShouldReturnName()
        {
            // Arrange
            var raceLength = new RaceLength();
            var name = "Track";

            // Act
            raceLength.TrackName = name; // set
            var result = raceLength.TrackName; // get

            // Assert
            Assert.AreEqual(name, result);
        }

        [Test]
        public void RaceLength_Time_ShouldReturnTime()
        {
            // Arrange
            var raceLength = new RaceLength();
            var time = TimeSpan.FromSeconds(96);

            // Act
            raceLength.Time = time; // set
            var result = raceLength.Time; // get

            // Assert
            Assert.AreEqual(time, result);
        }
    }
}
