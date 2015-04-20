using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadPony.Core
{
    public class Location : GameObject
    {
        public override GameObjectType Type
        {
            get { return GameObjectType.Location; }
        }
    }
}
