using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransferInController : ControllerBase
    {
        private readonly ITransferInService _transferInService;

        public TransferInController(ITransferInService transferInService)
        {
            _transferInService = transferInService;
        }

        [HttpPost]
        [Route("getitem")]
        public TransferInListResponse GetTransferInItems([FromBody] TransferInInput input)
        {
            var res = new TransferInListResponse();
            try
            {
                var result = _transferInService.GetTransferInItems(input);
                res.Flag = 1;
                res.Message = "Success";
                res.data = result;
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("insert")]
        public TransferInResponse Insert(TransferIn transferIn)
        {
            var res = new TransferInResponse();
            try
            {
                _transferInService.Insert(transferIn);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("update")]
        public TransferInResponse Update(TransferInUpdate transferIn)
        {
            var res = new TransferInResponse();
            try
            {
                _transferInService.Update(transferIn);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("list")]
        public TransferInListsResponse List()
        {
            TransferInListsResponse res = new TransferInListsResponse();

            try
            {
                res = _transferInService.List();
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
                res.data = new List<TransferInList>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public TransferInInvUpdate Select(int id)
        {
            TransferInInvUpdate objScheme = new TransferInInvUpdate();
            try
            {
                objScheme = _transferInService.GetTransferIn(id);
            }
            catch (Exception ex)
            {
            }
            return objScheme;
        }
        [HttpPost]
        [Route("transferno")]
        public TransferinDoc GetLastDocNo()
        {
            TransferinDoc res = new TransferinDoc();

            try
            {
                res = _transferInService.GetLastDocNo();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpDelete]
        [Route("delete/{id:int}")]
        public TransferInResponse Delete(int id)
        {
            var res = new TransferInResponse();
            try
            {
                _transferInService.Delete(id);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
