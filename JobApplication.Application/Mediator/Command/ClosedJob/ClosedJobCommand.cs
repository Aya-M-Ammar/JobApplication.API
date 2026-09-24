using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Mediator.Command.ClosedJob
{
    public class ClosedJobCommand:IRequest<bool>
    {
        public int JobId { get; set; }
    }
}
