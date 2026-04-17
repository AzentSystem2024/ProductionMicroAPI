using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace RetailApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpPost]
        [Route("list")]
        public List<Currency> List()
        {
            List<Currency> currencies = new List<Currency>();

            try
            {

                currencies = _currencyService.GetAllCurrency();
            }
            catch (Exception ex)
            {
            }
            return currencies.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Currency Select(int id)
        {
            Currency objCurrency = new Currency();
            try
            {

                objCurrency = _currencyService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objCurrency;
        }

        [HttpPost]
        [Route("save")]
        public CurrencyResponse SaveData(Currency currencyData)
        {
            CurrencyResponse res = new CurrencyResponse();

            try
            {

                Int32 ID = _currencyService.SaveData(currencyData);

                res.flag = "1";
                res.message = "Success";
                res.data = _currencyService.GetItems(ID);
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
        public CurrencyResponse Delete(int id)
        {
            CurrencyResponse res = new CurrencyResponse();

            try
            {


                _currencyService.DeleteCurrency(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _currencyService.GetItems(id);
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
