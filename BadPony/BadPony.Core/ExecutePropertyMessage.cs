using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadPony.Core
{
    public class ExecutePropertyMessage : IGameMessage
    {
        public string PropertyName { get; set; }
        public int ObjectId { get; set; }
    }
}
