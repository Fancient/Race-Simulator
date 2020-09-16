using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ModelTest
{
    [TestFixture]
    class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();

            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track track = new Track("Track", new SectionTypes[]{});
            _competition.Tracks.Enqueue(track);
            var result = _competition.NextTrack();

            Assert.AreEqual(track, result);
        }
    }
}
