using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiscReceiptController : ControllerBase
    {
        private readonly IMiscReceiptService _miscreceiptService;

        public MiscReceiptController(IMiscReceiptService miscreceiptService)
        {
            _miscreceiptService = miscreceiptService;
        }
        
    
        }
    }
