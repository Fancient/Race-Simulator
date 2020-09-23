using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ConsoleEdition
{
    public enum Direction
    {
        N,
        E,
        S,
        W
    }
    public static class Visualization
    {
        private const int _cursorStartPosX = 24;
        private const int _cursorStartPosY = 12;

        private static int _cPosX = _cursorStartPosX;
        private static int _cPosY = _cursorStartPosY;

        // tracks always start pointing right.
        private static Direction _currentDirection = Direction.E;

        #region graphics
        private static string[] _finishHorizontal = { "----", "  # ", "  # ", "----" };
        private static string[] _startGridHorizontal = { "----", "  ] ", "  ] ", "----" };

        private static string[] _straightHorizontal = {"----", "    ", "    ", "----"};
        private static string[] _straightVertical = {"|  |", "|  |", "|  |", "|  |"};

        private static string[] _cornerNE = {" /--", "/   ", "|   ", "|  /"};
        private static string[] _cornerNW = {@"--\ ", @"   \", @"   |", @"\  |"};
        private static string[] _cornerSE = {@"|  \", @"|   ", @"\   ", @" \--"};
        private static string[] _cornerSW = { "/  |", "   |", "   /", "--/ " };
        #endregion

        public static string[] SectionTypeToGraphic(SectionTypes sectionType, Direction direction)
        {
            return sectionType switch
            {
                SectionTypes.Straight => ((int)direction % 2) switch
                {
                    0 => _straightVertical,
                    1 => _straightHorizontal,
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                },
                SectionTypes.LeftCorner => (int)direction switch
                {
                    0 => _cornerNW,
                    1 => _cornerSW,
                    2 => _cornerSE,
                    3 => _cornerNE,
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                },
                SectionTypes.RightCorner => (int)direction switch
                {
                    0 => _cornerNE,
                    1 => _cornerNW,
                    2 => _cornerSW,
                    3 => _cornerSE,
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                },
                SectionTypes.StartGrid => _startGridHorizontal,
                SectionTypes.Finish => _finishHorizontal,
                _ => throw new ArgumentOutOfRangeException(nameof(sectionType), sectionType, null)
            };
        }

        public static void Initialize()
        {
            // initialize some stuff
        }

        public static void DrawTrack(Track track)
        {
            // to test, first draw string.
            foreach (Section trackSection in track.Sections)
            {
                DrawSingleSection(trackSection);
            }
        }


        public static void DrawSingleSection(Section section)
        {
            // first determine section string
            string[] sectionStrings = SectionTypeToGraphic(section.SectionType, _currentDirection);

            // print section
            int tempY = _cPosY;
            foreach (string s in sectionStrings)
            {
                Console.SetCursorPosition(_cPosX,tempY);
                Console.Write(s);
                tempY++;
            }

            // flip direction if corner piece
            if (section.SectionType == SectionTypes.RightCorner)
                _currentDirection = ChangeDirectionRight(_currentDirection);
            else if (section.SectionType == SectionTypes.LeftCorner)
                _currentDirection = ChangeDirectionLeft(_currentDirection);

            // change cursor position based on current.
            ChangeCursorToNextPosition();

        }

        public static Direction ChangeDirectionLeft(Direction d)
        {
            return d switch
            {
                Direction.N => Direction.W,
                Direction.E => Direction.N,
                Direction.S => Direction.E,
                Direction.W => Direction.S,
                _ => throw new ArgumentOutOfRangeException(nameof(d), d, null)
            };
        }

        public static Direction ChangeDirectionRight(Direction d)
        {
            return d switch
            {
                Direction.N => Direction.E,
                Direction.E => Direction.S,
                Direction.S => Direction.W,
                Direction.W => Direction.N,
                _ => throw new ArgumentOutOfRangeException(nameof(d), d, null)
            };
        }


        public static void ChangeCursorToNextPosition()
        {
            switch (_currentDirection)
            {
                case Direction.N:
                    _cPosY -= 4;
                    break;
                case Direction.E:
                    _cPosX += 4;
                    break;
                case Direction.S:
                    _cPosY += 4;
                    break;
                case Direction.W:
                    _cPosX -= 4;
                    break;
            }
        }
    }
}
