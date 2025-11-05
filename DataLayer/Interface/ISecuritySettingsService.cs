using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISecuritySettingsService
    {
        SecuritySettingResponse GetAllSecuritySetting(Int32 intUserID);
        Int32 Save(SecuritySettings securitySettings, Int32 userID);
    }
}
