using AutoMapper;
using JobApplication.Application.DTOs;
using JobApplication.Domain;
using JobApplication.Infrastracture.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Mediator.Query.GetAllJob
{
    public class GetAllJobHandler : IRequestHandler<GetAllJobQuery, IEnumerable<JobDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllJobHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<JobDTO>> Handle(GetAllJobQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _unitOfWork.GetRepository<Job>().GetAllAsync();
            if (!jobs.Any())
                return [];
            var returnJobs = _mapper.Map<IEnumerable<JobDTO>>(jobs);
            return returnJobs;
        }
    }
}
