using MicroApi.Models;
using static MicroApi.DataLayer.Service.SalaryService;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalaryService
    {
        GenerateSalaryResponse GenerateSalary(List<Salary> salaryList);
        SalaryLookupResponse GetSalaryLookup(SalaryLookupRequest request);
        PayrollViewResponse GetPayrollDetails(int id);
        public PayrollResponse Edit(UpdateItemRequest model);

    }
}
