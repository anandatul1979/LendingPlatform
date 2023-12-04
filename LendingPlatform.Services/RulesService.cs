using LendingPlatform.Services.Interfaces;
using LendingPlatform.Services.Models;

namespace LendingPlatform.Services
{
    public class RulesService : IRulesService
    {
        private readonly Func<Applicant, bool>[] RulesList;

        public RulesService()
        {
            RulesList = new[]
            {
                ValidateLoanLimits,
                LoanRule1,
                LoanRule2,
                LoanRule3,
                LoanRule4,
                LoanRule5,
            };
        }

        public bool IsApplicationSuccess(Applicant details) =>
            RulesList.All(r => r.Invoke(details));

        public bool ValidateLoanLimits(Applicant details)
        {
            if (details.LoanAmount > 1500000 || details.LoanAmount < 100000)
                return false;

            return true;
        }

        //Use better name to match the loan rule
        public bool LoanRule1(Applicant details)
        {
            if (details.LoanAmount >= 1000000)
            {
                if (details.LoanToValue > 60 || details.CreditScore < 950)
                {
                    return false;
                }
            }

            return true;
        }

        public bool LoanRule2(Applicant details)
        {
            if (details.LoanAmount < 1000000)
            {
                if (details.LoanToValue < 60 && details.CreditScore < 750)
                {
                    return false;
                }
            }

            return true;
        }

        public bool LoanRule3(Applicant details)
        {
            if (details.LoanAmount < 1000000)
            {
                if (details.LoanToValue >= 60 && details.LoanToValue < 80 && details.CreditScore < 800)
                {
                    return false;
                }
            }

            return true;
        }

        public bool LoanRule4(Applicant details)
        {
            if (details.LoanAmount < 1000000)
            {
                if (details.LoanToValue >= 80 && details.LoanToValue < 90 && details.CreditScore < 900)
                {
                    return false;
                }
            }

            return true;
        }

        public bool LoanRule5(Applicant details)
        {
            if (details.LoanAmount < 1000000)
            {
                if (details.LoanToValue >= 90)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
