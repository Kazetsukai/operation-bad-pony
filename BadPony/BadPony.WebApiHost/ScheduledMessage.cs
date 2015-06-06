using BadPony.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadPony.WebApiHost
{
    public class ScheduledMessageList
    {
        public List<IGameMessage> ScheduledMessages = new List<IGameMessage>();
        public int ScheduledTime {get; set;}

    }
}
