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

            Console.WriteLine($"Track: {Data.CurrentRace.Track.Name}");

            // game loop
            for(; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
