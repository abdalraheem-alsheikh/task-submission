using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class LookupDto : IMapFrom<TodoList>, IMapFrom<TodoItem>
    {
        public int Id { get; init; }

        public string? Title { get; init; }
    }
}
