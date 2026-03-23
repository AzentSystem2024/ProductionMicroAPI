using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPaySettingsService
    {
        public PaySettings GetPaySettings(PaySettingslist request);
        public bool SavePaySettings(PaySettings settings);
    }
}
