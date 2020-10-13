using NUnit.Framework;

namespace Model.Test
{
    [TestFixture]
    internal class Model_Section_Should
    {
        [Test]
        public void Section_Object_NotNull()
        {
            Section section = new Section(SectionTypes.Straight);

            Assert.NotNull(section);
        }

        [Test]
        public void Section_SectionType_ShouldReturnEqual()
        {
            Section section = new Section(SectionTypes.Straight);

            var actual = section.SectionType;

            Assert.AreEqual(SectionTypes.Straight, actual);
        }

        [Test]
        public void Section_SectionType_SetSection_ShouldReturnEqual()
        {
            Section section = new Section(SectionTypes.Straight);

            section.SectionType = SectionTypes.Finish;
            var actual = section.SectionType;

            Assert.AreEqual(SectionTypes.Finish, actual);
        }
    }
}