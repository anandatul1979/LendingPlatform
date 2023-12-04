using Microsoft.Extensions.DependencyInjection;
using LendingPlatform.Services.Interfaces;
using LendingPlatform.Services;
using LendingPlatform.Services.Models;

namespace LendingPlatform
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessRequest();
        }

        static void ProcessRequest()
        {
            // DI
            var serviceProvider = new ServiceCollection()
                .AddScoped<IDataService, DataService>()
                .AddScoped<IRulesService, RulesService>()
                .BuildServiceProvider();

            Console.Clear();

            // get the service instance
            var dataService = serviceProvider.GetService<IDataService>();
            var rulesService = serviceProvider.GetService<IRulesService>();

            var allApplicants = dataService.GetLoanApplicants();


            // Get application Stats, I would create a class with all these params
            GetApplicationStats(allApplicants, 
                out int successfulApplicants, 
                out int unSuccessfulApplicants, 
                out decimal totalLoanValues, 
                out decimal averageLtv);

            // user prompts
            Console.WriteLine(SystemMessages.Title);
            Console.WriteLine();
            Console.WriteLine($"{SystemMessages.SuccessfulApplications}: {successfulApplicants}");
            Console.WriteLine($"{SystemMessages.UnSuccessfulApplications}: {unSuccessfulApplicants}");
            Console.WriteLine($"{SystemMessages.TotalLoans}: {totalLoanValues}");
            Console.WriteLine($"{SystemMessages.AverageLtv}: {averageLtv}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(SystemMessages.TotalLoans);

            Console.WriteLine(SystemMessages.EnterLoanAmount);

            var strLoanAmount = Console.ReadLine();


            // These nested if/else statements could be better with another validator like fluent valiodator
            if (decimal.TryParse(strLoanAmount, out decimal loanValue) == false)
            {
                RestartConsole(SystemMessages.EnterValidLoan);
            }
            else
            {
                Console.WriteLine(SystemMessages.EnterAssetValue);
                var strAssetValue = Console.ReadLine();

                if (decimal.TryParse(strAssetValue, out decimal assetValue) == false)
                {
                    RestartConsole(SystemMessages.EnterValidAssetValue);
                }
                else
                {
                    Console.WriteLine(SystemMessages.EnterCreditScore);
                    var strCreditScore = Console.ReadLine();

                    if (int.TryParse(strCreditScore, out int creditScore) == false)
                    {
                        RestartConsole(SystemMessages.EnterValidCreditScore);
                    }
                    else
                    {
                        string strContinue = CheckSuccess(loanValue, assetValue, creditScore, dataService, rulesService);

                        if (string.Compare(strContinue, "y", true) == 0)
                            ProcessRequest();
                    }
                }
            }
        }

        private static void GetApplicationStats(List<Applicant> allApplicants, out int successfulApplicants, out int unSuccessfulApplicants, out decimal totalLoanValues, out decimal averageLtv)
        {
            // Get application stats
            // we can also do this at the database level which will show better performance if large amount of data
            successfulApplicants = allApplicants.Count(x => x.IsSuccess == true);
            unSuccessfulApplicants = allApplicants.Count(x => x.IsSuccess == false);
            totalLoanValues = allApplicants.FindAll(x => x.IsSuccess == true).Select(x => x.LoanAmount).Sum();

            // Calculate average LTV
            averageLtv = allApplicants.Count > 0 ? allApplicants.Select(x => x.LoanToValue).Average() : 0;
        }

        private static string? CheckSuccess(decimal loanValue, decimal assetValue, int creditScore, IDataService? dataService, IRulesService? rulesService)
        {
            var applicant = new Applicant
            {
                LoanAmount = loanValue,
                AssetValue = assetValue,
                CreditScore = creditScore
            };

            applicant.IsSuccess = rulesService.IsApplicationSuccess(applicant);

            dataService.AddApplicant(applicant);

            if (applicant.IsSuccess)
                Console.WriteLine(SystemMessages.ApplicationSuccess);
            else
                Console.WriteLine(SystemMessages.ApplicationUnSuccess);

            Console.WriteLine();
            Console.WriteLine(SystemMessages.CreateAnother);

            var strContinue = Console.ReadLine();
            return strContinue;
        }

        static void RestartConsole(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine(SystemMessages.EnterToContinue);
            Console.ReadLine();
            ProcessRequest();
        }
    }
}