﻿using System;
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
        private const int TAB = 8;

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
                        case "dt":
                            List<string[]> list = new List<string[]> { new string[] { "12345678", "123", "2312333123123123123213", "test" }, new string[] { "126789", "12312", "312312", "234" }, new string[] { "123", "21312", "12", "234" } };
                            drawTable(new string[4] { "1234", "4325", "2344", "test" }, list);
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
                string[] headers = new string[] { "ID", "Name", "Username", "Location" };              
                List<Player> players = game.GetAllObjects().OfType<Player>().ToList();
                List<string[]> playerData = new List<string[]>();
                foreach (Player player in players)
                {
                    playerData.Add(new string[]{ player.Id.ToString(), player.Name, player.UserName, game.GetObject(player.ContainerId).Name });                    
                }
                drawTable(headers, playerData);
            }
            else if (command[1] == "locations")
            {
                string[] headers = new string[] { "ID", "Name", "Exits" };                
                List<Location> locations = game.GetAllObjects().OfType<Location>().ToList();
                List<string[]> locationData = new List<string[]>();
                foreach (Location location in locations)
                {
                    string output = "";
                    List<Door> exits = game.GetContainedObjects(location.Id).OfType<Door>().ToList();
                    foreach (var exit in exits)
                    {
                        output += exit.Id + ", ";
                    }
                    if (output.Length > 0)
                    {
                        output = output.Remove(output.Length - 2);
                    }
                    locationData.Add(new string[] { location.Id.ToString(), location.Name, output });                    
                }
                drawTable(headers, locationData);
            }
            else if (command[1] == "doors")
            {
                string[] headers = new string[] { "ID", "Name", "Location" };                
                List<Door> doors = game.GetAllObjects().OfType<Door>().ToList();
                List<string[]> doorData = new List<string[]>();
                foreach (var door in doors)
                {
                    doorData.Add(new string[] {door.Id.ToString() , door.Name , game.GetObject(door.DestinationId).Name});
                }
                drawTable(headers, doorData);
            }
            else if (command[1] == "all")
            {
                string[] headers = new string[] { "ID", "Name", "Location" };                
                List<GameObject> all = game.GetAllObjects().ToList();
                List<string[]> allData = new List<string[]>();
                foreach (var item in all)
                {
                    allData.Add(new string[] {item.Id.ToString(), item.Name , game.GetObject(item.ContainerId).Name});
                }
                drawTable(headers, allData);
            }            
        }



        private static void drawTable(string[] headers, List<string[]> lines)
        {
            string heading = "";
            int count = 0;
            int[] lengths = new int[headers.Length];
            string body = "";

            foreach (string header in headers)
            {
                int longest = 1;
                foreach (var line in lines)
                {
                    if (count < line.Length)
                    {
                        var x = line[count].Length / 8;
                        if (x % TAB != 0)
                        {
                            x++;
                        }
                        if (x > longest)
                        {
                            longest = x;
                        }
                    }
                }
                lengths[count] = longest;
                count++;
                heading += header;
                for (int i = 0; i < longest - (header.Length / TAB); i++)
                {
                    heading += "\t";
                }
                
            }
            foreach (var line in lines)
            {
                count = 0;
                foreach (var entry in line)
                {
                    body += entry;
                    for (int i = 0; i < lengths[count] - (entry.Length / TAB); i++)
                    {
                        body += "\t";
                    }
                    count++;
                }
                body += "\n";
            }

            Console.WriteLine(heading);
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine(body);
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

    }
}
