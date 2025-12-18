using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalaryHeadService
    {
        public SalaryHeadListResponse GetAllSalaryHead(SalaryHeadListRequest request);
        public Int32 SaveData(SalaryHead salaryHead);
        public Int32 EditData(SalaryHeadUpdate salaryHead);
        public SalaryHeadUpdate GetItem(int id);
        public bool DeleteSalaryHead(int id);
    }
}
