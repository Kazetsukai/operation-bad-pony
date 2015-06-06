using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadPony.Core
{
    public class TimerMessage : IGameMessage
    {
        public DateTime time { get; set; }
    }
}
