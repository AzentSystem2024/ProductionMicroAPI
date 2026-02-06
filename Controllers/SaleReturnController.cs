using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.DataLayer.Services;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleReturnController : ControllerBase
    {
        private readonly ISaleReturnService _salereturn;

        public SaleReturnController(ISaleReturnService salereturn)
        {
            _salereturn = salereturn;
        }
        [HttpPost]
        [Route("insert")]
        public SaleReturnResponse Insert(SaleReturn model)
        {
            SaleReturnResponse res = new SaleReturnResponse();

            try
            {
                res = _salereturn.Insert(model);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public SaleReturnListResponse GetSaleReturnList(SaleReturnListRequest request)
        {
            SaleReturnListResponse res = new SaleReturnListResponse();

            try
            {
                res = _salereturn.GetSaleReturnList(request);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("pendinglist")]
        public SaleInvoiceDetailResponse GetSalesInvoiceDetail(InvoieRequest request)
        {
            SaleInvoiceDetailResponse res = new SaleInvoiceDetailResponse();

            try
            {
                res = _salereturn.GetSalesInvoiceDetail(request);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<SaleInvoiceDetail>();
            }

            return res;
        }
        [HttpPost]
        [Route("save")]
        public SaleReturnResponse InsertSaleReturn(SaleReturnInsertRequest model)
        {
            SaleReturnResponse res = new SaleReturnResponse();

            try
            {

                _salereturn.InsertSaleReturn(model);
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
        public SaleReturnResponse UpdateSaleReturn(SaleReturnInsertRequest model)
        {
            SaleReturnResponse res = new SaleReturnResponse();

            try
            {

                _salereturn.UpdateSaleReturn(model);
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
        [Route("commit")]
        public SaleReturnResponse CommitSaleReturn(SaleReturnInsertRequest model)
        {
            SaleReturnResponse res = new SaleReturnResponse();

            try
            {

                _salereturn.CommitSaleReturn(model);
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
        [Route("select/{id:int}")]
        public SaleReturnViewResponse GetSaleReturnById(int id)
        {
            SaleReturnViewResponse res = new SaleReturnViewResponse();

            try
            {
                res = _salereturn.GetSaleReturnById(id);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public SaleReturnResponse Delete(int id)
        {
            SaleReturnResponse res = new SaleReturnResponse();

            try
            {


                _salereturn.Delete(id);
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
