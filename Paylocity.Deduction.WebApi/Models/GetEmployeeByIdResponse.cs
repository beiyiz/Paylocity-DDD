using Paylocity.Deduction.Core.Aggregates;

namespace Paylocity.Deduction.WebApi.Models
{
    public class GetEmployeeByIdResponse
    {
        public Employee? Employee { get; set; }
        public decimal? Deductables { get; set; }
    }
}
