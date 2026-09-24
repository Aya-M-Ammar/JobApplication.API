using AutoMapper;
using JobApplication.Application.DTOs;
using JobApplication.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Services.Mapper
{
    public class MappProfile : Profile
    {
        public MappProfile()
        {
            CreateMap<Job, JobDTO>().ReverseMap();
          

        }

       
    }
}
