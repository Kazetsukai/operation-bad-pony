using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadPony.Core
{
    public class Item : GameObject
    {
        public override GameObjectType Type
        {
            get { return GameObjectType.Item; }
        }
    }
}
