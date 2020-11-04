using Model;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Model.Test
{
    [TestFixture]
    public class MainDataContextTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void OnDriversChanged_RunMethod_TrackNameShouldBeSameAsTrackFromParameters()
        {
            // Arrange
            var mainDataContext = new MainDataContext();
            object sender = null;
            var track = new Track("aa", null);
            DriversChangedEventArgs e = new DriversChangedEventArgs() { Track = track};

            // Act
            mainDataContext.OnDriversChanged(
                sender,
                e);

            // Assert
            Assert.AreEqual(track.Name, mainDataContext.TrackName);
        }

        [Test]
        public void TrackName_SetAndGet_ShouldEqual()
        {
            // Arrange
            var context = new MainDataContext();
            var expected = "Test";

            // Act
            context.TrackName = expected;
            var result = context.TrackName;

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
