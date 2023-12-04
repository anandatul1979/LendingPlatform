using LendingPlatform.Services;
using LendingPlatform.Services.Models;
namespace LendingPlatform.Tests
{
    [TestClass]
    public class ServicesTests
    {
        // I would mock interface and use dependency injection to write these tests providing I have more time
        [TestMethod]
        public void Rule_Check_Boundaries()
        {
            var rulesHandler = new RulesService();
            var applicant = new Applicant
            {
                LoanAmount = 1800000,
                AssetValue = 5200000,
                CreditScore = 999,
                LoanToValue = (1800000 / 5200000) * 100,
            };
            Assert.IsFalse(rulesHandler.ValidateLoanLimits(applicant));
        }

        [TestMethod]
        public void Check_Application_Success()
        {
            var rulesHandler = new RulesService();

            var applicant = new Applicant
            {
                LoanAmount = 1600000,
                AssetValue = 5000000,
                CreditScore = 999,
                LoanToValue = (1600000 / 5000000) * 100,
            };

            Assert.IsFalse(rulesHandler.IsApplicationSuccess(applicant));
        }

        // More tests can be written here for checking each rule
    }
}