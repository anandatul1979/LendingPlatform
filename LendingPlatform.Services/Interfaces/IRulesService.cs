using LendingPlatform.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingPlatform.Services.Interfaces
{
    public interface IRulesService
    {
        bool IsApplicationSuccess(Applicant details);
        bool ValidateLoanLimits(Applicant details);
        bool LoanRule1(Applicant details);
        bool LoanRule2(Applicant details);
        bool LoanRule3(Applicant details);
        bool LoanRule4(Applicant details);
        bool LoanRule5(Applicant details);
    }
}
