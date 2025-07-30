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
        public PayTimeResponse Save(PayTimeEntryInsert request)
        {
            PayTimeResponse res = new PayTimeResponse();

            try
            {

                res = _PayTimeEntryService.Save(request);
                               

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("select")]
        public PayTimeSelectResponse GetPayTimeEntry(PayTimeEntryRequest request)
        {
            PayTimeSelectResponse res = new PayTimeSelectResponse();

            try
            {
                res = _PayTimeEntryService.GetPayTimeEntry(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = "Error: " + ex.Message;
                res.data = null;
            }

            return res;
        }

    }
}
