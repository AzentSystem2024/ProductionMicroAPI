using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ITimeSheetService
    {
        public TimeSheetLogListResponseData GetAllTimeSheet(TimeSheetRequestlist request);
        public saveTimeSheetResponseData SaveData(saveTimeSheetData advData);
        public saveTimeSheetData selectTimeSheetData(int id);
        public saveTimeSheetResponseData UpdateData(saveTimeSheetData advData);
        public saveTimeSheetResponseData DeleteTimeSheet(int id);
        public TimeSheetHeaderListResponseData VerifyData(ApproveRequest request);
        public saveTimeSheetResponseData ApproveData(saveTimeSheetData advData);
        TimeSheetHeaderListResponseData GetTimeSheetByCompanyAndMonth(TimeSheetRequest request);
        TimeSheetHeaderListResponseData ApproveTimeSheets(ApproveRequest request);
        TimeSheetHeaderListResponseData GetPayrollPendingTimeSheets(TimeSheetRequest request);
        EmployeeVacationListResponseData GetEmployeeVacation(EmployeeVacationRequest request);


    }
}
