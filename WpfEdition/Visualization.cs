using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;

namespace WpfEdition
{
    public static class Visualization
    {
        internal enum Direction
        {
            N,
            E,
            S,
            W
        }

        internal enum Side
        {
            Left,
            Right
        }

        private static Race _race;

        #region SizeConstants

        private const int SectionDimension = 256;
        private const int DriverDimension = 64;

        #endregion SizeConstants

        #region ImagePaths

        // Track Pieces

        private const string StraightHorizontal = @".\Resource\TrackPieces\StraightHorizontal.png";
        private const string StraightVertical = @".\Resource\TrackPieces\StraightVertical.png";
        private const string StartGrid = @".\Resource\TrackPieces\StartGrid.png";
        private const string Finish = @".\Resource\TrackPieces\Finish.png";
        private const string CornerNE = @".\Resource\TrackPieces\CornerNE.png";
        private const string CornerNW = @".\Resource\TrackPieces\CornerNW.png";
        private const string CornerSE = @".\Resource\TrackPieces\CornerSE.png";
        private const string CornerSW = @".\Resource\TrackPieces\CornerSW.png";

        // Facing North

        private const string TeamBlueN = @".\Resource\Cars\N\TeamBlue.png";
        private const string TeamGreenN = @".\Resource\Cars\N\TeamGreen.png";
        private const string TeamOrangeN = @".\Resource\Cars\N\TeamOrange.png";
        private const string TeamPinkN = @".\Resource\Cars\N\TeamPink.png";
        private const string TeamRedN = @".\Resource\Cars\N\TeamRed.png";
        private const string TeamYellowN = @".\Resource\Cars\N\TeamYellow.png";

        // Facing East

        private const string TeamBlueE = @".\Resource\Cars\E\TeamBlue.png";
        private const string TeamGreenE = @".\Resource\Cars\E\TeamGreen.png";
        private const string TeamOrangeE = @".\Resource\Cars\E\TeamOrange.png";
        private const string TeamPinkE = @".\Resource\Cars\E\TeamPink.png";
        private const string TeamRedE = @".\Resource\Cars\E\TeamRed.png";
        private const string TeamYellowE = @".\Resource\Cars\E\TeamYellow.png";

        // Facing South

        private const string TeamBlueS = @".\Resource\Cars\S\TeamBlue.png";
        private const string TeamGreenS = @".\Resource\Cars\S\TeamGreen.png";
        private const string TeamOrangeS = @".\Resource\Cars\S\TeamOrange.png";
        private const string TeamPinkS = @".\Resource\Cars\S\TeamPink.png";
        private const string TeamRedS = @".\Resource\Cars\S\TeamRed.png";
        private const string TeamYellowS = @".\Resource\Cars\S\TeamYellow.png";

        // Facing West

        private const string TeamBlueW = @".\Resource\Cars\W\TeamBlue.png";
        private const string TeamGreenW = @".\Resource\Cars\W\TeamGreen.png";
        private const string TeamOrangeW = @".\Resource\Cars\W\TeamOrange.png";
        private const string TeamPinkW = @".\Resource\Cars\W\TeamPink.png";
        private const string TeamRedW = @".\Resource\Cars\W\TeamRed.png";
        private const string TeamYellowW = @".\Resource\Cars\W\TeamYellow.png";

        // Broken
        private const string Broken = @".\Resource\X.png";

        #endregion ImagePaths

        public static void Initialize(Race race)
        {
            _race = race;
        }

        /// <summary>
        /// Draw track on a BitmapSource object
        /// </summary>
        /// <param name="track">Track to draw</param>
        /// <returns>BitmapSource of drawn track</returns>
        public static BitmapSource DrawTrack(Track track)
        {
            (int width, int heigth) size;
            (int x, int y) startPosition;
            (size.width, size.heigth, startPosition) = GetTrackSize(track);
            Bitmap Canvas = ImageCache.CreateEmptyBitmap(size.width, size.heigth);
            Graphics g = Graphics.FromImage(Canvas);

            int xPos = startPosition.x;
            int yPos = startPosition.y;
            Direction currentDirection = Direction.E;

            foreach (Section section in track.Sections)
            {
                // draw the section
                DrawSingleSection(xPos, yPos, currentDirection, g, section);

                // determine direction for sectionType
                currentDirection = DetermineDirectionForSectionType(currentDirection, section.SectionType);

                // draw the participants (if there) on the section
                DrawParticipantsOnSection(xPos, yPos, currentDirection, g, section);

                // move position
                MovePosition(ref xPos, ref yPos, currentDirection);
            }

            return ImageCache.CreateBitmapSourceFromGdiBitmap(Canvas);
        }

