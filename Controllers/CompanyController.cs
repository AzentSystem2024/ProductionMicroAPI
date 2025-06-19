using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        [Route("insert")]
        public CompanyResponse Insert(Company company)
        {
            CompanyResponse res = new CompanyResponse();
            try
            {
                res = _companyService.Insert(company);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("update")]
        public CompanyResponse Update(CompanyUpdate company)
        {
            CompanyResponse res = new CompanyResponse();
            try
            {
                res = _companyService.UpdateCompany(company);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("list")]
        public CompanyListResponse CompanyList()
        {
            CompanyListResponse res = new CompanyListResponse();
            try
            {
                res = _companyService.GetCompanyList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public CompanyResponse GetCompanyById(int id)
        {
            CompanyResponse res = new CompanyResponse();

            try
            {
                res = _companyService.GetCompanyById(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public CompanyResponse DeleteCompany(int id)
        {
            CompanyResponse res = new CompanyResponse();
            try
            {
                res = _companyService.DeleteCompany(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }



    }
}
