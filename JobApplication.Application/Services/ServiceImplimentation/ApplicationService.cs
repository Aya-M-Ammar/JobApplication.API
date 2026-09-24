using JobApplication.Application.Services.ServiceAbstraction;
using JobApplication.Domain;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Entities.Enums;
using JobApplication.Infrastracture.Contract;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace JobApplication.Application.Services.ServiceImplimentation
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<bool> Apply(int JobID, int requesterId)
        {
            var job = await _unitOfWork.GetRepository<Job>().GetByIdAsync(JobID);

            if (job == null)
                return false;

            if (!job.IsActive)
                return false;

            var application = new JobCandidateApplication
            {
                JobId = JobID,
                CandidateId = requesterId,
                AppliedAt = DateTime.UtcNow,
                JobApplicationStatus = JobApplicationStatus.Applied,
                StatusUpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<JobCandidateApplication>().AddAsync(application);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> Cancel(int id, int requesterId)
        {
            var application = await _unitOfWork.GetRepository<JobCandidateApplication>().GetByIdAsync(id);
            if (application == null) return false;
            if (application.CandidateId != requesterId) return false;
            if (application.JobApplicationStatus != JobApplicationStatus.Applied &&
       application.JobApplicationStatus != JobApplicationStatus.UnderReview)
                return false;
            application.JobApplicationStatus = JobApplicationStatus.Canceled;
            application.CancelledAt = DateTime.UtcNow;
            application.StatusUpdatedAt = DateTime.UtcNow;
            _unitOfWork.GetRepository<JobCandidateApplication>().Update(application);
            return await _unitOfWork.SaveChangesAsync() > 0;




        }
    }
}
