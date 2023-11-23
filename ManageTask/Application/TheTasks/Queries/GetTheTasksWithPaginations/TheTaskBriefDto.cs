﻿using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TheTasks.Queries.GetTheTasksWithPaginations
{
    public class TheTaskBriefDto :IMapFrom<TheTask>
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public string Description { get; set; }

    }
}
