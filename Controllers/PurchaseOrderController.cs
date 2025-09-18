using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }
        [HttpPost]
        [Route("supplierlist")]
        public List<SupplierList> SuuplierList(SupplierInput input)
        {
            List<SupplierList> itemCategories = new List<SupplierList>();

            try
            {
                
                itemCategories = _purchaseOrderService.GetSupplier(input);
            }
            catch (Exception ex)
            {
            }
            return itemCategories.ToList();
        }

        [HttpPost]
        [Route("list")]
        public PurchaseOrderResponse List()
        {
            PurchaseOrderResponse res = new PurchaseOrderResponse();
            List<PurchaseOrderHeader> itemBrands = new List<PurchaseOrderHeader>();

            try
            {
                string apiKey = "";
                Int32 intUserID = 1;

                /*
                foreach (var header in Request.Headers)
                {
                    if (header.Key == "x-api-key")
                        apiKey = header.Value.ToList()[0];
                }

                
                
                User_DAL userDAL = new User_DAL();
                Int32 intUserID = userDAL.GetUserIDWithToken(apiKey);
                if (intUserID < 1)
                {
                    res.flag = "0";
                    res.message = "Invalid authorization key";
                    return res;
                }

                */

                
                itemBrands = _purchaseOrderService.GetPOList(intUserID);

                res.flag = 1;
                res.message = "Success";
                res.data = itemBrands;
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("insert")]
        public PurchaseOrderResponse Insert(PurchaseOrderHeader Data)
        {
            PurchaseOrderResponse res = new PurchaseOrderResponse();

            try
            {
                
                _purchaseOrderService.Insert(Data);
                res.flag = 1;
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("update")]
        public PurchaseOrderResponse Update(PurchaseOrderHeader Data)
        {
            PurchaseOrderResponse res = new PurchaseOrderResponse();

            try
            {
                
                _purchaseOrderService.Update(Data);
                res.flag = 1;
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public PurchaseOrderHeader Select(int id)
        {
            PurchaseOrderHeader objScheme = new PurchaseOrderHeader();
            try
            {
                
                objScheme = _purchaseOrderService.GetPurchaseOrder(id);
            }
            catch (Exception ex)
            {

            }

            return objScheme;
        }

        [HttpPost]
        [Route("verify")]
        public PurchaseOrderResponse Verify(PurchaseOrderHeader Data)
        {
            PurchaseOrderResponse res = new PurchaseOrderResponse();

            try
            {
                
                _purchaseOrderService.Verify(Data);
                res.flag = 1;
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("approval")]
        public PurchaseOrderResponse Approve(PurchaseOrderHeader Data)
        {
            PurchaseOrderResponse res = new PurchaseOrderResponse();

            try
            {
                
                _purchaseOrderService.Approval(Data);
                res.flag = 1;
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("itemlist")]
        public List<ItemList> ItemList(ItemInput input)
        {
            List<ItemList> itemlist = new List<ItemList>();

            try
            {
                
                itemlist = _purchaseOrderService.GetItemList(input);
            }
            catch (Exception ex)
            {
            }
            return itemlist.ToList();
        }
        [HttpPost]
        [Route("POno")]
        public PurchaseDoc GetLastDocNo()
        {
            PurchaseDoc res = new PurchaseDoc();

            try
            {
                res = _purchaseOrderService.GetLastDocNo();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
