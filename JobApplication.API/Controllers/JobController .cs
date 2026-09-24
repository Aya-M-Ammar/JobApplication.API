
using JobApplication.Application.DTOs;
using JobApplication.Application.Mediator.Command.CancelJob;
using JobApplication.Application.Mediator.Command.ClosedJob;
using JobApplication.Application.Mediator.Command.CreateJob;
using JobApplication.Application.Mediator.Command.ReOpenJob;
using JobApplication.Application.Mediator.Query.GetAllJob;
using JobApplication.Application.Mediator.Query.GetJobById;
using JobApplication.Application.Services.ServiceAbstraction;
using JobApplication.Application.Services.ServiceImplimentation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMediator _mediator;

        public JobController(IJobService jobService,IMediator mediator)
        {
            _jobService = jobService;
            this._mediator = mediator;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<JobDTO>>> GetAllJobs()
        {
            //var jobs = await _jobService.GetAllJobsAsync();
            var jobs = await _mediator.Send(new GetAllJobQuery());
            return Ok(jobs);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<JobDTO>> GetJobById(int id)
        {
            //var job = await _jobService.GetJobByIdAsync(id);
            var job = _mediator.Send(new GetJobByIdQuery() { JobId=id});
            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);

        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateJob(JobDTO Createjob)
        {
            //var Result = await _jobService.CreateJobAsync(Createjob);
            var Result = await _mediator.Send(new CreateJobCommand() { Description= Createjob.Description,Title= Createjob.Title,IsActive= Createjob.IsActive });

            return Ok(Result);


        }
        [HttpPut("close/{id}")]
        public async Task<ActionResult<bool>> CloseJob(int id)
        {
            // var Result = await _jobService.ClosedJob(id);
            var Result = await _mediator.Send(new ClosedJobCommand() { JobId = id });

            return Ok(Result);


        }
        [HttpPut("ReOpen/{id}")]

        public async Task<ActionResult<bool>> ReopenJob(int id)
        {
           // var Result = await _jobService.ReOpenJob(id);

            var Result = await _mediator.Send(new ReOpenCommand() {Id=id });

            return Ok(Result);


        }


        [HttpPut("cancel/{id}")]

        public async Task<IActionResult> Cancel(int id)
        {
            var requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            //var result = await _jobService.CancelJob(id);
            var Result =await _mediator.Send(new CancelJobCommand(){ JobId = id });

            if (!Result)
                return BadRequest("You cannot cancel this application.");

            return Ok("Application cancelled successfully.");
        }


    }
}
