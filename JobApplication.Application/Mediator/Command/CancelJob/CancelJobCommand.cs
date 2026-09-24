using MediatR;

namespace JobApplication.Application.Mediator.Command.CancelJob
{
    public class CancelJobCommand : IRequest<bool>
    {
        public int JobId { get; set; }
    }
}