using Controller;
using System;
using System.Threading;

namespace ConsoleEdition
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.SetWindowSize(180, 60);

            Data.Initialize(); // initialize data (tracks and participants)
            Data.NextRaceEventHandler += Visualization.OnNextRaceEvent; // tell data about visualization's next race method.
            Data.NextRace(); // start first race

            // game loop
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}