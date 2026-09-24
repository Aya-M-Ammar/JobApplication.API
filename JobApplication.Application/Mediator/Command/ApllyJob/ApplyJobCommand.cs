using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Mediator.Command.ApllyJob
{
    public class ApplyJobCommand:IRequest<bool>
    {
        public int JobId { get; set; }
        public int requesterId { get; set; }
    }
}
