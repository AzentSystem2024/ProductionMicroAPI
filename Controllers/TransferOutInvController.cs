using MicroApi.DataLayer.Interface;
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
    }
}
