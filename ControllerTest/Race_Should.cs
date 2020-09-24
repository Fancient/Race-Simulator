using System.Collections.Generic;
using System.Linq;
using Model;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Controller.Test
{
    [TestFixture]
    class Race_Should
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
            track = new Track("Ovaal", new SectionTypes[]
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
            equipment = new Car(0,0,0,false);
            participants.Add(new Driver("a",0, equipment, TeamColors.Blue));
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
            Section section = track.Sections.First.Value;

            var result = race.GetSectionData(section);

            // compare sectiondata
            Assert.IsInstanceOf<SectionData>(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Race_RandomizeEquipment()
        {
            // we will check if equipment of a single participant has changed,
            // since all participants have the same equipment in setup
            
            // get baseline equipment values
            int originalQuality = equipment.Quality;
            int originalPerformance = equipment.Performance;

            race.RandomizeEquipment();

            var resultQuality = participants[0].Equipment.Quality;
            var resultPerformance = participants[0].Equipment.Performance;

            // this Assert can be quite dangerous. the RandomizeEquipment() method creates random
            // values and has a small possibility to generate the same numbers as the original equipment.
            // meaning the unit test could fail for actually expected behaviour.
            // TODO: Find a better way to test Race.RandomizeEquipment()
            Assert.AreNotEqual(originalQuality, resultQuality);
            Assert.AreNotEqual(originalPerformance, resultPerformance);
        }

        [Test]
        public void Race_GetStartGrids_ShouldNotReturnNull()
        {
            Assert.NotNull(race.GetStartGrids());
        }

        [Test]
        public void Race_GetStartGrids_ShouldReturnListContainingOnlyStartGrids()
        {
            var result = race.GetStartGrids();

            // there must be a better way to do this
            // TODO: Find a better way to assert
            bool containingOtherSectionType = false;
            foreach (Section section in result)
            {
                if (section.SectionType != SectionTypes.StartGrid)
                    containingOtherSectionType = true;
            }

            Assert.IsFalse(containingOtherSectionType);
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

        // TODO: Maybe add test to check what happens with more participants than available start grids
    }
}
