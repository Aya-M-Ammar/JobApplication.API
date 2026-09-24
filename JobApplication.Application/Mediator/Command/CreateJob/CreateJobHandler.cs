using AutoMapper;
using JobApplication.Application.DTOs;
using JobApplication.Domain;
using JobApplication.Infrastracture.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace JobApplication.Application.Mediator.Command.CreateJob
{
    public class CreateJobHandler : IRequestHandler<CreateJobCommand, JobDTO?>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateJobHandler(IMapper mapper,IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        public async Task<JobDTO?> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return null;

            var job = _mapper.Map<Job>(request);
            await _unitOfWork.GetRepository<Job>().AddAsync(job);
            if (await _unitOfWork.SaveChangesAsync() > 0)
             return   _mapper.Map<JobDTO>(job);
            return null;
        }
    }
}
