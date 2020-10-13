using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Model.Test
{
    [TestFixture]
    internal class Model_Track_Should
    {
        private string _name = "a name";
        private SectionTypes[] _sections;

        [SetUp]
        public void SetUp()
        {
            _sections = new SectionTypes[] { SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.RightCorner };
        }

        [Test]
        public void Track_Instance_ShouldReturnObject()
        {
            Track track = new Track(_name, _sections);

            Assert.IsNotNull(track);
        }

        [Test]
        public void Track_Constructor_ShouldSetName()
        {
            Track track = new Track(_name, _sections);

            Assert.AreEqual(_name, track.Name);
        }

        [Test]
        public void Track_GenerateSectionsShouldReturnLinkedList()
        {
            // GenerateSections will be called by constructor
            Track track = new Track(_name, _sections);

            LinkedList<Section> resultLinkedList = track.Sections;
            // convert LinkedList to array of sectionTypes to assert equal.
            SectionTypes[] resultSectionTypes = new SectionTypes[resultLinkedList.Count];
            for (int i = 0; i < resultLinkedList.Count; i++)
            {
                resultSectionTypes[i] = resultLinkedList.ElementAt(i).SectionType;
            }

            Assert.AreEqual(_sections, resultSectionTypes);
        }
    }
}