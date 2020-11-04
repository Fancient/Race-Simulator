using Model;
using NSubstitute;
using NUnit.Framework;

namespace Model.Test
{
    [TestFixture]
    public class DriverTests
    {

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestMethod1()
        {
            // Arrange
            var name = "Driver";
            var points = 0;
            var subEquipment = Substitute.For<IEquipment>();
            var teamColor = TeamColors.Blue;

            // Act
            var driver = new Driver(name,points,subEquipment,teamColor);


            // Assert
            Assert.AreEqual(name, driver.Name);
            Assert.AreEqual(points, driver.Points);
            Assert.AreEqual(subEquipment, driver.Equipment);
            Assert.AreEqual(teamColor, driver.TeamColor);
        }
    }
}
