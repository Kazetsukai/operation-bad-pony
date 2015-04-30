using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using BadPony.Core;

namespace BadPony.WebApiHost
{
    class AdminCLI
    {
        private static bool exit = false;        
        private static string prompt = "BP: ";
        private static string adminResources = "./AdminResources/";
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string input;
        private string[] command;

        public void StartCLI(Game game)
        {
            while (!exit)
            {
                Console.ResetColor();
                Console.Write(prompt);
                input = Console.ReadLine();
                command = input.Split(' ');
                if (command.Length > 0)
                {
                    switch (command[0])
                    {
                        case "bad":
                            displayPony(command);
                            break;
                        case "exit":
                            logger.Info("\tWAPI\tExit command entered on CLI");
                            exit = true;
                            break;
                        case "help":
                            displayHelp();
                            break;
                        case "list":
                            displayList(command, game);
                            break;
                        case "web":
                            System.Diagnostics.Process.Start("http://localhost:9090");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("{0} is not a known command. Type help for a list of available commands.", command[0]);
                            continue;
                    } 
                }
                Array.Clear(command, 0, command.Length);
            }
        }

        private static void displayPony(string[] command)
        {
            if (command.Length > 1 && command[1] == "pony" || command[1] == "brony")
            {                
                LoadArt(command[1]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} is not a known command. Type help for a list of available commands.", command[0]);
            }
        }

        private static void LoadArt(string artFile)
        {
            StreamReader fin = null;
            try
            {
                fin = new StreamReader(new FileStream(adminResources + artFile + ".txt", FileMode.Open));
                while (fin.Peek() != -1)
                {
                    Console.WriteLine(fin.ReadLine());
                }
            }
            catch (FileNotFoundException fnfex)
            {
                Console.WriteLine(fnfex.Message);
            }
            catch (DirectoryNotFoundException dnfex)
            {
                Console.WriteLine(dnfex.Message);
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
            finally
            {
                if (fin != null)
                {
                    fin.Close();
                }
            }
        }

        private static void displayHelp()
        {
            Console.WriteLine("The following commands are available within the Operation Bad Pony administration console -\n");
            Console.WriteLine("help:\t\t\tDisplays this list of commands");
            Console.WriteLine("list <object type>:\tDisplay a list of all objects of the specified type");
            Console.WriteLine("exit:\t\t\tShutdown the game server");
            Console.WriteLine();
        }

        private static void displayList(string[] command, Game game)
        {
            if (command.Length < 2)
            {
                Console.Write("What would you like to list? (no or Enter to skip listing): ");
                string listType = Console.ReadLine();
                if (listType == null || listType.ToLower() == "no")
                {
                    return;
                }
                List<String> commands = command.ToList();
                commands.Add(listType);
                command = commands.ToArray();
            }

            if (command[1] == "players")
            {
                Console.WriteLine("ID\tName\t\tUsername\tLocation\n-----------------------------------------------------------");
                List<Player> players = game.GetAllObjects().OfType<Player>().ToList();
                foreach (Player player in players)
                {
                    Console.WriteLine(player.Id + "\t" + player.Name + "\t\t" + player.UserName +"\t\t"+ game.GetObject(player.ContainerId).Name);
                }
            }
            else if (command[1] == "locations")
            {
                Console.WriteLine("ID\tName\t\t\tExits\n-----------------------------------------------------------");
                List<Location> locations = game.GetAllObjects().OfType<Location>().ToList();
                foreach(Location location in locations)
                {
                    string output = location.Id + "\t" + location.Name + "\t\t\t";
                    List<Door> exits = game.GetContainedObjects(location.Id).OfType<Door>().ToList();
                    foreach (var exit in exits)
                    {
                        output += exit.Id + ", ";
                    }
                    output = output.Remove(output.Length - 2);
                    Console.WriteLine(output);
                }
            }
            else if(command[1] == "doors")
            {
                Console.WriteLine("ID\tName\t\t\tLocations\n-----------------------------------------------------------");
                List<Door> doors = game.GetAllObjects().OfType<Door>().ToList();
                foreach (var door in doors)
                {
                    Console.WriteLine(door.Id + "\t" + door.Name + "\t" + game.GetObject(door.DestinationId).Name);                    
                }
            }

            Console.WriteLine("-----------------------------------------------------------");
        }
    }
}
