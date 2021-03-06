﻿using BadPony.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Program.UpTime++;
                var currentTimeMessageList = ScheduledMessages.FirstOrDefault(sm => sm.ScheduledTime == Program.UpTime) ;
                if (currentTimeMessageList != null)
                {
                    foreach (var message in currentTimeMessageList.ScheduledMessages)
                    {
                        var result = Program.Game.PostMessage(message);
                        if (message is IncrementAPMessage && result)
                        {
                            IncrementAPMessage msg = (IncrementAPMessage)message;
                            msg.Time += 60;
                            AddScheduledMessage(msg, msg.Time);
                            //var scheduleNextIncrementTime = ScheduledMessages.FirstOrDefault(sm => sm.ScheduledTime == msg.Time);
                            //if (scheduleNextIncrementTime == null)
                            //{
                            //    ScheduledMessages.Add(new ScheduledMessageList(msg.Time, msg));
                            //}
                            //else
                            //{
                            //    scheduleNextIncrementTime.ScheduledMessages.Add(msg);
                            //}
                        }
                    }
                }                             
                Thread.Sleep(1000);
            }
            SchedulerStarted = false;
        }

        public static void AddScheduledMessage(IGameMessage msg, int time)
        {
            var scheduledTimeList = ScheduledMessages.FirstOrDefault(sm => sm.ScheduledTime == time);
            if(scheduledTimeList == null)
            {
                ScheduledMessages.Add(new ScheduledMessageList(time, msg));
            }
            else
            {
                scheduledTimeList.ScheduledMessages.Add(msg);
            }
        }
    }
}