using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.DataLayer.Services;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class FinalSettlementController : ControllerBase
    {
        private readonly IFinalSettlementService _FinalSettlementService;
        public FinalSettlementController(IFinalSettlementService FinalSettlementService)
        {
            _FinalSettlementService = FinalSettlementService;
        }

        [HttpPost]
        [Route("Settlement")]
        public FinalSettlementResponse FinalSettlement(FinalSettlementRequest request)
        {
            return _FinalSettlementService.GetFinalSettlement(request);
        }

    }
}

