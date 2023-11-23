using Application.Common.Models;
using Application.Tasks.Commands.CreateTaskCommand;
using Application.Tasks.Commands.DeleteTaskCommand;
using Application.TheTasks.Commands.UpdateTaskCommand;
using Application.TheTasks.Queries.GetTheTasksListQuery;
using Application.TheTasks.Queries.GetTheTasksWithPaginations;
using Application.TheTasks.Queries.NewFolder;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [ApiController]
        [Route("api/[controller]")]
        public class TheTaskController : ApiControllerBase
        {
            private readonly IMediator _mediator;

            public TheTaskController(IMediator mediator)
            {
                _mediator = mediator;
            }



            [HttpGet("paged")]
            public async Task<ActionResult<PaginatedList<TheTaskBriefDto>>> GetTheTaskWithPagination([FromQuery] GetTheTaskWithPagination query)
            {
                return await Mediator.Send(query);
            }

            [HttpGet("list")]
            public async Task<ActionResult<List<TheTask>>> GetTheTasks()
            {
              var query =   new GetTheTasksListQuery();
              var theTasks = await _mediator.Send(query);

                return Ok(theTasks);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<TheTask>> GetTheTask(int id)
            {
                var query = new GetTheTaskQuery { Id = id };
                var theTask = await _mediator.Send(query);
                if (theTask == null)
                {
                    return NotFound();
                }
                return theTask;
            }


            [HttpPost]
            public async Task<ActionResult<int>> CreateTheTask(CreateTheTaskCommand command)
            {
                var theTaskId = await _mediator.Send(command);
                return theTaskId;
            }


            [HttpPut("{id}")]
            public async Task<ActionResult<TheTask>> UpdateCourse(int id, UpdateTheTaskCommand command)
            {
                if (id != command.Id)
                {
                    return BadRequest();
                }

                var updatedTheTask = await _mediator.Send(command);
                return updatedTheTask;
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCourse(int id)
            {
                var query = new DeleteTheTaskCommand { Id = id };
                var deletedTheTask = await _mediator.Send(query);
                if (deletedTheTask == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
    }
    }
