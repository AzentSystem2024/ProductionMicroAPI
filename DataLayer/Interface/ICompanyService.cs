using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ICompanyService
    {
        CompanyResponse Insert(Company company);
        CompanyResponse UpdateCompany(CompanyUpdate company);
        CompanyListResponse GetCompanyList();
        CompanyResponse GetCompanyById(int id);
        CompanyResponse DeleteCompany(int id);


    }
}