        /// <summary>
        /// Draw participants on section (if there are participants on section).
        /// </summary>
        /// <param name="xPos">current position of X</param>
        /// <param name="yPos">current position of Y</param>
        /// <param name="currentDirection">current direction</param>
        /// <param name="g">Graphics object to draw on</param>
        /// <param name="section">Current Section object</param>
        private static void DrawParticipantsOnSection(int xPos, int yPos, Direction currentDirection, Graphics g, Section section)
        {
            // look for participants
            IParticipant leftParticipant = _race.GetSectionData(section).Left;
            IParticipant rightParticipant = _race.GetSectionData(section).Right;

            if (leftParticipant != null)
            {
                (int x, int y) = GetParticipantOffset(Side.Left, currentDirection); // get x&y offset for participant
                DrawParticipantOnCoord(leftParticipant, g, currentDirection, xPos + x, yPos + y); // draw participant
                if (leftParticipant.Equipment.IsBroken)
                    DrawBrokenImageOnCoord(g, xPos + x, yPos + y); // draw broken image on top of participant if participant is broken.
            }

            if (rightParticipant != null)
            {
                (int x, int y) = GetParticipantOffset(Side.Right, currentDirection); // get x&y offset for participant
                DrawParticipantOnCoord(rightParticipant, g, currentDirection, xPos + x, yPos + y); // draw participant
                if (rightParticipant.Equipment.IsBroken)
                    DrawBrokenImageOnCoord(g, xPos + x, yPos + y); // draw broken image on top of participant if participant is broken.
            }
        }

        /// <summary>
        /// Draws Image that means a participant is broken
        /// </summary>
        /// <param name="g">Graphics object to draw on</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void DrawBrokenImageOnCoord(Graphics g, int x, int y)
        {
            g.DrawImage(ImageCache.GetBitmap(Broken), x, y, DriverDimension, DriverDimension);
        }

        /// <summary>
        /// Gets pixel offsets for driver based on side and direction
        /// </summary>
        /// <param name="side">side of driver (Left or Right)</param>
        /// <param name="currentDirection">current direction of driver</param>
        /// <returns>tuple of x and y</returns>
        private static (int x, int y) GetParticipantOffset(Side side, Direction currentDirection)
        {
            return side switch
            {
                Side.Left when currentDirection == Direction.N => (60, 80), // side to side: 60, 96
                Side.Left when currentDirection == Direction.E => (112, 60), // side to side: 96, 60
                Side.Left when currentDirection == Direction.S => (132, 112), // side to side: 132, 96
                Side.Left when currentDirection == Direction.W => (80, 132), // side to side: 96, 132
                Side.Right when currentDirection == Direction.N => (132, 112), // side to side: 132, 96
                Side.Right when currentDirection == Direction.E => (80, 132), // side to side: 96, 132
                Side.Right when currentDirection == Direction.S => (60, 80), // side to side: 60, 96
                Side.Right when currentDirection == Direction.W => (112, 60), // side to side: 96, 60
                _ => (0, 0) // whenever side is incorrect (IDE wants this)
            };
        }

        /// <summary>
        /// Draw Participant on given coordinates
        /// </summary>
        /// <param name="participant">participant to draw</param>
        /// <param name="g">Graphics object to draw on</param>
        /// <param name="d">Direction that participant is facing (current direction)</param>
        /// <param name="xPos">x position in pixels</param>
        /// <param name="yPos">y position in pixels</param>
        private static void DrawParticipantOnCoord(IParticipant participant, Graphics g, Direction d, int xPos, int yPos)
        {
            Bitmap participantBitmap = ImageCache.GetBitmap(TeamColorToFilename(participant.TeamColor, d));
            g.DrawImage(participantBitmap, xPos, yPos, DriverDimension, DriverDimension);
        }

