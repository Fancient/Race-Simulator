using NSubstitute;
using NUnit.Framework;
using System;
using System.Windows.Media.Imaging;
using Controller;
using Model;
using WpfEdition;
using static WpfEdition.Visualization;

namespace WpfEdition.Test
{
    [TestFixture]
    public class VisualizationTests
    {
        private Track _track;
        private Race _race;

        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            _track = Data.Competition.Tracks.Dequeue();
            _race = new Race(_track, Data.Competition.Participants);
            ImageCache.Initialize();
            Visualization.Initialize(_race);
        }

        [TestCase(Direction.N, Direction.W)]
        [TestCase(Direction.E, Direction.N)]
        [TestCase(Direction.S, Direction.E)]
        [TestCase(Direction.W, Direction.S)]
        public void DirectionLeftTurn_Input_ExpectedOutput(Direction input, Direction expected)
        {
            // Act
            var result = Visualization.DirectionLeftTurn(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(Direction.N, Direction.E)]
        [TestCase(Direction.E, Direction.S)]
        [TestCase(Direction.S, Direction.W)]
        [TestCase(Direction.W, Direction.N)]
        public void DirectionRightTurn_Input_ExpectedOutput(Direction input, Direction expected)
        {
            // Act
            var result = Visualization.DirectionRightTurn(input);

            // Assert
            Assert.AreEqual(expected, result);
        }


        [TestCase(Direction.N, SectionTypes.LeftCorner, Direction.W)]
        [TestCase(Direction.E, SectionTypes.LeftCorner, Direction.N)]
        [TestCase(Direction.S, SectionTypes.LeftCorner, Direction.E)]
        [TestCase(Direction.W, SectionTypes.LeftCorner, Direction.S)]
        [TestCase(Direction.N, SectionTypes.RightCorner, Direction.E)]
        [TestCase(Direction.E, SectionTypes.RightCorner, Direction.S)]
        [TestCase(Direction.S, SectionTypes.RightCorner, Direction.W)]
        [TestCase(Direction.W, SectionTypes.RightCorner, Direction.N)]
        [TestCase(Direction.N, SectionTypes.Finish, Direction.N)]
        public void DetermineDirectionForSectionType(Direction d, SectionTypes st, Direction expected)
        {
            // Act
            var result = Visualization.DetermineDirectionForSectionType(d, st);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetTrackSize_InputATrack_ReturnCorrectValues()
        {
            // Arrange
            var track = new Track("The Oval", new[]
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner
            });
            int expectedWidth = 1792;
            int expectedHeight = 768;
            int expectedStartX = 256;
            int expectedStartY = 0;

            int actualWidth;
            int actualHeight;
            (int X, int Y) actualStart;

            // Act
            (actualWidth, actualHeight, actualStart) = Visualization.GetTrackSize(track);

            // Assert
            Assert.AreEqual(expectedWidth, actualWidth);
            Assert.AreEqual(expectedHeight, actualHeight);
            Assert.AreEqual(expectedStartX, actualStart.X);
            Assert.AreEqual(expectedStartY, actualStart.Y);
        }

        [Test]
        public void DrawTrack_TrackAvailable_ReturnsBitmapSource()
        {
            // Act
            var result = Visualization.DrawTrack(_track);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BitmapSource>(result);
        }

        [TestCase(Side.Left, Direction.N, 60,80)]
        [TestCase(Side.Left, Direction.E, 112,60)]
        [TestCase(Side.Left, Direction.S, 132,112)]
        [TestCase(Side.Left, Direction.W, 80,132)]
        [TestCase(Side.Right, Direction.N, 132,112)]
        [TestCase(Side.Right, Direction.E, 80,132)]
        [TestCase(Side.Right, Direction.S, 60,80)]
        [TestCase(Side.Right, Direction.W, 112,60)]
        public void GetParticipantOffset_CorrectInput_ReturnCorrectOffset(Side side, Direction cd, int expectedX,
            int expectedY)
        {
            // Act
            var result = Visualization.GetParticipantOffset(side, cd);

            // Assert
            Assert.AreEqual(expectedX, result.x);
            Assert.AreEqual(expectedY, result.y);
        }

        [Test, Combinatorial]
        public void TeamColorToFileName_ReturnsNotEmptyString([Values]TeamColors color, [Values]Direction d)
        {
            // Act
            var result = Visualization.TeamColorToFilename(color, d);

            // Assert
            Assert.IsNotEmpty(result);
        }

        [TestCase(Direction.N, 10, 9)]
        [TestCase(Direction.E, 11, 10)]
        [TestCase(Direction.S, 10, 11)]
        [TestCase(Direction.W, 9, 10)]
        public void NextPosition_CorrectInput_ShouldChangeXOrY(Direction d, int expectedX, int expectedY)
        {
            // Arrange
            int actualX = 10;
            int actualY = 10;

            // Act
            Visualization.NextPosition(ref actualX, ref actualY, d);

            // Assert
            Assert.AreEqual(expectedX, actualX);
            Assert.AreEqual(expectedY, actualY);
        }

        [TestCase(Direction.N, Visualization.SectionDimension * 10, Visualization.SectionDimension * 9)]
        [TestCase(Direction.E, Visualization.SectionDimension * 11, Visualization.SectionDimension * 10)]
        [TestCase(Direction.S, Visualization.SectionDimension * 10, Visualization.SectionDimension * 11)]
        [TestCase(Direction.W, Visualization.SectionDimension * 9, Visualization.SectionDimension * 10)]
        public void MovePosition_CorrectInput_ShouldChangeXOrY(Direction d, int expectedX, int expectedY)
        {
            // Arrange
            int actualX = Visualization.SectionDimension * 10;
            int actualY = Visualization.SectionDimension * 10;

            // Act
            Visualization.MovePosition(ref actualX, ref actualY, d);

            // Assert
            Assert.AreEqual(expectedX, actualX);
            Assert.AreEqual(expectedY, actualY);
        }
    }
}
