using AutoMapper;
using JobApplication.Application.DTOs;
using JobApplication.Domain;
using JobApplication.Infrastracture.Contract;
using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace JobApplication.Application.Mediator.Query.GetJobById
{
    public class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, JobDTO?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetJobByIdHandler(IUnitOfWork unitOfWork,IMapper mapper )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<JobDTO?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _unitOfWork.GetRepository<Job>().GetByIdAsync(request.JobId);
            if (job is null)
                return null;
            var returnedjob = _mapper.Map<JobDTO>(job);
            return returnedjob;
        }
    }
}
