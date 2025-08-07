using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DropDownController : ControllerBase
    {
        private readonly IDropDownService _DropDownService;

        public DropDownController(IDropDownService DropDownService)
        {
            _DropDownService = DropDownService;
        }

        [HttpPost]
        public List<DropDown> ListData(DropDownInput vInput)
        {
            List<DropDown> vData = new List<DropDown>();
            try
            {
                vData = _DropDownService.GetDropDownData(
                    vInput.NAME,
                    vInput.COUNTRY_ID,
                    vInput.STATE_ID,
                    vInput.DISTRICT_ID,
                    vInput.COMPANY_ID
                  
                );
            }
            catch (Exception ex)
            {
                // Optionally log or handle the exception
            }
            return vData;
        }

    }
}
