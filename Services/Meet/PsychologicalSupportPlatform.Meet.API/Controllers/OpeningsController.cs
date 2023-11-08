using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Create;
using PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Delete;
using PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Update;
using PsychologicalSupportPlatform.Meet.Application.Openings.Queries;

namespace PsychologicalSupportPlatform.Meet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpeningsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public OpeningsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        
        [HttpPost]
        // [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> CreateOpening(AddOpeningDTO opening)
        {
            var createCmdOpening = mapper.Map<AddCmdOpeningDTO>(opening);
            var command = mapper.Map<CreateOpeningCommand>(createCmdOpening);
            var response = await mediator.Send(command);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
        
            return Ok(response.Message);
        }

        [HttpDelete]
        // [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> DeleteOpening(int id)
        {
            var command = new DeleteOpeningCommand(id);
            var response = await mediator.Send(command);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
        
            return Ok(response.Message);
        }

        [HttpPut]
        // [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> UpdateOpening(OpeningDTO openingDTO)
        {
            var createCmdOpening = mapper.Map<CmdOpeningDTO>(openingDTO);
            var command = mapper.Map<UpdateOpeningCommand>(createCmdOpening);
            var response = await mediator.Send(command);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
        
            return Ok(response.Message);
        }

        [HttpGet]
        // [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetAllOpenings()
        {
            var command = new GetAllOpeningsQuery();
            var response = await mediator.Send(command);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }
        
            return Ok(response.Data);
        }

        [HttpGet("id={id}")]
        // [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> GetOpeningById(int id)
        {
            var command = new GetOpeningByIdQuery() { Id = id};
            var response = await mediator.Send(command);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            
            return Ok(response.Data);
        }
        
        [HttpGet("day={day}")]
        // [Authorize]
        public async Task<IActionResult> GetOpeningByDayOfWeek(DayOfWeek day)
        {
            var command = new GetOpeningsByDayOfWeekQuery() { DayOfWeek = day};
            var response = await mediator.Send(command);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            
            return Ok(response.Data);
        }
        
        [HttpGet("active={active}")]
        // [Authorize]
        public async Task<IActionResult> GetOpeningByStatus(bool active)
        {
            var command = new GetOpeningsByStatusQuery() { Active = active};
            var response = await mediator.Send(command);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            
            return Ok(response.Data);
        }
    }
}
