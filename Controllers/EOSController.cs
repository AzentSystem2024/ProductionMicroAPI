using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EOSController : ControllerBase
    {
        private readonly IEOSService _EosReasonService;
        public EOSController(IEOSService EOSService)
        {
            _EosReasonService = EOSService;
        }
        [HttpPost]
        [Route("list")]
        public EosReasonResponse List()
        {
            EosReasonResponse res = new EosReasonResponse();
            try
            {
                res.datas = _EosReasonService.GetAllEosReasons();
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public EosReasonResponse Select(int id)
        {
            EosReasonResponse response = new EosReasonResponse();
            try
            {
                response.data = _EosReasonService.GetEosReasonById(id);
            }
            catch (Exception ex)
            {
                response.flag = "0";
                response.message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("save")]
        public EosReasonResponse SaveData(EosReason eosReasonData)
        {
            EosReasonResponse res = new EosReasonResponse();
            try
            {
                Int32 ID = _EosReasonService.SaveEosReason(eosReasonData);
                res.flag = "1";
                res.message = "Success";
                res.data = _EosReasonService.GetEosReasonById(ID);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("edit")]
        public EosReasonResponse EditData(EosReason eosReasonData)
        {
            EosReasonResponse res = new EosReasonResponse();
            try
            {
                Int32 ID = _EosReasonService.SaveEosReason(eosReasonData);
                res.flag = "1";
                res.message = "Success";
                res.data = _EosReasonService.GetEosReasonById(ID);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public EosReasonResponse Delete(int id)
        {
            EosReasonResponse res = new EosReasonResponse();
            try
            {
                _EosReasonService.DeleteEosReason(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _EosReasonService.GetEosReasonById(id);
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

