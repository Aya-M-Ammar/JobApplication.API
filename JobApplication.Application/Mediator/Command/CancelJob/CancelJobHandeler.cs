using JobApplication.Domain;
using JobApplication.Infrastracture.Contract;
using MediatR;

namespace JobApplication.Application.Mediator.Command.CancelJob
{
    public class CancelJobHandler : IRequestHandler<CancelJobCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelJobHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(
            CancelJobCommand request,
            CancellationToken cancellationToken)
        {
            var job = await _unitOfWork
                .GetRepository<Job>()
                .GetByIdAsync(request.JobId);

            if (job == null)
                return false;

            job.IsActive = false;

            _unitOfWork.GetRepository<Job>().Update(job);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}