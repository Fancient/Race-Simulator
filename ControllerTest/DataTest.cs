using NUnit.Framework;

namespace Controller.Test
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