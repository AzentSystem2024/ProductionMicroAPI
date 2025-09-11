using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferOutInvController : ControllerBase
    {
        private readonly ITransferOutInvService _transferOutInv;
        public TransferOutInvController(ITransferOutInvService transferOutInv)
        {
            _transferOutInv = transferOutInv;
        }
        [HttpPost]
        [Route("insert")]
        public TransferSaveResponse Insert(TransferOutInv transferOut)
        {
            TransferSaveResponse res = new TransferSaveResponse();

            try
            {

                _transferOutInv.Insert(transferOut);
                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("getitem")]
        public ItemInfoResponse GetItemInfo(ItemRequest request)
        {
            ItemInfoResponse res = new ItemInfoResponse();

            try
            {
                var result = _transferOutInv.GetItemInfo(request);

                res.Flag = 1;
                res.Message = "Success";
                res.Data = result;
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
