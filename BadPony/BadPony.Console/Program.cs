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
                Console.WriteLine();
            }
        }
    }
}
