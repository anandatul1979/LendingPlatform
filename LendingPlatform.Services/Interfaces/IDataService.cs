using LendingPlatform.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingPlatform.Services.Interfaces
{
    public interface IDataService
    {
        List<Applicant> AddApplicant(Applicant loanApplicant);
        List<Applicant> GetLoanApplicants();
    }
}
