using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychologicalSupportPlatform.Common;
using PsychologicalSupportPlatform.Meet.API.Extensions;
using PsychologicalSupportPlatform.Meet.Application.DTOs;
using PsychologicalSupportPlatform.Meet.Application.DTOs.Meetup;
using PsychologicalSupportPlatform.Meet.Application.Meetups.Commands.Approve;
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
            var userId = User.GetLoggedInUserId();
            var addMeetupDTO = new AddMeetupDTO(meetup.Date, meetup.ScheduleCellId, int.Parse(userId));
            var command = mapper.Map<OrderMeetupCommand>(addMeetupDTO);
            var response = await mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> DeleteMeetup(int id)
        {
            var command = new DeleteMeetupCommand(id);
            var response = await mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist)]
        public async Task<IActionResult> UpdateMeetup(MeetupDTO meetup)
        {
            var command = mapper.Map<UpdateMeetupCommand>(meetup);
            var response = await mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetAllMeetups([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new GetAllMeetupsQuery(pageNumber, pageSize);
            var response = await mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("id={id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetMeetupById(int id)
        {
            var command = new GetMeetupByIdQuery() {Id = id};
            var response = await mediator.Send(command);

            return Ok(response);
        }
        
        [HttpGet("date={date}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetMeetupByDate(DateOnly date, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new GetMeetupsByDateQuery(date, pageNumber, pageSize);
            var response = await mediator.Send(command);

            return Ok(response);
        }
        
        [HttpGet("student={studentId}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Psychologist + "," + Roles.Manager)]
        public async Task<IActionResult> GetMeetupByStudentId(int studentId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var command = new GetMeetupsByStudentIdQuery(studentId, pageNumber, pageSize);
            var response = await mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("approve/meetup_id={meetupId}")]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> ApproveMeetupByStudent(int meetupId)
        {
            var userId = User.GetLoggedInUserId();
            var command = new ApproveMeetupByStudentCommand(new ApproveMeetupDTO(int.Parse(userId), meetupId));
            var response = await mediator.Send(command);

            return Ok(response);
        }
    }
}
