
using AutoMapper;
using JobApplication.Application.DTOs;
using JobApplication.Application.Services.ServiceAbstraction;
using JobApplication.Domain;
using JobApplication.Infrastracture.Contract;

namespace JobApplication.Application.Services.ServiceImplimentation
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JobService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<bool> CancelJob(int id)
        {

            var job = await _unitOfWork.GetRepository<Job>().GetByIdAsync(id);
            if (job == null) return false;
            job.IsActive = false;
           _unitOfWork.GetRepository<Job>().Update(job);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> ClosedJob(int id)
        {
            var job = await _unitOfWork.GetRepository<Job>().GetByIdAsync(id);
            if (job == null) return false;

            if (job.IsActive == false) return false;

            job.IsActive = false;
            job.ClosedAt = DateTime.Now;

            _unitOfWork.GetRepository<Job>().Update(job);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<JobDTO?> CreateJobAsync(JobDTO newjob)
        {
            if (newjob == null) return null;
          
            var job = _mapper.Map<Job>(newjob);
            await _unitOfWork.GetRepository<Job>().AddAsync(job);
            if (await _unitOfWork.SaveChangesAsync() > 0)
                return newjob;
            return null;


        }

        public async Task<IEnumerable<JobDTO>> GetAllJobsAsync()
        {
            var jobs = await _unitOfWork.GetRepository<Job>().GetAllAsync();
            if (!jobs.Any())
                return [];
            var returnJobs = _mapper.Map<IEnumerable<JobDTO>>(jobs);
            return returnJobs;
        }

        public async Task<JobDTO?> GetJobByIdAsync(int id)
        {
            var job = await _unitOfWork.GetRepository<Job>().GetByIdAsync(id);
            if (job is null)
                return null;
            var returnedjob = _mapper.Map<JobDTO>(job);
            return returnedjob;
        }

        public async Task<JobDTO?> ReOpenJob(int id)
        {
            var oldJob = await _unitOfWork.GetRepository<Job>().GetByIdAsync(id);
            if (oldJob is null)
                return null;
            oldJob.IsActive = true;
            oldJob.ClosedAt = null;
            _unitOfWork.GetRepository<Job>().Update(oldJob);
            if (await _unitOfWork.SaveChangesAsync() > 0) return _mapper.Map<JobDTO>(oldJob);
            return null;
        }
    }
}

