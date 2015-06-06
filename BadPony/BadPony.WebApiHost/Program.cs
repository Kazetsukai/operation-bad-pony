using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using BadPony.Core;
using Microsoft.Owin.Hosting;
using NLog;

namespace BadPony.WebApiHost
{
    public class Program
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public static bool running = true;
        public static Game Game;
        public static int UpTime;

        public static void Main(string[] args)
        {
            logger.Info("\tWAPI\tWebApiHost started");
            Game = new Game();

            var url = "http://localhost:9090";
            WebApp.Start<Startup>(url);
            Console.WriteLine("WebApi self-host running at " + url + "...");
            logger.Info("\tWAPI\tStarting CLI");
            
            Thread schedThread = new Thread(new ThreadStart(Scheduler.Start));            
            Thread cliThread = new Thread(new ThreadStart(AdminCLI.Start));            
            
            cliThread.Start();
            schedThread.Start();
            logger.Info("\tWAPI\tGame over!!! - Please insert 50c to play again.");
        }   
    }
}
