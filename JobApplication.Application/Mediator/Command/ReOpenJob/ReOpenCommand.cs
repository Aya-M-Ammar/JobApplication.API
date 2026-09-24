using JobApplication.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Mediator.Command.ReOpenJob
{
    public class ReOpenCommand:IRequest<JobDTO?>
    {
        public int Id  { get; set; }
      
    }
}
