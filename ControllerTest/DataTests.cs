using Controller;
using Model;
using NUnit.Framework;

namespace Controller.Test
{
    [TestFixture]
    public class DataTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Competition_Get_IsNotNull()
        {
            // Arrange
            Data.Initialize();

            // Act
            var result = Data.Competition;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Initialize_Initialization_CompetitionShouldBeFilled()
        {
            // Arrange

            // Act
            Data.Initialize();

            // Assert
            Assert.True(Data.Competition.Participants.Count > 0);
            Assert.True(Data.Competition.Tracks.Count > 0);
        }

        [Test]
        public void NextRace_NoCurrentRace_ShouldStart()
        {
            // Arrange
            Data.Initialize();

            // Act
            Data.NextRace();

            // Assert
            Assert.NotNull(Data.CurrentRace);
        }
    }
}
