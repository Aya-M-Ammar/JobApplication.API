using JobApplication.Domain;
using JobApplication.Infrastracture.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Mediator.Command.ClosedJob
{
    public class ClosedJobHandler : IRequestHandler<ClosedJobCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClosedJobHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(ClosedJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _unitOfWork.GetRepository<Job>().GetByIdAsync(request.JobId);
            if (job == null) return false;

            if (job.IsActive == false) return false;

            job.IsActive = false;
            job.ClosedAt = DateTime.Now;

            _unitOfWork.GetRepository<Job>().Update(job);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
