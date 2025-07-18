using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalaryService
    {
        GenerateSalaryResponse GenerateSalary(List<Salary> salaryList);
        SalaryLookupResponse GetSalaryLookup(SalaryLookupRequest request);


    }
}