        /// <summary>
        /// Gets filename of image that belongs to teamcolor and current direction
        /// </summary>
        /// <param name="color">Teamcolor of participant</param>
        /// <param name="d">current direction</param>
        /// <returns>filename</returns>
        private static string TeamColorToFilename(TeamColors color, Direction d)
        {
            return d switch
            {
                Direction.N => color switch
                {
                    TeamColors.Red => TeamRedN,
                    TeamColors.Green => TeamGreenN,
                    TeamColors.Yellow => TeamYellowN,
                    TeamColors.Orange => TeamOrangeN,
                    TeamColors.Blue => TeamBlueN,
                    TeamColors.Pink => TeamPinkN,
                    _ => throw new ArgumentOutOfRangeException(nameof(color), color, "Invalid value for team color.")
                },
                Direction.E => color switch
                {
                    TeamColors.Red => TeamRedE,
                    TeamColors.Green => TeamGreenE,
                    TeamColors.Yellow => TeamYellowE,
                    TeamColors.Orange => TeamOrangeE,
                    TeamColors.Blue => TeamBlueE,
                    TeamColors.Pink => TeamPinkE,
                    _ => throw new ArgumentOutOfRangeException(nameof(color), color, "Invalid value for team color.")
                },
                Direction.S => color switch
                {
                    TeamColors.Red => TeamRedS,
                    TeamColors.Green => TeamGreenS,
                    TeamColors.Yellow => TeamYellowS,
                    TeamColors.Orange => TeamOrangeS,
                    TeamColors.Blue => TeamBlueS,
                    TeamColors.Pink => TeamPinkS,
                    _ => throw new ArgumentOutOfRangeException(nameof(color), color, "Invalid value for team color.")
                },
                Direction.W => color switch
                {
                    TeamColors.Red => TeamRedW,
                    TeamColors.Green => TeamGreenW,
                    TeamColors.Yellow => TeamYellowW,
                    TeamColors.Orange => TeamOrangeW,
                    TeamColors.Blue => TeamBlueW,
                    TeamColors.Pink => TeamPinkW,
                    _ => throw new ArgumentOutOfRangeException(nameof(color), color, "Invalid value for team color.")
                },
                _ => throw new ArgumentOutOfRangeException(nameof(d), d, "Invalid value for direction.")
            };
        }

        /// <summary>
        /// Draws a single section piece on graphics object
        /// </summary>
        /// <param name="xPos">current x position in pixels</param>
        /// <param name="yPos">current y position in pixels</param>
        /// <param name="direction">current direction</param>
        /// <param name="g">Graphics object that will be used to draw on</param>
        /// <param name="section">current Section object</param>
        private static void DrawSingleSection(int xPos, int yPos, Direction direction, Graphics g, Section section)
        {
            Bitmap sectionBitmap = ImageCache.GetBitmap(SectionTypeToFilename(section.SectionType, direction)); // get section from cache
            g.DrawImage(sectionBitmap, xPos, yPos, SectionDimension, SectionDimension); // draw the image
        }

        /// <summary>
        /// Moves position of "cursor" based on current position and direction.
        /// </summary>
        /// <param name="xPos">reference to current value for X</param>
        /// <param name="yPos">reference to current value for Y</param>
        /// <param name="direction">current facing direction</param>
        private static void MovePosition(ref int xPos, ref int yPos, Direction direction)
        {
            switch (direction)
            {
                case Direction.N:
                    yPos -= SectionDimension;
                    break;

                case Direction.E:
                    xPos += SectionDimension;
                    break;

                case Direction.S:
                    yPos += SectionDimension;
                    break;

                case Direction.W:
                    xPos -= SectionDimension;
                    break;
            }
        }

        /// <summary>
        /// is used by DrawTrack method; changes direction if current section is a corner;
        /// </summary>
        /// <param name="d">current direction</param>
        /// <param name="st">current section's type</param>
        /// <returns>new direction</returns>
        private static Direction DetermineDirectionForSectionType(Direction d, SectionTypes st)
        {
            return st switch
            {
                SectionTypes.LeftCorner => DirectionLeftTurn(d),
                SectionTypes.RightCorner => DirectionRightTurn(d),
                _ => d
            };
        }

