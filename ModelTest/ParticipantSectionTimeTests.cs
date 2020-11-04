using Model;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Model.Test
{
    [TestFixture]
    public class ParticipantSectionTimeTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Add_2TimesSingleSectionSameParticipant_ShouldReplace()
        {
            // Arrange
            var section = new Section(SectionTypes.Straight);
            var participantSectionTime1 = new ParticipantSectionTime() { Name = "Test", Section = section, Time = TimeSpan.FromSeconds(12)};
            var participantSectionTime2 = new ParticipantSectionTime() { Name = "Test", Section = section, Time = TimeSpan.FromSeconds(6)};
            var list = new List<ParticipantSectionTime>();

            // Act
            participantSectionTime1.Add(list);
            participantSectionTime2.Add(list);
            var result = list[0];

            // Assert
            Assert.True(list.Count == 1);
            Assert.AreEqual(participantSectionTime2.Time, result.Time);
        }

        [Test]
        public void Add_2TimesSingleSection2Participants_ShouldHave2Items()
        {
            // Arrange
            var section = new Section(SectionTypes.Straight);
            var participantSectionTime1 = new ParticipantSectionTime() { Name = "Test", Section = section, Time = TimeSpan.FromSeconds(12) };
            var participantSectionTime2 = new ParticipantSectionTime() { Name = "Test2", Section = section, Time = TimeSpan.FromSeconds(6) };
            var list = new List<ParticipantSectionTime>();

            // Act
            participantSectionTime1.Add(list);
            participantSectionTime2.Add(list);

            // Assert
            Assert.True(list.Count == 2);
            Assert.AreEqual(participantSectionTime1, list[0]);
            Assert.AreEqual(participantSectionTime2, list[1]);
        }

        [Test]
        public void BestParticipant_InputMultipleTimes_ReturnBestParticipantBasedOnTime()
        {
            // Arrange
            var section1 = new Section(SectionTypes.Straight);
            var section2 = new Section(SectionTypes.LeftCorner);
            var sTime1 = new ParticipantSectionTime() { Name = "Test1", Section = section1, Time = TimeSpan.FromSeconds(2) };
            var sTime2 = new ParticipantSectionTime() { Name = "Test1", Section = section2, Time = TimeSpan.FromSeconds(3) };
            var sTime3 = new ParticipantSectionTime() { Name = "Test2", Section = section1, Time = TimeSpan.FromSeconds(4) };
            var sTime4 = new ParticipantSectionTime() { Name = "Test2", Section = section2, Time = TimeSpan.FromSeconds(5) };
            var sTime5 = new ParticipantSectionTime() { Name = "Test3", Section = section1, Time = TimeSpan.FromSeconds(1) }; // fastest
            var sTime6 = new ParticipantSectionTime() { Name = "Test3", Section = section2, Time = TimeSpan.FromSeconds(6) };
            var sTime7 = new ParticipantSectionTime() { Name = "Test4", Section = section1, Time = TimeSpan.FromSeconds(7) };
            var sTime8 = new ParticipantSectionTime() { Name = "Test4", Section = section2, Time = TimeSpan.FromSeconds(8) };
            var list = new List<ParticipantSectionTime>();

            // Act
            sTime1.Add(list);
            sTime2.Add(list);
            sTime3.Add(list);
            sTime4.Add(list);
            sTime5.Add(list);
            sTime6.Add(list);
            sTime7.Add(list);
            sTime8.Add(list);
            var result = list[0].BestParticipant(list);

            // Assert
            Assert.AreEqual("Test3",result);
        }
    }
}
