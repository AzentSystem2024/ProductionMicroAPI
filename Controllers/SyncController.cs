using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SyncController : ControllerBase
    {
        private readonly ISyncService _syncService;

        public SyncController(ISyncService syncService)
        {
            _syncService = syncService;
        }
        [HttpPost]
        [Route("article-production")]
        public SyncResponse Insert(Sync model)
        {
            SyncResponse res = new SyncResponse();

            try
            {
                res = _syncService.Insert(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("box-production")]
        public SyncResponse UploadPackProduction(PackProductionSync model)
        {
            SyncResponse res = new SyncResponse();

            try
            {
                res = _syncService.UploadPackProduction(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("transfer-out")]
        public SyncResponse UploadProductionTransferOut(ProductionTransferOut model)
        {
            SyncResponse res = new SyncResponse();

            try
            {
                res = _syncService.UploadProductionTransferOut(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("transfer-in")]
        public SyncResponse UploadProductionTransferIn(ProductionTransferIn model)
        {
            SyncResponse res = new SyncResponse();

            try
            {
                res = _syncService.UploadProductionTransferIn(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
