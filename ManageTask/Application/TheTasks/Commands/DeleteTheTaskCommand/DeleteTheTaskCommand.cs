using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Commands.DeleteTaskCommand
{

    public record DeleteTheTaskCommand : IRequest<TheTask>
    {
        public int Id { get; set; }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTheTaskCommand, TheTask>
    {
        private readonly IApplicationDbContext context;

        public DeleteTaskCommandHandler(IApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<TheTask> Handle(DeleteTheTaskCommand request, CancellationToken cancellationToken)
        {
            var theTask = await context.Tasks.FindAsync(request.Id);
            if (theTask == null)
            {
                throw new NotFoundException("TheTask", request.Id);
            }

            context.Tasks.Remove(theTask);
           await context.SaveChangesAsync(cancellationToken);
            return theTask;
        }
    }
}
