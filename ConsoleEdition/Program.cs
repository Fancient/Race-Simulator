using System;
using System.Threading;
using Controller;

namespace ConsoleEdition
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();

            Console.SetWindowSize(180,60);

            Console.WriteLine($"Track: {Data.CurrentRace.Track.Name}");
            
            Visualization.Initialize(Data.CurrentRace);
            Visualization.DrawTrack(Data.CurrentRace.Track);

            // game loop
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
