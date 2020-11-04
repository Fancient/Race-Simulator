using System;
using Model;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Controller.Test
{
    // Class race is really hard to test because it runs on a timer thread. Race finish states are quite difficult to test.
    [TestFixture]
    internal class RaceTests
    {
        private Race race;

        // create own set of participants and tracks
        private List<IParticipant> participants;

        private IEquipment equipment;
        private Track track;

        [SetUp]
        public void SetUp()
        {
            // setup track
            track = new Track("Ovaal", new[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner
            });

            participants = new List<IParticipant>();
            equipment = new Car(0, 0, 0, false);
            participants.Add(new Driver("a", 0, equipment, TeamColors.Blue));
            participants.Add(new Driver("b", 0, equipment, TeamColors.Blue));
            participants.Add(new Driver("c", 0, equipment, TeamColors.Blue));
            participants.Add(new Driver("d", 0, equipment, TeamColors.Blue));
            participants.Add(new Driver("e", 0, equipment, TeamColors.Blue));
            race = new Race(track, participants);
        }

        [Test]
        public void Race_Instance_ShouldNotNull()
        {
            Assert.IsNotNull(race);
        }

        [Test]
        public void Race_GetSectionData_ShouldReturnObject()
        {
            Section section = track.Sections.First?.Value;

            var result = race.GetSectionData(section);

            // compare sectiondata
            Assert.IsInstanceOf<SectionData>(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Race_RandomizeEquipment()
        {
            // we will check if equipment of a single participant is in bounds.
            // since all participants have the same equipment in setup
            // set values out of bounds first.
            participants[0].Equipment.Quality = 32;
            participants[0].Equipment.Performance = 32;

            race.RandomizeEquipment();
            var resultQuality = participants[0].Equipment.Quality;
            var resultPerformance = participants[0].Equipment.Performance;

            // Check if values are within bounds.
            Assert.GreaterOrEqual(resultQuality, 8);
            Assert.LessOrEqual(resultQuality, 20);
            Assert.GreaterOrEqual(resultPerformance, 5);
            Assert.LessOrEqual(resultPerformance, 15);
        }

        [Test]
        public void Race_GetStartGrids_ShouldNotReturnNull()
        {
            Assert.NotNull(race.GetStartGrids());
        }

        [Test]
        public void Race_GetStartGrids_ShouldReturnListContainingOnlyStartGrids()
        {
            // Arrange
            var startGrids = race.GetStartGrids();

            // Act
            var result = startGrids.Any(x => x.SectionType != SectionTypes.StartGrid);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Race_PlaceParticipant_ShouldPlaceParticipantOnSection()
        {
            Section section = new Section(SectionTypes.StartGrid);
            IParticipant participant = new Driver("a", 0, equipment, TeamColors.Blue);

            race.PlaceParticipant(participant, false, section);
            var result = race.GetSectionData(section).Left;

            Assert.AreEqual(participant, result);
        }

        [Test]
        public void Race_PlaceParticipantsOnStartGrid_ShouldPlaceParticipants()
        {
            // call method
            race.PlaceParticipantsOnStartGrid();

            // check if participants are placed, check first section
            var p0 = race.GetSectionData(race.GetStartGrids()[0]).Left;
            var p1 = race.GetSectionData(race.GetStartGrids()[0]).Right;

            Assert.AreEqual(participants[0], p0);
            Assert.AreEqual(participants[1], p1);
        }

        [TestCase(10, 5, 21)]
        [TestCase(20, 5, 23)]
        [TestCase(30, 5, 26)]
        [TestCase(10, 10, 23)]
        [TestCase(20, 10, 28)]
        [TestCase(30, 10, 33)]
        [TestCase(10, 15, 26)]
        [TestCase(20, 15, 33)]
        [TestCase(30, 15, 41)]
        public void GetSpeedFromParticipant_MultipleInputs_ReturnsValue(int speed, int performance, int expected)
        {
            // Arrange
            var participant = new Driver("Test", 0, new Car(0, performance, speed, false), TeamColors.Blue);

            // Act
            var result = race.GetSpeedFromParticipant(participant);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(false,false,3)]
        [TestCase(true,false, 2)]
        [TestCase(false,true, 1)]
        [TestCase(true, true, 0)]
        public void FreePlacesLeftOnSectionData_InputSectionData_ReturnExpected(bool left, bool right, int expected)
        {
            // Arrange
            var sd = new SectionData();
            if (left)
                sd.Left = Substitute.For<IParticipant>();
            if (right)
                sd.Right = Substitute.For<IParticipant>();

            // Act
            var result = race.FreePlacesLeftOnSectionData(sd);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLapsParticipant_ParticipantInRace_ReturnsDrivenLaps()
        {
            // Arrange

            // Act
            race.UpdateLap(race.Participants[0], DateTime.Now); // update lap of first driver
            var result = race.GetLapsParticipant(race.Participants[0]);

            // Arrange
            Assert.AreEqual(0, result); // expect 0, race starts with lapsdriven on -1.
        }

        [Test]
        public void IsFinished_ShouldReturnExpected()
        {
            // Arrange
            var laps = Race.Laps;
            race.InitializeParticipantTimeEachLap();
            
            // Act
            //update lap amount of laps +1, considering lapsdriven starts at -1.
            for(int i = 0; i <= Race.Laps; i++)
                race.UpdateLap(race.Participants[0], DateTime.Now);
            var result = race.IsFinished(race.Participants[0]);

            // Assert
            Assert.True(result);
        }

        [TestCase(SectionTypes.Finish, true)]
        [TestCase(SectionTypes.Straight, false)]
        [TestCase(SectionTypes.LeftCorner, false)]
        [TestCase(SectionTypes.RightCorner, false)]
        [TestCase(SectionTypes.StartGrid, false)]
        public void IsFinishSection_ShouldReturnExpected(SectionTypes type, bool expected)
        {
            // Arrange
            var section = new Section(type);

            // Act
            var result = race.IsFinishSection(section);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CheckRaceFinished_RaceNotStarted_ShouldReturnFalse()
        {
            // Arrange
            
            // Act
            var result = race.CheckRaceFinished();

            // Assert
            Assert.False(result);
        }

        [Test]
        public void CheckRaceFinished_RaceFinished_ShouldReturnTrue()
        {
            // Arrange
            race.Positions.Clear();

            // Act
            var result = race.CheckRaceFinished();

            // Assert
            Assert.True(result);
        }

        [Test]
        public void GetFinishOrderParticipants_RaceStarted_ReturnEmptyList()
        {
            // Arrange

            // Act
            var result = race.GetFinishOrderParticipants();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }
    }
}