using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ConsoleEdition
{
    public enum Direction
    {
        UP,
        LEFT,
        DOWN,
        RIGHT
    }
    public static class Visualization
    {
        private const int _cursorStartPosX = 24;
        private const int _cursorStartPosY = 12;

        private static int _currentPosX = _cursorStartPosX;
        private static int _currentPosY = _cursorStartPosY;

        // tracks always start pointing right.
        private static Direction _currentDirection = Direction.RIGHT;

        #region graphics

        private static string[] _finishHorizontal = { "----", "  # ", "  # ", "----" };
        private static string[] _finishVertical = { "----", "  # ", "  # ", "----" };
        private static string[] _startGridHorizontal = { "----", "  ] ", "  ] ", "----" };

        #endregion
        public static void Initialize()
        {
            // initialize some stuff
        }

        public static void DrawTrack(Track track)
        {
            // to test, first draw string.
            DrawSingleSection(_finishHorizontal, ref _currentPosX, ref _currentPosY, ref _currentDirection);
            DrawSingleSection(_finishHorizontal, ref _currentPosX, ref _currentPosY, ref _currentDirection);
            DrawSingleSection(_finishHorizontal, ref _currentPosX, ref _currentPosY, ref _currentDirection);
        }

        public static void DrawSingleSection(string[] sectionStrings, ref int cursorX, ref int cursorY, ref Direction direction)
        {
            // steps to do:
            // start printing given section
            // prepare cursor position for next section.
            // while preparing for next section, take rotation of track into account.
            // (maybe direction variable?) ((or enum))
            foreach (string s in sectionStrings)
            {
                Console.SetCursorPosition(cursorX, cursorY);
                Console.Write(s);
                cursorY++;
            }

            cursorY -= 4;
            cursorX += 4;

        }

    }
}
