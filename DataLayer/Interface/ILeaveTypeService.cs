using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ILeaveTypeService
    {
        public LeaveTypeLogListResponseData GetAllLeaveType();
        public saveLeaveTypeResponseData SaveData(saveLeaveTypeData typeData);
        public saveLeaveTypeData selectLeaveTypeData(int id);
        public saveLeaveTypeResponseData UpdateData(saveLeaveTypeData typeData);
        public saveLeaveTypeResponseData DeleteLeaveType(int id);

    }
}
