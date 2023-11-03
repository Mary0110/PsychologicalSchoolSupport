using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Application.Opening.Queries;
using PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Create;
using PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Delete;
using PsychologicalSupportPlatform.Meet.Application.Openings.Commands.Update;

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
        
        [HttpPost("Create")]
        public async Task<IActionResult> CreateOpening(AddOpeningDTO opening)
        {
            try
            {
                var command = mapper.Map<CreateOpeningCommand>(opening);
                int response = await mediator.Send(command);
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteOpening(int id)
        {
            try
            {
                var command = new DeleteOpeningCommand(id);
                await mediator.Send(command);
                
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOpening(OpeningDTO eventUpdate)
        {
            try
            {
                var command = mapper.Map<UpdateOpeningCommand>(eventUpdate);
                await mediator.Send(command);
                
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllOpenings")]
        public async Task<IActionResult> GetAllOpenings()
        {
            try
            {
                var command = new GetAllOpeningsQuery();
                var response = await mediator.Send(command);

                if (response is null)
                {
                    return NotFound();
                }
                
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOpeningById")]
        public async Task<IActionResult> GetOpeningById(int id)
        {
            try
            {
                var command = new GetOpeningByIdQuery() { Id = id};
                var response = await mediator.Send(command);
                
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
