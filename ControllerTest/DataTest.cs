using NUnit.Framework;
using Controller;

namespace ControllerTest
{
    [TestFixture]
    public class DataTest
    {
        [SetUp]
        public void Setup()
        {
            Data.Initialize();
        }

        [Test]
        public void TestCompetitionNotNull()
        {
            Assert.IsNotNull(Data.CompetitionData, "Competition Property is Null.");
        }
    }
}