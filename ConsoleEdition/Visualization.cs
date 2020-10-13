using System;
using System.Collections.Generic;
using System.Text;
using Controller;
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
        private const int _cursorStartPosY = 16;

        private static int _cPosX;
        private static int _cPosY;

        private static Race _currentRace;

        // tracks always start pointing right.
        private static Direction _currentDirection;

        #region graphics
        private static string[] _finishHorizontal = { "----", " 1# ", "2 # ", "----" };
        private static string[] _startGridHorizontal = { "----", " 1] ", "2]  ", "----" };

        private static string[] _straightHorizontal = { "----", "  1 ", " 2  ", "----" };
        private static string[] _straightVertical = { "|  |", "|2 |", "| 1|", "|  |" };

        private static string[] _cornerNE = { " /--", "/1  ", "| 2 ", "|  /" };
        private static string[] _cornerNW =
        {
            @"--\ ",
            @"  1\",
            @" 2 |",
            @"\  |"
        };
        private static string[] _cornerSE =
        {
            @"|  \",
            @"| 1 ",
            @"\2  ",
            @" \--"
        };
        private static string[] _cornerSW =
        {
            "/  |", 
            " 1 |",
            "  2/",
            "--/ "
        };
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

        public static void OnNextRaceNextRaceEvent(object sender, RaceStartEventArgs e)
        {
            // reinitialize
            Initialize(e.Race);

            // link events, draw track first time
            _currentRace.DriversChanged += OnDriversChanged;

            DrawTrack(_currentRace.Track);
        }

        public static void Initialize(Race race)
        {
            // initialize race and prepare console
            _currentRace = race;
            _currentDirection = Direction.E;
            PrepareConsole();
        }

        public static void PrepareConsole()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Track: {_currentRace.Track.Name}");
        }

        public static void DrawTrack(Track track)
        {
            _cPosX = _cursorStartPosX;
            _cPosY = _cursorStartPosY;
            // just to be sure, reset cursorposition
            Console.SetCursorPosition(_cPosX, _cPosY);
            // for testing purposes, draw participants
            PrintParticipants();

            // level 6.9, print best participants for section time and lap time.
            PrintBestParticipants();

            foreach (Section trackSection in track.Sections)
            {
                DrawSingleSection(trackSection);
            }
        }



        public static void DrawSingleSection(Section section)
        {
            // first determine section string
            string[] sectionStrings = ReplacePlaceHolders(
                SectionTypeToGraphic(section.SectionType, _currentDirection),
                _currentRace.GetSectionData(section).Left, _currentRace.GetSectionData(section).Right
            );

            // print section
            int tempY = _cPosY;
            foreach (string s in sectionStrings)
            {
                Console.SetCursorPosition(_cPosX, tempY);
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

        // todo: make reference
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

        public static string[] ReplacePlaceHolders(string[] inputStrings, IParticipant leftParticipant, IParticipant rightParticipant)
        {
            // create returnStrings array
            string[] returnStrings = new string[inputStrings.Length];

             
            // gather letters from Participants, letter will be a whitespace when participant is null;
            string lP = leftParticipant == null ? " " : leftParticipant.Equipment.IsBroken ? "X" : leftParticipant.Name.Substring(0, 1).ToUpper();
            string rP = rightParticipant == null ? " " : rightParticipant.Equipment.IsBroken ? "X" : rightParticipant.Name.Substring(0, 1).ToUpper();

            // replace string 1 and 2 with participants
            for (int i = 0; i < returnStrings.Length; i++)
            {
                returnStrings[i] = inputStrings[i].Replace("1", lP).Replace("2", rP);
            }

            return returnStrings;
        }

        // event handler OnDriversChanged, this is called when drivers change position on Track.
        public static void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            DrawTrack(e.Track);
        }

        private static void PrintBestParticipants()
        {
            Console.SetCursorPosition(0, 40);
            // padding is needed because name changes and console is not cleared constantly.
            Console.WriteLine($"Best section time done by: {_currentRace.GetBestParticipantSectionTime().PadRight(10)}");
            Console.WriteLine($"Best lap time done by:     {_currentRace.GetBestParticipantLapTime().PadRight(10)}");
        }
        private static void PrintParticipants()
        {
            // TODO: Remove debugging method
            Console.SetCursorPosition(0,1);
            foreach (IParticipant participant in _currentRace.Participants)
            {
                
                Console.WriteLine($"{(participant.Name + ":").PadRight(7)} Speed: {participant.Equipment.Speed.ToString().PadRight(3)} Performance: {participant.Equipment.Performance.ToString().PadRight(3)} Quality: {participant.Equipment.Quality.ToString().PadRight(3)} Actual speed {_currentRace.GetSpeedFromParticipant(participant).ToString().PadRight(3)} Distance: {_currentRace.GetDistanceParticipant(participant).ToString().PadRight(3)} Laps:{_currentRace.GetLapsParticipant(participant).ToString().PadLeft(2)} broken: {participant.Equipment.IsBroken.ToString().PadRight(5)}");
            }
        }
    }
}
