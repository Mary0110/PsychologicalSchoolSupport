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
        public async Task<IActionResult> CreateScheduleCell(AddScheduleCellDTO scheduleCell)
        {
            var createCmdScheduleCell = mapper.Map<AddCmdScheduleCellDTO>(scheduleCell);
            var command = mapper.Map<CreateScheduleCellCommand>(createCmdScheduleCell);
            var response = await mediator.Send(command);

            return Ok(response.Message);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> DeleteScheduleCell(int id)
        {
            var command = new DeleteScheduleCellCommand(id);
            var response = await mediator.Send(command);

            return Ok(response.Message);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> UpdateScheduleCell(ScheduleCellDTO scheduleCellDTO)
        {
            var createCmdScheduleCell = mapper.Map<CmdScheduleCellDTO>(scheduleCellDTO);
            var command = mapper.Map<UpdateScheduleCellCommand>(createCmdScheduleCell);
            var response = await mediator.Send(command);
            
            return Ok(response.Message);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetAllScheduleCells()
        {
            var command = new GetAllScheduleCellsQuery();
            var response = await mediator.Send(command);

            return Ok(response.Data);
        }

        [HttpGet("id={id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> GetScheduleCellById(int id)
        {
            var command = new GetScheduleCellByIdQuery() {Id = id};
            var response = await mediator.Send(command);

            return Ok(response.Data);
        }
        
        [HttpGet("day={day}")]
        [Authorize]
        public async Task<IActionResult> GetScheduleCellByDayOfWeek(DayOfWeek day)
        {
            var command = new GetScheduleCellsByDayOfWeekQuery() {DayOfWeek = day};
            var response = await mediator.Send(command);

            return Ok(response.Data);
        }
        
        [HttpGet("active={active}")]
        [Authorize]
        public async Task<IActionResult> GetScheduleCellByStatus(bool active)
        {
            var command = new GetScheduleCellsByStatusQuery() {Active = active};
            var response = await mediator.Send(command);

            return Ok(response.Data);
        }
    }
}
