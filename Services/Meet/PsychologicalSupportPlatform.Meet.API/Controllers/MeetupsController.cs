using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.API.Extensions;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Approve;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Delete;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetup;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.OrderMeetupByPsychologist;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Update;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Queries;

namespace PsychologicalSupportPlatform.Meet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MeetupsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpPost("psychologist/meetup-ordering")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> OrderMeetupByPsychologist([FromBody] AddMeetupDTO meetup)
        {
            var userId = User.GetLoggedInUserId();
            var command = new OrderMeetupByPsychologistCommand(meetup, userId);
            var response = await _mediator.Send(command);
            
            return Ok(response);
        }
        
        [HttpPost("student/meetup-ordering")]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> OrderMeetupByStudent([FromBody] AddMeetupByStudentDTO meetup)
        {
            var userId = User.GetLoggedInUserId();
            var addMeetupDTO = new AddMeetupDTO(meetup.Date, meetup.ScheduleCellId, int.Parse(userId));
            var command = _mapper.Map<OrderMeetupCommand>(addMeetupDTO);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> DeleteMeetup([FromRoute] int id)
        {
            var command = new DeleteMeetupCommand(id);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> UpdateMeetup([FromRoute] int id, AddMeetupDTO meetup)
        {
            var command = new UpdateMeetupCommand(id, meetup);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetAllMeetups([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new GetAllMeetupsQuery(pageNumber, pageSize);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetMeetupById(int id)
        {
            var command = new GetMeetupByIdQuery() {Id = id};
            var response = await _mediator.Send(command);

            return Ok(response);
        }
        
        [HttpGet("dates/{date}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetMeetupByDate([FromRoute] DateOnly date, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new GetMeetupsByDateQuery(date, pageNumber, pageSize);
            var response = await _mediator.Send(command);

            return Ok(response);
        }
        
        [HttpGet("students/{studentId}/meetups")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetMeetupByStudentId([FromRoute] int studentId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new GetMeetupsByStudentIdQuery(studentId, pageNumber, pageSize);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("student/meetup-approval/{meetupId}")]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> ApproveMeetupByStudent(int meetupId)
        {
            var userId = User.GetLoggedInUserId();
            var command = new ApproveMeetupByStudentCommand(new ApproveMeetupDTO(int.Parse(userId), meetupId));
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
