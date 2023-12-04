using LendingPlatform.Services.Interfaces;
using LendingPlatform.Services.Models;

namespace LendingPlatform.Services
{
    public class DataService : IDataService
    {
        public List<Applicant> GetLoanApplicants()
        {
            // Read all applicants from the DB and return
            return new List<Applicant>();
        }

        public List<Applicant> AddApplicant(Applicant loanApplicant)
        {
            // Add new applicant to the DB
            return new List<Applicant>();
        }
    }
}
