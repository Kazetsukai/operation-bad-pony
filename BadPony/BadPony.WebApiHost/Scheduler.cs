using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace BadPony.WebApiHost
{
    public class Scheduler
    {
        public static void Tick()
        {
            int upTime = 0;
            Console.WriteLine("Scheduler started");
            while (BadPony.WebApiHost.Program.running)
            {
                upTime++;
                if (upTime % 60 == 0)
                {
                    Console.WriteLine("Uptime {0} minutes", upTime/60);
                    BadPony.WebApiHost.Program.Game.IncrementAP();
                }                
                Thread.Sleep(1000);
            }
        }
    }
}
