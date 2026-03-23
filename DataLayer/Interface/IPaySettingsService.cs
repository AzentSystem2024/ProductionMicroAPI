using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPaySettingsService
    {
        public PaySettings GetPaySettings();
        public bool SavePaySettings(PaySettings settings);
    }
}
