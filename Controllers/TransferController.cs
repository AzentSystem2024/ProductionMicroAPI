using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;
        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }
        [HttpPost]
        [Route("list")]
        public TransferListResponse TransferList()
        {
            TransferListResponse res = new TransferListResponse();
            try
            {
                res = _transferService.GetTransferList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
                res.Data = new List<Transfer>();
            }

            return res;
        }

    }
}
