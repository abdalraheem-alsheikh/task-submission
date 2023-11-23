using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Tasks.Commands.CreateTaskCommand
{
    public record CreateTheTaskCommand : IRequest<int>
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        
    }

    public class CreateTheTaskCommandHandler : IRequestHandler<CreateTheTaskCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateTheTaskCommandHandler(IApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<int> Handle(CreateTheTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TheTask
            {
                Name = request.Name,
                Description = request.Description,
                
            };

           await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }
}
