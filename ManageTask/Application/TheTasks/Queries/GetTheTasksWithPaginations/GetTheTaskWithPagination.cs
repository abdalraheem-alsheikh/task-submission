using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TheTasks.Queries.GetTheTasksWithPaginations
{
    public class GetTheTaskWithPagination : IRequest<PaginatedList<TheTaskBriefDto>>
    {
        public string Name { get; set; } = null!;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }

    public class GettheTaskWithPaginationHandler : IRequestHandler<GetTheTaskWithPagination, PaginatedList<TheTaskBriefDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GettheTaskWithPaginationHandler(IApplicationDbContext _dbContext, IMapper _mapper)
        {
            context = _dbContext;
            mapper = _mapper;
        }

        public async Task<PaginatedList<TheTaskBriefDto>> Handle(GetTheTaskWithPagination request, CancellationToken cancellationToken)
        {
            return await context.Tasks
              .Where(x => !string.IsNullOrEmpty(request.Name) ? x.Name == request.Name : true)
              .OrderBy(x => x.Name)
              .ProjectTo<TheTaskBriefDto>(mapper.ConfigurationProvider)
              .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
