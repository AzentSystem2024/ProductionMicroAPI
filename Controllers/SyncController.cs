using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
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
        public SyncResponse Insert(List<SyncArticleProduction> model)
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
        public SyncResponse UploadProductionDN(ProductionDN model)
        {
            SyncResponse res = new SyncResponse();

            try
            {
                res = _syncService.UploadProductionDN(model);
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
        public GRNUploadResponse SaveProductionTransferInGRN(ProductionTransferInGRN model)
        {
            GRNUploadResponse res = new GRNUploadResponse();

            try
            {
                res = _syncService.SaveProductionTransferInGRN(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        //[HttpPost]
        //[Route("delivery-note")]
        //public SyncResponse UploadProductionDN(ProductionDN model)
        //{
        //    SyncResponse res = new SyncResponse();

        //    try
        //    {
        //        res = _syncService.UploadProductionDN(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Flag = 0;
        //        res.Message = "Error: " + ex.Message;
        //    }

        //    return res;
        //}
        [HttpPost]
        [Route("delivery-return")]
        public SyncResponse UploadProductionDR(ProductionDR model)
        {
            SyncResponse res = new SyncResponse();

            try
            {
                res = _syncService.UploadProductionDR(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public ProductionViewResponse GetProductionById(int id)
        {
            ProductionViewResponse res = new ProductionViewResponse();

            try
            {
                res = _syncService.GetProductionById(id);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("production-list")]
        public ProductionListResponse GetProductionList(ProductionListRequest model)
        {
            ProductionListResponse res = new ProductionListResponse();

            try
            {
                res = _syncService.GetProductionList(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("boxselect/{id:int}")]
        public ProductionViewResponse GetBoxProductionById(int id)
        {
            ProductionViewResponse res = new ProductionViewResponse();

            try
            {
                res = _syncService.GetBoxProductionById(id);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("boxlist")]
        public ProductionListResponse GetBoxProductionList(ProductionListRequest model)
        {
            ProductionListResponse res = new ProductionListResponse();

            try
            {
                res = _syncService.GetBoxProductionList(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("DNlist")]
        public DNListResponse GetDNList(ProductionListRequest model)
        {
            DNListResponse res = new DNListResponse();

            try
            {
                res = _syncService.GetDNList(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("DNselect/{id:int}")]
        public DNViewResponse GetProductionDNById(int id)
        {
            DNViewResponse res = new DNViewResponse();

            try
            {
                res = _syncService.GetProductionDNById(id);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("savedispatch")]
        public SyncResponse SaveDispatch(DispatchSave model)
        {
            SyncResponse res = new SyncResponse();

            try
            {
                res = _syncService.SaveDispatch(model);
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
