using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs.ScheduleCell;
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
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ScheduleCellsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> CreateScheduleCell(CreateScheduleCellDTO scheduleCell)
        {
            var command = _mapper.Map<CreateScheduleCellCommand>(scheduleCell);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> DeleteScheduleCell([FromRoute] int id)
        {
            var command = new DeleteScheduleCellCommand(id);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> UpdateScheduleCell([FromRoute] int id, AddScheduleCellDTO scheduleCellDTO)
        {
            var command = new UpdateScheduleCellCommand(id, scheduleCellDTO);
            var response = await _mediator.Send(command);
            
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetAllScheduleCells([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new GetAllScheduleCellsQuery(pageNumber, pageSize);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> GetScheduleCellById([FromRoute] int id)
        {
            var command = new GetScheduleCellByIdQuery() {Id = id};
            var response = await _mediator.Send(command);

            return Ok(response);
        }
        
        [HttpGet("days/{day}")]
        [Authorize]
        public async Task<IActionResult> GetScheduleCellsByDayOfWeek([FromRoute] DayOfWeek day, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new GetScheduleCellsByDayOfWeekQuery(day, pageNumber, pageSize);
            var response = await _mediator.Send(command);

            return Ok(response);
        }
        
        [HttpGet("status/{active}")]
        [Authorize]
        public async Task<IActionResult> GetScheduleCellByStatus([FromRoute] bool active,
            [FromQuery] int pageNumber,  [FromQuery] int pageSize)
        {
            var command = new GetScheduleCellsByStatusQuery(active, pageNumber, pageSize);
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
