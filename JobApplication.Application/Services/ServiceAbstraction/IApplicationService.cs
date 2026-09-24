using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Services.ServiceAbstraction
{
    public interface IApplicationService
    {

        public Task<bool> Cancel(int id, int requesterId);
        public Task<bool> Apply(int JobID, int requesterId);

    }
}
