using JobApplication.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;


namespace JobApplication.Application.Services.ServiceAbstraction
{
    public interface IJobService
    {
        Task<IEnumerable<JobDTO>> GetAllJobsAsync();
        Task<JobDTO?> GetJobByIdAsync(int id);
        Task<JobDTO?> CreateJobAsync(JobDTO newjob);

        Task<bool> ClosedJob(int id);
        Task<JobDTO?> ReOpenJob(int id);
        Task<bool> CancelJob(int id);
    }
}
