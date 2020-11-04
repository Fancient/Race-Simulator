using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Model;
using WpfEdition;

namespace WpfEdition.Test
{
    [TestFixture]
    public class CompetitionStatisticsDataContextTests
    {
        private CompetitionStatisticsDataContext _competitionStatisticsDataContext;

        [SetUp]
        public void SetUp()
        {
            _competitionStatisticsDataContext = new CompetitionStatisticsDataContext();
        }

        [Test]
        public void Class_ImplementsINotifyPropertyChanged()
        {
            // Assert
            Assert.IsInstanceOf<INotifyPropertyChanged>(_competitionStatisticsDataContext);
        }

        [Test]
        public void ParticipantPoints_SetAndGet_ReturnsNotNull()
        {
            // Arrange
            var list = new List<ParticipantPoints>();
            list.Add(new ParticipantPoints());
            list.Add(new ParticipantPoints());

            // Act
            _competitionStatisticsDataContext.ParticipantPoints = list;
            var result = _competitionStatisticsDataContext.ParticipantPoints;

            // Assert
            Assert.AreEqual(list, result);
        }

        [Test]
        public void ParticipantSpeeds_SetAndGet_ReturnsNotNull()
        {
            // Arrange
            var list = new List<ParticipantSpeed>();
            list.Add(new ParticipantSpeed());
            list.Add(new ParticipantSpeed());

            // Act
            _competitionStatisticsDataContext.ParticipantSpeeds = list;
            var result = _competitionStatisticsDataContext.ParticipantSpeeds;

            // Assert
            Assert.AreEqual(list, result);
        }

        [Test]
        public void RaceLengths_SetAndGet_ReturnsNotNull()
        {
            // Arrange
            var list = new List<RaceLength>();
            list.Add(new RaceLength());
            list.Add(new RaceLength());

            // Act
            _competitionStatisticsDataContext.RaceLengths = list;
            var result = _competitionStatisticsDataContext.RaceLengths;

            // Assert
            Assert.AreEqual(list, result);
        }

        [Test]
        public void BestParticipantPoints_SetAndGet_ReturnsNotNull()
        {
            // Arrange
            var bestParticipantPoints = new ParticipantPoints();

            // Act
            _competitionStatisticsDataContext.BestParticipantPoints = bestParticipantPoints;
            var result = _competitionStatisticsDataContext.BestParticipantPoints;

            // Assert
            Assert.AreEqual(bestParticipantPoints, result);
        }

        [Test]
        public void BestParticipantSpeed_SetAndGet_ReturnsNotNull()
        {
            // Arrange
            var bestParticipantSpeed = new ParticipantSpeed();

            // Act
            _competitionStatisticsDataContext.BestParticipantSpeed = bestParticipantSpeed;
            var result = _competitionStatisticsDataContext.BestParticipantSpeed;

            // Assert
            Assert.AreEqual(bestParticipantSpeed, result);
        }
    }
}
