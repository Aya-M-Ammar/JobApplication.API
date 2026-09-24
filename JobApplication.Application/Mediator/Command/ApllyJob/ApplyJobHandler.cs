using JobApplication.Domain;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Entities.Enums;
using JobApplication.Infrastracture.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Mediator.Command.ApllyJob
{
    public class ApplyJobHandler : IRequestHandler<ApplyJobCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplyJobHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(ApplyJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _unitOfWork.GetRepository<Job>().GetByIdAsync(request.JobId);

            if (job == null)
                return false;

            if (!job.IsActive)
                return false;

            var application = new JobCandidateApplication
            {
                JobId = request.JobId,
                CandidateId = request.requesterId,
                AppliedAt = DateTime.UtcNow,
                JobApplicationStatus = JobApplicationStatus.Applied,
                StatusUpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<JobCandidateApplication>().AddAsync(application);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
