using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ITimeSheetService
    {
        public TimeSheetLogListResponseData GetAllTimeSheet();
        public saveTimeSheetResponseData SaveData(saveTimeSheetData advData);
        public saveTimeSheetData selectTimeSheetData(int id);
        public saveTimeSheetResponseData UpdateData(saveTimeSheetData advData);
        public saveTimeSheetResponseData DeleteTimeSheet(int id);
        public saveTimeSheetResponseData VerifyData(saveTimeSheetData advData);
        public saveTimeSheetResponseData ApproveData(saveTimeSheetData advData);

    }
}
