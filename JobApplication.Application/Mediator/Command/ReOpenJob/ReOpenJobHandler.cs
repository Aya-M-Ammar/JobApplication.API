using AutoMapper;
using JobApplication.Application.DTOs;
using JobApplication.Domain;
using JobApplication.Infrastracture.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Mediator.Command.ReOpenJob
{
    public class ReOpenJobHandler : IRequestHandler<ReOpenCommand, JobDTO?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReOpenJobHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<JobDTO?> Handle(ReOpenCommand request, CancellationToken cancellationToken)
        {
            var oldJob = await _unitOfWork.GetRepository<Job>().GetByIdAsync(request.Id);
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
