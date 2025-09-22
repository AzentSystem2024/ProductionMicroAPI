using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VatClassController : ControllerBase
    {
        private readonly IVatClassService _vatClassService;
        public VatClassController(IVatClassService vatClassService)
        {
            _vatClassService = vatClassService;
        }
        [HttpPost]
        [Route("list")]
        public List<VatClass> List()
        {
            List<VatClass> vatclass = new List<VatClass>();

            try
            {
                
                vatclass = _vatClassService.GetAllVatClass();
            }
            catch (Exception ex)
            {
            }
            return vatclass.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public VatClass Select(int id)
        {
            VatClass objVatClass = new VatClass();
            try
            {
                
                objVatClass = _vatClassService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objVatClass;
        }

        [HttpPost]
        [Route("save")]
        public VatClassResponse SaveData(VatClass vatclassData)
        {
            VatClassResponse res = new VatClassResponse();

            try
            {
                
                Int32 ID = _vatClassService.SaveData(vatclassData);

                res.flag = "1";
                res.message = "Success";
                res.data = _vatClassService.GetItems(ID);
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
        public VatClassResponse Delete(int id)
        {
            VatClassResponse res = new VatClassResponse();

            try
            {
                

                _vatClassService.DeleteVatClass(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _vatClassService.GetItems(id);
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
