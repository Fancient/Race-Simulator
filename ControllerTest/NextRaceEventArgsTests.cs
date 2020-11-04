using System.Collections.Generic;
using Controller;
using Model;
using NUnit.Framework;

namespace Controller.Test
{
    [TestFixture]
    public class NextRaceEventArgsTests
    {
        [Test]
        public void Race_Instantiation_ShouldBeSame()
        {
            // Arrange
            var nextRaceEventArgs = new NextRaceEventArgs() { Race = new Race(new Track("Test", new [] {
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.Straight
            }), new List<IParticipant>() )};

            // Act

            // Assert
            Assert.IsNotNull(nextRaceEventArgs);
            Assert.IsNotNull(nextRaceEventArgs.Race);
        }
    }
}
