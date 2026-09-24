using JobApplication.Application.Mediator.Command.ApllyJob;
using JobApplication.Application.Services.ServiceAbstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IMediator _mediator;

        public ApplicationController(IApplicationService applicationService, IMediator mediator)
        {
            this._applicationService = applicationService;
            this._mediator = mediator;
        }
       
       
        [HttpPost("{jobId}")]
        public async Task<IActionResult> Apply(int jobId,int candidateid)
        {
            // ti use authorize
           // var requesterID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
           // var result = await _applicationService.Apply(jobId, requesterId);
           var result=await _mediator.Send(new ApplyJobCommand() { JobId=jobId,requesterId= candidateid });
            if (!result) return BadRequest("Cannot apply for this job.");
            return Ok("Application submitted successfully.");
        }
    }
}