        /// <summary>
        /// Calculates Track Size and Determines start position
        /// </summary>
        /// <param name="track"></param>
        /// <returns>Width and Height in pixels, Tuple start position in pixels</returns>
        private static (int width, int height, (int x, int y)) GetTrackSize(Track track)
        {
            int startX = 10, startY = 10; // start at 10, 10. This way we can determine min and max positions
            int curX = startX, curY = startY;
            List<int> positionsX = new List<int>();
            List<int> positionsY = new List<int>();
            Direction direction = Direction.E; // start east

            // fill lists
            foreach (Section section in track.Sections)
            {
                // determine direction, set x and y accordingly, add to list.

                // add position to lists
                positionsX.Add(curX);
                positionsY.Add(curY);

                // change direction if needed
                if (section.SectionType == SectionTypes.LeftCorner)
                {
                    direction = DirectionLeftTurn(direction);
                }
                else if (section.SectionType == SectionTypes.RightCorner)
                {
                    direction = DirectionRightTurn(direction);
                }

                // go to next position based on current direction
                NextPosition(ref curX, ref curY, direction);
            }

            // determine min and max positions
            int minX = positionsX.Min();
            int maxX = positionsX.Max() + 1; // give enough room for drawing method
            int minY = positionsY.Min();
            int maxY = positionsY.Max() + 1; // give enough room for drawing method

            // determine size
            int width = (maxX - minX) * SectionDimension;
            int height = (maxY - minY) * SectionDimension;

            // determine startposition
            int x = (startX - minX) * SectionDimension;
            int y = (startY - minY) * SectionDimension;

            return (width, height, (x, y));
        }

        /// <summary>
        /// Switch expression to return the right filename belonging to the given sectiontype and direction
        /// </summary>
        /// <param name="sectionType">Current sections type</param>
        /// <param name="direction">Current facing direction</param>
        /// <returns>filename</returns>
        private static string SectionTypeToFilename(SectionTypes sectionType, Direction direction)
        {
            return sectionType switch
            {
                SectionTypes.Straight => ((int)direction % 2) switch
                {
                    0 => StraightVertical,
                    1 => StraightHorizontal,
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                },
                SectionTypes.LeftCorner => (int)direction switch
                {
                    0 => CornerNW,
                    1 => CornerSW,
                    2 => CornerSE,
                    3 => CornerNE,
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                },
                SectionTypes.RightCorner => (int)direction switch
                {
                    0 => CornerNE,
                    1 => CornerNW,
                    2 => CornerSW,
                    3 => CornerSE,
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                },
                SectionTypes.StartGrid => StartGrid,
                SectionTypes.Finish => Finish,
                _ => throw new ArgumentOutOfRangeException(nameof(sectionType), sectionType, null)
            };
        }

        /// <summary>
        /// Sets next position of cursor, this method is used in the GetTrackSize method.
        /// </summary>
        /// <param name="curX"></param>
        /// <param name="curY"></param>
        /// <param name="direction"></param>
        private static void NextPosition(ref int curX, ref int curY, Direction direction)
        {
            switch (direction)
            {
                case Direction.N:
                    curY--;
                    break;

                case Direction.E:
                    curX++;
                    break;

                case Direction.S:
                    curY++;
                    break;

                case Direction.W:
                    curX--;
                    break;
            }
        }

        /// <summary>
        /// lowers the direction enum by 1. (Taking a left turn)
        /// when it is at first element, goes to last
        /// </summary>
        /// <param name="d"></param>
        /// <returns>previous element in Direction</returns>
        internal static Direction DirectionLeftTurn(Direction d)
        {
            return (Direction)(((uint)d - 1) % 4);
        }

        /// <summary>
        /// raises the direction enum by 1. (Taking a right turn)
        /// when it is at last element, goes to first
        /// </summary>
        /// <param name="d"></param>
        /// <returns>next element in Direction</returns>
        internal static Direction DirectionRightTurn(Direction d)
        {
            return (Direction)(((uint)d + 1) % 4);
        }
    }
}