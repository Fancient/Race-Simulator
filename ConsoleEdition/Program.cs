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

            //Console.WriteLine($"Track: {Data.CurrentRace.Track.Name}");
            
            Visualization.Initialize(Data.CurrentRace);
            // add event
            Data.CurrentRace.DriversChanged += Visualization.OnDriversChanged;

            Visualization.DrawTrack(Data.CurrentRace.Track);

            Data.CurrentRace.RandomizeEquipment();

            //Data.CurrentRace.Start();

            // game loop
            for (; ; )
            {
                //Data.CurrentRace.OnTimedEvent(Data.CurrentRace, new EventArgs());
                Thread.Sleep(100);
            }
        }
    }
}
