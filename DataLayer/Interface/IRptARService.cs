using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IRptARService
    {
        public RptARResponseOutput claimDetailsReport(RptARInput vInput);

    }
}
