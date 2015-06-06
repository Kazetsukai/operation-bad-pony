using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BadPony.WebApiHost
{
    public class Scheduler
    {
        public static bool SchedulerStarted = false;
        public static List<ScheduledMessageList> ScheduledMessages;

        public static void Start()
        {
            Program.UpTime = 0;
            ScheduledMessages = new List<ScheduledMessageList>();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Scheduler started");
            Console.ResetColor();
            SchedulerStarted = true;
            while (Program.running)
            {
                if (ScheduledMessages.FirstOrDefault(sm => sm.ScheduledTime == Program.UpTime) != null)
                {

                }
                Program.UpTime++;
                if (Program.UpTime % 60 == 0)
                {                    
                    BadPony.WebApiHost.Program.Game.IncrementAP();
                }                
                Thread.Sleep(1000);
            }
            SchedulerStarted = false;
        }
    }
}