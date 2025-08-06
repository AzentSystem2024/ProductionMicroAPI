using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PDCController : ControllerBase
    {
        private readonly IPDCService _pdcService;
        public PDCController(IPDCService dcService)
        {
            _pdcService = dcService;
        }
        [HttpPost]
        [Route("list")]
        public PDCResponse PDCList()
        {
            PDCResponse response = new PDCResponse();
            try
            {
                response = _pdcService.GetPDCList();
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.Data = new List<PDCList>();
            }
            return response;
        }
        [HttpPost]
        [Route("save")]
        public PDCSaveResponse SaveData(PDCModel pdc)
        {
            PDCSaveResponse response = new PDCSaveResponse();
            try
            {
                _pdcService.SaveData(pdc);
                response.Flag = "1";
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Message = ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("update")]
        public PDCSaveResponse UpdateData(PDCModel pdc)
        {
            PDCSaveResponse response = new PDCSaveResponse();
            try
            {
                _pdcService.UpdateData(pdc);
                response.Flag = "1";
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Message = ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public PDCSelectResponse SelectData(int? id)
        {
            PDCSelectResponse response = new PDCSelectResponse();
            try
            {
                response = _pdcService.GetPDCById(id);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public PDCSaveResponse Delete(int id)
        {
            PDCSaveResponse response = new PDCSaveResponse();
            try
            {
                _pdcService.Delete(id);
                response.Flag = "1";
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Message = ex.Message;
            }
            return response;
        }
    }

}

