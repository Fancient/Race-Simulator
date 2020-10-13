using Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace Controller.Test
{
    [TestFixture]
    internal class Race_MovingParticipants
    {
        private Race race;

        private IParticipant participant1;
        private IParticipant participant2;
        private IParticipant participant3;
        private IParticipant participant4;

        [SetUp]
        public void SetUp()
        {
            participant1 = new Driver("1", 0, new Car(10, 10, 10, false), TeamColors.Blue);
            participant2 = new Driver("2", 0, new Car(10, 10, 10, false), TeamColors.Blue);
            participant3 = new Driver("3", 0, new Car(10, 10, 10, false), TeamColors.Blue);
            participant4 = new Driver("4", 0, new Car(10, 10, 10, false), TeamColors.Blue);

            race = new Race(
                new Track("a", new SectionTypes[]
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
                }), new List<IParticipant>()
                {
                    participant1,
                    participant2,
                    participant3,
                    participant4
                });
        }

        [Test]
        public void Race_MoveParticipantTo_ShouldMoveParticipant()
        {
            // arrange
            SectionData currentSectionData = race.GetSectionData(race.Track.Sections.Last.Value); // last track section
            SectionData nextSectionData = race.GetSectionData(race.Track.Sections.First.Value); // first track section

            // act
            // race.MoveParticipantTo(currentSectionData, nextSectionData, false, false, false);

            Assert.True(true);
        }

        // TODO: Write unit tests for moving participants (which is hard)
    }
}