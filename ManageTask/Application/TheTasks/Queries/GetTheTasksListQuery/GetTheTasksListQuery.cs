using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TheTasks.Queries.GetTheTasksListQuery
{
    public record GetTheTasksListQuery : IRequest<List<TheTask>>
    {
    }

    public class GetTheTasksListQueryHandler : IRequestHandler<GetTheTasksListQuery, List<TheTask>>
    {
        private readonly IApplicationDbContext _context;

        public GetTheTasksListQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TheTask>> Handle(GetTheTasksListQuery request, CancellationToken cancellationToken)
        {
            var theTask =await _context.Tasks.ToListAsync(cancellationToken);
            return theTask; 
        }
    }
}
