using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalaryWPSService
    {
        SalaryWPSResponse GetSalaryWPS(SalaryWPSRequest request);
    }
}
