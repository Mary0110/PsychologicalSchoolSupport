using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Create;
using PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Delete;
using PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Commands.Update;
using PsychologicalSupportPlatform.Meet.Application.ScheduleCells.Queries;

namespace PsychologicalSupportPlatform.Meet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleCellsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ScheduleCellsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> CreateScheduleCell(CreateScheduleCellDTO scheduleCell)
        {
            var createCmdScheduleCell = mapper.Map<AddScheduleCellDTO>(scheduleCell);
            var command = mapper.Map<CreateScheduleCellCommand>(createCmdScheduleCell);
            var response = await mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> DeleteScheduleCell(int id)
        {
            var command = new DeleteScheduleCellCommand(id);
            var response = await mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> UpdateScheduleCell(ScheduleCellDTO scheduleCellDTO)
        {
            var createCmdScheduleCell = mapper.Map<UpdateScheduleCellDTO>(scheduleCellDTO);
            var command = mapper.Map<UpdateScheduleCellCommand>(createCmdScheduleCell);
            var response = await mediator.Send(command);
            
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetAllScheduleCells([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new GetAllScheduleCellsQuery(pageNumber, pageSize);
            var response = await mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("id={id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> GetScheduleCellById(int id)
        {
            var command = new GetScheduleCellByIdQuery() {Id = id};
            var response = await mediator.Send(command);

            return Ok(response);
        }
        
        [HttpGet("day={day}")]
        [Authorize]
        public async Task<IActionResult> GetScheduleCellByDayOfWeek(DayOfWeek day, int pageNumber, int pageSize)
        {
            var command = new GetScheduleCellsByDayOfWeekQuery(day, pageNumber, pageSize);
            var response = await mediator.Send(command);

            return Ok(response);
        }
        
        [HttpGet("active={active}")]
        [Authorize]
        public async Task<IActionResult> GetScheduleCellByStatus(bool active, int pageNumber, int pageSize)
        {
            var command = new GetScheduleCellsByStatusQuery(active, pageNumber, pageSize);
            var response = await mediator.Send(command);

            return Ok(response);
        }
    }
}
