using System;
using System.Threading;
using Controller;

namespace ConsoleEdition
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(180, 60);

            Data.Initialize(); // initialize data (tracks and participants)
            Data.VisualizationNextRaceEventHandler += Visualization.OnNextRaceNextRaceEvent; // tell data about visualization's next race method.
            Data.NextRace(); // start first race


            // game loop
            for (; ; )
            {
                //Data.CurrentRace.OnTimedEvent(Data.CurrentRace, new EventArgs());
                Thread.Sleep(100);
            }
        }
    }
}
