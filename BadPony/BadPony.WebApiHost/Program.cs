using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BadPony.Core;
using Microsoft.Owin.Hosting;
using NLog;

namespace BadPony.WebApiHost
{
    public class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public static Game Game;

        public static void Main(string[] args)
        {
            logger.Info("\tWAPI\tWebApiHost started");
            Game = new Game();

            var url = "http://+:9090";
            WebApp.Start<Startup>(url);
            Console.WriteLine("WebApi self-host running at " + url + "...");
            logger.Info("\tWAPI\tStarting CLI");
            AdminCLI cli = new AdminCLI();
            cli.StartCLI(Game);
            logger.Info("\tWAPI\tGame over!!! - Please insert 50c to play again.");
        }   
    }
}
