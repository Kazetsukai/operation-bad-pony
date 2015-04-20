using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadPony.Core
{
    public class Door : GameObject
    {
        public Door()
        {
            EnterDescription = "{player} comes in through {name}";
            ExitDescription = "{player} leaves through {name}";
        }

        public int DestinationId { get; set; }

        public string EnterDescription { get; set; }
        public string ExitDescription { get; set; }

        public override GameObjectType Type
        {
            get { return GameObjectType.Door; }
        }
    }
}
