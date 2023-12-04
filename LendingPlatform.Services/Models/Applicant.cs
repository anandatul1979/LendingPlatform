
namespace LendingPlatform.Services.Models
{
    public class Applicant
    {
        public string ID { get; set; }

        public decimal LoanAmount { get; set; }

        public decimal AssetValue { get; set; }

        public decimal LoanToValue { get; set; }

        public int CreditScore { get; set; }

        public bool IsSuccess { get; set; }

        public DateTime DateRequested { get; set; }

    }
}
