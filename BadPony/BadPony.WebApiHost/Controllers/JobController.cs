using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BadPony.Core;
using NLog;

namespace BadPony.WebApiHost.Controllers
{
    [Authorize]
    public class JobController : ApiController
    {
        public Job Get(int id)
        {
            return (Job)Program.Game.GetObject(id);
        }

        public bool Post(DoJobMessage message)
        {
            bool startIncrements = false;
            if(Program.Game.GetPlayerById(message.PlayerId).ActionPoints == Game.DailyAP)
            {
                startIncrements = true;
            }
            if (Program.Game.PostMessage(message))
            {
                if (startIncrements)
                {
                    IncrementAPMessage msg = new IncrementAPMessage { PlayerID = message.PlayerId, Time = Program.UpTime + 60 };
                    Scheduler.AddScheduledMessage(msg, msg.Time);
                }
                return true;
            }
            return false;
        }
    }
}
