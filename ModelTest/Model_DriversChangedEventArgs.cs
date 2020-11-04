using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Model.Test
{
    [TestFixture]
    class Model_DriversChangedEventArgs
    {
        private Track track;

        [SetUp]
        public void SetUp()
        {
            // dummy track
            track = new Track("track", new [] {SectionTypes.Finish, SectionTypes.Straight});
        }

        [Test]
        public void DriversChangedEventArgs_Instantiation_ShouldResultInObject()
        {
            // Arrange

            // Act
            var result = new DriversChangedEventArgs();

            // Assert
            Assert.IsInstanceOf(typeof(DriversChangedEventArgs), result);
        }

        [Test]
        public void DriversChangedEventArgs_Instantiation_IsTypeOfEventArgs()
        {
            // Arrange

            // Act
            var result = new DriversChangedEventArgs();

            // Assert
            Assert.IsInstanceOf(typeof(EventArgs), result);
        }

        [Test]
        public void DriversChangedEventArgs_Track_ReturnsTrack()
        {
            // Arrange
            var driversChangedEventArgs = new DriversChangedEventArgs();
            driversChangedEventArgs.Track = track;

            // Act
            var result = driversChangedEventArgs.Track;

            // Assert
            Assert.AreEqual(track, result);
        }
    }
}
