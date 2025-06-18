using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDistributorService
    {
        DistributorResponse Insert(Distributor distributor);
    }
}
