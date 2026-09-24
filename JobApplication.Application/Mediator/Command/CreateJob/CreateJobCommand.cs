using JobApplication.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Mediator.Command.CreateJob
{
    public class CreateJobCommand:IRequest<JobDTO?>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; } = default!;
    }
}
