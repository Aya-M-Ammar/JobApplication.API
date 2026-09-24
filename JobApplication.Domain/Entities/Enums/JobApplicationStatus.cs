using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Domain.Entities.Enums
{
    public enum JobApplicationStatus
    {
        Applied,
        UnderReview,
        InterView,
        Accepted,
        Rejected,
        Canceled
    }
}
