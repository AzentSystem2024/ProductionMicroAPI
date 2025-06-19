using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDistributorService
    {
        DistributorResponse Insert(Distributor distributor);
        DistributorResponse UpdateDistributor(DistributorUpdate distributor);
        DistributorListResponse GetDistributorList();
        DistributorResponse GetDistributorById(int id);
        DistributorResponse DeleteDistributorData(int id);
        InsertResponse InsertDistrict(District district);
        InsertResponse InsertCity(City city);



    }
}
