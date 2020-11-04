using Model;
using NUnit.Framework;

namespace ConsoleEdition.Test
{
    public class VisualizationTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [TestCase(SectionTypes.Straight, Direction.N, new string[] { "|  |", "|2 |", "| 1|", "|  |" })]
        [TestCase(SectionTypes.Straight, Direction.S, new string[] { "|  |", "|2 |", "| 1|", "|  |" })]
        [TestCase(SectionTypes.Straight, Direction.E, new string[] { "----", "  1 ", " 2  ", "----" })]
        [TestCase(SectionTypes.Straight, Direction.W, new string[] { "----", "  1 ", " 2  ", "----" })]
        [TestCase(SectionTypes.Finish, Direction.E, new string[] { "----", " 1# ", "2 # ", "----" })]
        [TestCase(SectionTypes.StartGrid, Direction.E, new string[] { "----", " 1] ", "2]  ", "----" })]
        [TestCase(SectionTypes.LeftCorner, Direction.N, new string[] { @"--\ ", @"  1\", @" 2 |", @"\  |" })]
        [TestCase(SectionTypes.LeftCorner, Direction.E, new string[] { @"/  |", @" 1 |", @"  2/", @"--/ " })]
        [TestCase(SectionTypes.LeftCorner, Direction.S, new string[] { @"|  \", @"| 1 ", @"\2  ", @" \--" })]
        [TestCase(SectionTypes.LeftCorner, Direction.W, new string[] { @" /--", @"/1  ", @"| 2 ", @"|  /"  })]
        [TestCase(SectionTypes.RightCorner, Direction.N, new string[] { @" /--", @"/1  ", @"| 2 ", @"|  /" })]
        [TestCase(SectionTypes.RightCorner, Direction.E, new string[] { @"--\ ", @"  1\", @" 2 |", @"\  |" })]
        [TestCase(SectionTypes.RightCorner, Direction.S, new string[] { @"/  |", @" 1 |", @"  2/", @"--/ " })]
        [TestCase(SectionTypes.RightCorner, Direction.W, new string[] { @"|  \", @"| 1 ", @"\2  ", @" \--" })]
        public void SectionTypeToGraphic_Input_ResultExpected(SectionTypes type, Direction d, string[] expectedStrings)
        {
            // Act
            var result = Visualization.SectionTypeToGraphic(type, d);

            // Assert
            Assert.AreEqual(expectedStrings, result);
        }

        [TestCase(Direction.N, Direction.W)]
        [TestCase(Direction.E, Direction.N)]
        [TestCase(Direction.S, Direction.E)]
        [TestCase(Direction.W, Direction.S)]
        public void ChangeDirectionLeft_Input_ExpectedOutput(Direction input, Direction expected)
        {
            // Act
            var result = Visualization.ChangeDirectionLeft(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(Direction.N, Direction.E)]
        [TestCase(Direction.E, Direction.S)]
        [TestCase(Direction.S, Direction.W)]
        [TestCase(Direction.W, Direction.N)]
        public void ChangeDirectionRight_Input_ExpectedOutput(Direction input, Direction expected)
        {
            // Act
            var result = Visualization.ChangeDirectionRight(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReplacePlaceHolders_InputCorrectStringAndParticipants_ShouldReplace()
        {
            // Arrange
            var participant1 = new Driver("Abb",0,new Car(0, 0, 0, false), TeamColors.Blue);
            var participant2 = new Driver("Bbb", 0, new Car(0, 0, 0, false), TeamColors.Blue);
            string[] inputStrings = {"|  |", "|1 |", "| 2|", "|  |"};
            string[] expectedStrings = {"|  |", "|A |", "| B|", "|  |"};

            // Act
            var resultStrings = Visualization.ReplacePlaceHolders(inputStrings, participant1, participant2);

            // Assert
            Assert.AreEqual(expectedStrings, resultStrings);
        }

        [Test]
        public void ReplacePlaceHolders_InputCorrectStringAndBrokenParticipants_ShouldReplace()
        {
            // Arrange
            var participant1 = new Driver("Abb", 0, new Car(0, 0, 0, true), TeamColors.Blue);
            var participant2 = new Driver("Bbb", 0, new Car(0, 0, 0, true), TeamColors.Blue);
            string[] inputStrings = { "|  |", "|1 |", "| 2|", "|  |" };
            string[] expectedStrings = { "|  |", "|X |", "| X|", "|  |" };

            // Act
            var resultStrings = Visualization.ReplacePlaceHolders(inputStrings, participant1, participant2);

            // Assert
            Assert.AreEqual(expectedStrings, resultStrings);
        }
    }
}