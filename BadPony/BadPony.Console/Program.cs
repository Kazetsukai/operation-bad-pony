using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadPony.Core;

namespace BadPony.ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Game gameInstance = new Game();

            var location = gameInstance.GetAllObjects().FirstOrDefault();

            while (true)
            {
                ShowRoom(location);

                var line = Console.ReadLine();

                if (line.ToLower() == "exit")
                    return;


            }
        }

        private static void ShowRoom(GameObject location)
        {
            if (location == null)
            {
                Console.WriteLine("You are floating in the void.");
            }
            else
            {
                PrintInColour(location.Name, ConsoleColor.White);
                Console.WriteLine();
                Console.WriteLine(location.Description);
            }
        }

        // Classic defiance against American spelling
        public static void PrintInColour(string message, ConsoleColor colour, ConsoleColor backgroundColour = ConsoleColor.Black)
        {
            var oldColour = Console.ForegroundColor;
            var oldBackColour = Console.BackgroundColor;

            Console.ForegroundColor = colour;
            Console.BackgroundColor = backgroundColour;

            Console.WriteLine(message);

            Console.ForegroundColor = oldColour;
            Console.BackgroundColor = oldBackColour;
        }
    }
}
