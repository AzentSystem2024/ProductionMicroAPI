using Microsoft.AspNetCore.Authorization;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using System.Data.SqlClient;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseInvoiceController : ControllerBase
    {
        private readonly IPurchaseInvoiceService _PurchaseInvoiceService;
        public PurchaseInvoiceController(IPurchaseInvoiceService PurchaseInvoiceService)
        {
            _PurchaseInvoiceService = PurchaseInvoiceService;
        }
        [HttpPost]
        [Route("GetPendingPoList")]
        public PIDropdownResponce PendingPoList(PIDropdownInput input)
        {
            PIDropdownResponce res = new PIDropdownResponce();
            try
            {

                var result = _PurchaseInvoiceService.GetPendingPoList(input);

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
        [Route("GetGrnDetails")]
        public GRNDetailResponce POList(GRNDetailInput input)
        {
            GRNDetailResponce res = new GRNDetailResponce();
            try
            {



                var result = _PurchaseInvoiceService.GetSelectedPoDetailS(input);


                res.Flag = 1;
                res.Message = "Success";
                res.GRNDetails = result.GRNDetails;

                return res; // Return the response
            }
            catch (Exception ex)
            {

                res.Flag = 0;
                res.Message = "Error: " + ex.Message; // Include error message


                return res; // Return the error response
            }
        }
        [HttpPost]
        [Route("insert")]
        public PurchResponce Insert(PurchHeader Data)
        {
            PurchResponce res = new PurchResponce();

            try
            {

                _PurchaseInvoiceService.Insert(Data);
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
        public PurchResponce Update(PurchHeader Data)
        {
            PurchResponce res = new PurchResponce();

            try
            {

                _PurchaseInvoiceService.Update(Data);
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
        public PurchSelectResponce select(int id)
        {
            PurchSelectResponce res = new PurchSelectResponce();
            try
            {

                res.Data = _PurchaseInvoiceService.GetPurchaseInvoiceById(id);
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
        [Route("delete/{id:int}")]
        public PurchResponce Delete(int id)
        {
            PurchResponce res = new PurchResponce();

            try
            {


                _PurchaseInvoiceService.Delete(id);
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
        [Route("verify")]
        public PurchResponce Verify(PurchHeader Data)
        {
            PurchResponce res = new PurchResponce();

            try
            {

                _PurchaseInvoiceService.Verify(Data);
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
        public PurchResponce List()
        {
            PurchResponce res = new PurchResponce();

            try
            {
                List<PurchaseInvoice> purchInvoice = _PurchaseInvoiceService.GetPurchaseInvoiceList(); 

                res.Flag = 1;
                res.Message = "Success";
                res.PurchHeaders = purchInvoice;
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
                res.PurchHeaders = new List<PurchaseInvoice>();
            }

            return res;
        }


        [HttpPost]
        [Route("approve")]
        public PurchResponce Approve(PurchHeader Data)
        {
            PurchResponce res = new PurchResponce();

            try
            {

                _PurchaseInvoiceService.Approve(Data);
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
