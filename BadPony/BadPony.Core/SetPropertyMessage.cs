﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadPony.Core
{
    public class SetPropertyMessage : IGameMessage
    {
        public int ObjectId { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}
