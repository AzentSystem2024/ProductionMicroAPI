using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IAdvanceService
    {
        public AdvanceLogListResponseData GetAllPayAdvance();
        public saveAdvanceResponseData SaveData(saveAdvanceData advData);
        public saveAdvanceData selectPayAdvanceData(int id);
        public saveAdvanceResponseData UpdateData(saveAdvanceData advData);
        public saveAdvanceResponseData DeletePayAdvance(int id);
        public saveAdvanceResponseData VerifyData(saveAdvanceData advData);
        public saveAdvanceResponseData ApproveData(saveAdvanceData advData);

    }
}
