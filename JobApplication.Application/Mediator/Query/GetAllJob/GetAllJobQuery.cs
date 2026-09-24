using JobApplication.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Mediator.Query.GetAllJob
{
    public class GetAllJobQuery:IRequest<IEnumerable<JobDTO>>
    {
    }
}
