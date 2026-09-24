using JobApplication.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Mediator.Query.GetJobById
{
    public class GetJobByIdQuery:IRequest<JobDTO?>
    {
        public int JobId { get; set; }
    }
}
