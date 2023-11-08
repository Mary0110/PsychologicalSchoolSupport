using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Delete;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetup;
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
        
        [HttpPost("order/psychologist")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> OrderMeetupByPsychologist(AddMeetupDTO meetup)
        {
            var command = mapper.Map<OrderMeetupCommand>(meetup);
            var response = await mediator.Send(command);
            
            return Ok(response);
        }
        
        [HttpPost("order/student")]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> OrderMeetupByStudent(AddMeetupByStudentDTO meetup)
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            var curUserIdStr = user?.FindFirst(ClaimTypes.NameIdentifier).Value;
            bool success = int.TryParse(curUserIdStr, out int curUserId);

            if (!success)
            {
                return BadRequest();
            }
            
            var addMeetupDTO = new AddMeetupDTO(meetup.Date, meetup.OpeningId, curUserId);
            var command = mapper.Map<OrderMeetupCommand>(addMeetupDTO);
            var response = await mediator.Send(command);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> DeleteMeetup(int id)
        {

                var command = new DeleteMeetupCommand(id);
                var response = await mediator.Send(command);
                
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                
                return Ok();
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> UpdateMeetup(MeetupDTO meetup)
        {
            
            var command = mapper.Map<UpdateMeetupCommand>(meetup);
            var response = await mediator.Send(command);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            
            return Ok(response.Message);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetAllMeetups()
        {
            var command = new GetAllMeetupsQuery();
            var response = await mediator.Send(command);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }
        
            return Ok(response.Data);
        }

        [HttpGet("id={id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetMeetupById(int id)
        {
            var command = new GetMeetupByIdQuery() { Id = id};
            var response = await mediator.Send(command);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
        
            return Ok(response.Data);
        }
        
        [HttpGet("date={date}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetMeetupByDate(DateOnly date)
        {
            var command = new GetMeetupsByDateQuery(){ Date = date};
            var response = await mediator.Send(command);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
        
            return Ok(response.Data);
        }
        
        [HttpGet("student={studentId}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetMeetupByStudentId(int studentId)
        {
            var command = new GetMeetupsByStudentIdQuery(){ StudentId = studentId};
            var response = await mediator.Send(command);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
        
            return Ok(response.Data);
        }
    }
}
