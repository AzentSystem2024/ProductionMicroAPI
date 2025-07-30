using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Services;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayTimeEntryController :ControllerBase
    {
        private readonly IPayTimeEntryService _PayTimeEntryService;
        public PayTimeEntryController(IPayTimeEntryService PayTimeEntryService)
        {
            _PayTimeEntryService = PayTimeEntryService;
        }

        [HttpPost]
        [Route("save")]
        public PayTimeResponse Save(PayTimeEntry payData)
        {
            PayTimeResponse res = new PayTimeResponse();

            try
            {

                _PayTimeEntryService.Save(payData);

                res.flag = "1";
                res.message = "Success";

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
