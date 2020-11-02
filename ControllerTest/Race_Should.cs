using Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Controller.Test
{
    [TestFixture]
    internal class Race_Should
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
    }
}