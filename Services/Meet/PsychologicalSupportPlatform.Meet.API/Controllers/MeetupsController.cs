using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Exceptions;
using PsychologicalSupportPlatform.Meet.Application.Meetup.Queries;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Create;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Delete;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

namespace PsychologicalSupportPlatform.Meet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public MeetupsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        
        [HttpPost("Create")]
        public async Task<IActionResult> CreateMeetup(AddMeetupDTO meetup)
        {
            try
            {
                var command = mapper.Map<CreateMeetupCommand>(meetup);
                int response = await mediator.Send(command);
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteMeetup(int id)
        {
            try
            {
                var command = new DeleteMeetupCommand(id);
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
        public async Task<IActionResult> UpdateMeetup(MeetupDTO meetup)
        {
            try
            {
                var command = mapper.Map<UpdateMeetupCommand>(meetup);
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

        [HttpGet("GetAllMeetups")]
        public async Task<IActionResult> GetAllMeetups()
        {
            try
            {
                var command = new GetAllMeetupsQuery();
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

        [HttpGet("GetMeetupById")]
        public async Task<IActionResult> GetMeetupById(int id)
        {
            try
            {
                var command = new GetMeetupByIdQuery() { Id = id};
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
