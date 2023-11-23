using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TheTasks.Queries.NewFolder
{
    public record GetTheTaskQuery : IRequest<TheTask>
    {
        public int Id { get; set; }
    }
    public class GetTheTaskQueryHandler : IRequestHandler<GetTheTaskQuery, TheTask>
    {
        private readonly IApplicationDbContext context;

        public GetTheTaskQueryHandler(IApplicationDbContext _context)
        {
            context = _context; 
        }
        public async Task<TheTask> Handle(GetTheTaskQuery request, CancellationToken cancellationToken)
        {
            var theTask = await context.Tasks.FindAsync(request.Id);

             if (theTask == null) 
             {  
                throw new NotFoundException("TheTask", request.Id);
            }

                return theTask;
        }
    }
}
