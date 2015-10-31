using BadPony.Core;
using System.Collections.Generic;

namespace BadPony.WebApiHost
{
    public class ScheduledMessageList
    {        
        public int ScheduledTime {get; set;}
        public List<IGameMessage> ScheduledMessages = new List<IGameMessage>();

        public ScheduledMessageList(int time, IGameMessage message)
        {
            ScheduledTime = time;
            ScheduledMessages.Add(message);
        }
    }
}
