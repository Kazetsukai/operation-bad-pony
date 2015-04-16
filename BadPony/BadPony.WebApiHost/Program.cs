using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BadPony.Core;
using Microsoft.Owin.Hosting;

namespace BadPony.WebApiHost
{
    public class Program
    {
        public static Game Game;

        public static void Main(string[] args)
        {
            Game = new Game();

            var url = "http://localhost:9090";
            WebApp.Start<Startup>(url);
            Console.WriteLine("WebApi self-host running at " + url + "...");
            Console.ReadLine();
        }

         
    }
}
