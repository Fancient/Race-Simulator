using NSubstitute;
using NUnit.Framework;
using System;
using System.ComponentModel;
using Controller;
using WpfEdition;

namespace WpfEdition.Test
{
    [TestFixture]
    public class RaceStatisticsDataContextTests
    {
        private RaceStatisticsDataContext _raceStatisticsDataContext;

        [SetUp]
        public void SetUp()
        {
            _raceStatisticsDataContext = new RaceStatisticsDataContext();
        }

        [Test]
        public void Class_ImplementsINotifyPropertyChanged()
        {
            // Assert
            Assert.IsInstanceOf<INotifyPropertyChanged>(_raceStatisticsDataContext);
        }

        [Test]
        public void CurrentRace_SetAndGet_AreEqual()
        {
            // Arrange
            Data.Initialize();
            var race = new Race(Data.Competition.NextTrack(), Data.Competition.Participants);

            // Act
            _raceStatisticsDataContext.CurrentRace = race;
            var result = _raceStatisticsDataContext.CurrentRace;

            // Assert
            Assert.AreEqual(race, result);
        }

        [Test]
        public void LapTimes_AfterConstructor_NotNull()
        {
            Assert.IsNotNull(_raceStatisticsDataContext.LapTimes);
        }

        [Test]
        public void SectionTimes_AfterConstructor_NotNull()
        {
            Assert.IsNotNull(_raceStatisticsDataContext.SectionTimes);
        }

        [Test]
        public void Participants_SetAndGet_AreEqual()
        {
            // Arrange
            Data.Initialize();
            var expected = Data.Competition.Participants;

            // Act
            _raceStatisticsDataContext.Participants = expected;
            var actual = _raceStatisticsDataContext.Participants;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BestSectionTime_SetAndGet_AreEqual()
        {
            // Arrange
            var expected = "Test";

            // Act
            _raceStatisticsDataContext.BestSectionTime = expected;
            var actual = _raceStatisticsDataContext.BestSectionTime;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BestLapTime_SetAndGet_AreEqual()
        {
            // Arrange
            var expected = "Test";

            // Act
            _raceStatisticsDataContext.BestLapTime = expected;
            var actual = _raceStatisticsDataContext.BestLapTime;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
