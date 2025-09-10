using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost]
        [Route("list")]
        public List<Country> List()
        {
            List<Country> countries = new List<Country>();

            try
            {
                
                countries = _countryService.GetAllCountry();
            }
            catch (Exception ex)
            {
            }
            return countries.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Country Select(int id)
        {
            Country objCountry = new Country();
            try
            {
                
                objCountry = _countryService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objCountry;
        }

        [HttpPost]
        [Route("save")]
        public CountryResponse SaveData(Country countryData)
        {
            CountryResponse res = new CountryResponse();

            try
            {
                
                Int32 ID = _countryService.SaveData(countryData);

                res.flag = "1";
                res.message = "Success";
                res.data = _countryService.GetItems(ID);
            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public CountryResponse Delete(int id)
        {
            CountryResponse res = new CountryResponse();

            try
            {
                

                _countryService.DeleteCountry(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _countryService.GetItems(id);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }
    }
}
