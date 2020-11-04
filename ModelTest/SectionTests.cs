using Model;
using NSubstitute;
using NUnit.Framework;

namespace Model.Test
{
    [TestFixture]
    public class SectionTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void ToString_NormalInstance_ReturnsSectionTypeName()
        {
            // Arrange
            var section = new Section(SectionTypes.Straight);

            // Act
            var result = section.ToString();

            // Assert
            Assert.AreEqual(SectionTypes.Straight.ToString(), result);
        }
    }
}
