﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TheTask : BaseAuditableEntity
    {
        
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } =string.Empty;
    }
}
