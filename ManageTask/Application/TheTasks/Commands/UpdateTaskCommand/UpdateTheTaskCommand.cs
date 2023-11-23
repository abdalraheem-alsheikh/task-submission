using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TheTasks.Commands.UpdateTaskCommand
{
    public class UpdateTheTaskCommand:IRequest<TheTask>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }

    public class UpdateTheTaskCommandHandler : IRequestHandler<UpdateTheTaskCommand, TheTask>
    {
        private readonly IApplicationDbContext dbContext;

        public UpdateTheTaskCommandHandler(IApplicationDbContext _dbContext)
        {
                dbContext = _dbContext;
        }
        public async Task<TheTask> Handle(UpdateTheTaskCommand request, CancellationToken cancellationToken)
        {
            var theTask = await dbContext.Tasks.FindAsync(request.Id);
                if (theTask == null)
                {
                    throw new NotFoundException("TheTask", request.Id);
                 }

                theTask.Name = request.Name;
                theTask.Description = request.Description;
                dbContext.Tasks.Update(theTask); 
                await dbContext.SaveChangesAsync(cancellationToken);
                return theTask;
        }
    }
}
