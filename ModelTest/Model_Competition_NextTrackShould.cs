using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
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
            Track track = new Track("Track", new SectionTypes[] { });
            _competition.Tracks.Enqueue(track);
            var result = _competition.NextTrack();

            Assert.AreEqual(track, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track track = new Track("Track", new SectionTypes[] { });
            _competition.Tracks.Enqueue(track);

            var result = _competition.NextTrack();
            result = _competition.NextTrack();

            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            // this test will assert if the NextTrack method properly returns the next in queue
            // create tracks
            Track track1 = new Track("Track1", new SectionTypes[] { });
            Track track2 = new Track("Track2", new SectionTypes[] { });

            // enqueue tracks
            _competition.Tracks.Enqueue(track1);
            _competition.Tracks.Enqueue(track2);

            // dequeue tracks twice, expect track 2 in result
            var result = _competition.NextTrack();
            result = _competition.NextTrack();

            Assert.AreEqual(track2, result);
        }
    }
}
