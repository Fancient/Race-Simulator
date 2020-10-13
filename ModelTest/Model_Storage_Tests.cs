using NUnit.Framework;
using System;

namespace Model.Test
{
    [TestFixture]
    internal class Model_Storage_Tests
    {
        [Test]
        public void Instantiation_IStorageConstraint_IsNotNull()
        {
            // arrange

            // act
            var result = new Storage<IStorageConstraint>();

            // assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddToList_InstanceOfParticipantPoints_AddsToList()
        {
            // arrange
            var input = new ParticipantPoints();
            var storage = new Storage<ParticipantPoints>();

            // act
            storage.AddToList(input);

            // assert, don't really know how to tackle this,
            // but this test will fail when an exception is thrown at AddToList
            Assert.Pass();
        }

        // test all available classes BestParticipants
        [Test]
        public void BestParticipant_ParticipantSpeed_ReturnsBestParticipantsName()
        {
            // Arrange
            var BestParticipantName = "best";
            var part1 = new ParticipantSpeed() { Name = "n1", Speed = 23, TrackName = "a" };
            var part2 = new ParticipantSpeed() { Name = "n2", Speed = 27, TrackName = "a" };
            var part3 = new ParticipantSpeed() { Name = "n3", Speed = 34, TrackName = "a" };
            var bestp = new ParticipantSpeed() { Name = BestParticipantName, Speed = 43, TrackName = "a" };

            var storage = new Storage<ParticipantSpeed>();
            storage.AddToList(part1);
            storage.AddToList(part2);
            storage.AddToList(bestp);
            storage.AddToList(part3);

            // Act
            var result = storage.BestParticipant();

            // Assert
            Assert.AreEqual(BestParticipantName, result);
        }

        [Test]
        public void BestParticipant_ParticipantSectionTime_ReturnsBestParticipantsName()
        {
            // Arrange
            var BestParticipantName = "best";
            var section1 = new Section(SectionTypes.Straight);

            var part1 = new ParticipantSectionTime() { Name = "n1", Section = section1, Time = TimeSpan.FromSeconds(20) };
            var part2 = new ParticipantSectionTime() { Name = "n2", Section = section1, Time = TimeSpan.FromSeconds(17) };
            var part3 = new ParticipantSectionTime() { Name = "n3", Section = section1, Time = TimeSpan.FromSeconds(18) };
            var bestp = new ParticipantSectionTime() { Name = BestParticipantName, Section = section1, Time = TimeSpan.FromSeconds(10) };

            var storage = new Storage<ParticipantSectionTime>();
            storage.AddToList(part1);
            storage.AddToList(part2);
            storage.AddToList(bestp);
            storage.AddToList(part3);

            // Act
            var result = storage.BestParticipant();

            // Assert
            Assert.AreEqual(BestParticipantName, result);
        }

        [Test]
        public void BestParticipant_ParticipantLapTime_ReturnsBestParticipantsName()
        {
            // Arrange
            var BestParticipantName = "best";

            var part1 = new ParticipantLapTime() { Name = "n1", Time = TimeSpan.FromSeconds(60) };
            var part2 = new ParticipantLapTime() { Name = "n2", Time = TimeSpan.FromSeconds(52) };
            var part3 = new ParticipantLapTime() { Name = "n3", Time = TimeSpan.FromSeconds(56) };
            var bestp = new ParticipantLapTime() { Name = BestParticipantName, Time = TimeSpan.FromSeconds(49) };

            var storage = new Storage<ParticipantLapTime>();
            storage.AddToList(part1);
            storage.AddToList(part2);
            storage.AddToList(bestp);
            storage.AddToList(part3);

            // Act
            var result = storage.BestParticipant();

            // Assert
            Assert.AreEqual(BestParticipantName, result);
        }

        [Test]
        public void BestParticipant_ParticipantPoints_ReturnsBestParticipantsName()
        {
            // Arrange
            var BestParticipantName = "best";
            var part1 = new ParticipantPoints() { Name = "n1", Points = 15 };
            var part2 = new ParticipantPoints() { Name = "n2", Points = 27 };
            var part3 = new ParticipantPoints() { Name = "n3", Points = 20 };
            var bestp = new ParticipantPoints() { Name = BestParticipantName, Points = 35 };

            var storage = new Storage<ParticipantPoints>();
            storage.AddToList(part1);
            storage.AddToList(part2);
            storage.AddToList(bestp);
            storage.AddToList(part3);

            // Act
            var result = storage.BestParticipant();

            // Assert
            Assert.AreEqual(BestParticipantName, result);
        }

        [Test]
        public void Add_ParticipantPoints_PointsAdded()
        {
            // Arrange
            var part1 = new ParticipantPoints() { Name = "A", Points = 15 };
            var part2 = new ParticipantPoints() { Name = "A", Points = 8 };

            var storage = new Storage<ParticipantPoints>();

            // Act
            storage.AddToList(part1);
            var points1 = storage.GetList()[0].Points;
            storage.AddToList(part2);
            var points2 = storage.GetList()[0].Points;

            // Assert
            Assert.AreEqual(15, points1);
            Assert.AreEqual(23, points2);
        }
    }
}