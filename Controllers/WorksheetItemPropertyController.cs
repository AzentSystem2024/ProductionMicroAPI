using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorksheetItemPropertyController : ControllerBase
    {
        private readonly IWorksheetItemPropertyService _worksheetItemPropertyService;
        public WorksheetItemPropertyController(IWorksheetItemPropertyService worksheetItemPropertyService)
        {
            _worksheetItemPropertyService = worksheetItemPropertyService;
        }
        [HttpPost]
        [Route("list")]
        public WorksheetItemPropertiesResponse List()
        {
            WorksheetItemPropertiesResponse res = new WorksheetItemPropertiesResponse();
            List<WorksheetItem> worksheetItems = new List<WorksheetItem>();

            try
            {
                
                worksheetItems = _worksheetItemPropertyService.GetAllWorksheetItem();

                res.flag = "1";
                res.message = "Success";
                res.dataworksheet = worksheetItems;
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("insert")]
        public WorksheetItem Insert(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.Insert(Data);
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
        public WorksheetItem Update(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.Update(Data);
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
        [Route("delete/{id:int}")]
        public WorksheetItemPropertiesResponse Delete(int id)
        {
            WorksheetItemPropertiesResponse res = new WorksheetItemPropertiesResponse();

            try
            {
                

                _worksheetItemPropertyService.DeleteItemProperty(id);
                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("verify")]
        public WorksheetItem Verify(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.Verify(Data);
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
        public WorksheetItem Approve(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.Approval(Data);
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
        public WorksheetItem Select(int id)
        {
            WorksheetItem objItems = new WorksheetItem();
            try
            {
                
                objItems = _worksheetItemPropertyService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objItems;
        }


        [HttpPost]
        [Route("itempropertylist")]
        public ItemStorePropertiesResponse ItemPropertyList()
        {
            ItemStorePropertiesResponse res = new ItemStorePropertiesResponse();
            List<ItemStoreProperties> liststore = new List<ItemStoreProperties>();

            try
            {
                
                liststore = _worksheetItemPropertyService.GetItemPropertyList();

                res.flag = "1";
                res.message = "Success";
                res.data = liststore;
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }


        [HttpPost]
        [Route("itempricepropertylist")]
        public ItemPricePropertiesResponse ItemPriceList()
        {
            ItemPricePropertiesResponse res = new ItemPricePropertiesResponse();
            List<ItemPriceProperties> liststore = new List<ItemPriceProperties>();

            try
            {
                
                liststore = _worksheetItemPropertyService.GetItemPriceList();

                res.flag = "1";
                res.message = "Success";
                res.data = liststore;
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("Pricelist")]
        public WorksheetItemPropertiesResponse Pricelist()
        {
            WorksheetItemPropertiesResponse res = new WorksheetItemPropertiesResponse();
            List<WorksheetItem> pricelist = new List<WorksheetItem>();

            try
            {
                
                pricelist = _worksheetItemPropertyService.GetAllWorksheetItemPrice();

                res.flag = "1";
                res.message = "Success";
                res.dataworksheet = pricelist;
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }



        [HttpPost]
        [Route("insertprice")]
        public WorksheetItemPropertiesResponse InsertPrice(WorksheetItem menu)
        {
            WorksheetItemPropertiesResponse res = new WorksheetItemPropertiesResponse();
            try
            {
                
                Int32 ModuleID = _worksheetItemPropertyService.InsertPrice(menu);

                res.flag = "1";
                res.message = "Success";
                res.data = _worksheetItemPropertyService.GetPriceItem(ModuleID);
            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("updateprice")]
        public WorksheetItem UpdatePrice(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.UpdatePrice(Data);
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
        [Route("selectprice/{id:int}")]
        public WorksheetItem SelectItemPrice(int id)
        {
            WorksheetItem objItems = new WorksheetItem();
            try
            {
                
                objItems = _worksheetItemPropertyService.GetPriceItem(id);
            }
            catch (Exception ex)
            {

            }

            return objItems;
        }



        [HttpPost]
        [Route("deleteprice/{id:int}")]
        public ItemPricePropertiesResponse DeletePrice(int id)
        {
            ItemPricePropertiesResponse res = new ItemPricePropertiesResponse();

            try
            {
                

                _worksheetItemPropertyService.DeleteItemPrice(id);
                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("Verifyprice")]
        public WorksheetItem VerifyPrice(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.VerifyPrice(Data);
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
        [Route("Approvalprice")]
        public WorksheetItem ApprovePrice(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.ApprovalPrice(Data);
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
        [Route("Promotionlist")]
        public WorksheetItemPropertiesResponse Promotionlist()
        {
            WorksheetItemPropertiesResponse res = new WorksheetItemPropertiesResponse();
            List<WorksheetItem> pricelist = new List<WorksheetItem>();

            try
            {
                
                pricelist = _worksheetItemPropertyService.GetAllWorksheetPromotionSchema();

                res.flag = "1";
                res.message = "Success";
                res.dataworksheet = pricelist;
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("insertpromotion")]
        public WorksheetItem InsertPromotion(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.InsertPromotion(Data);
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
        [Route("updatepromotion")]
        public WorksheetItem UpdatePromotion(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.UpdatePromotion(Data);
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
        [Route("deletepromotion/{id:int}")]
        public ItemPricePropertiesResponse DeletePromotion(int id)
        {
            ItemPricePropertiesResponse res = new ItemPricePropertiesResponse();

            try
            {
                

                _worksheetItemPropertyService.DeletePromotion(id);
                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("Verifypromotion")]
        public WorksheetItem VerifyPromotion(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.VerifyPromotion(Data);
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
        [Route("approvepromotion")]
        public WorksheetItem ApprovePromotion(WorksheetItem Data)
        {
            WorksheetItem res = new WorksheetItem();

            try
            {
                
                _worksheetItemPropertyService.ApprovePromotion(Data);
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
        [Route("selectpromotion/{id:int}")]
        public WorksheetItem SelectItemPromotion(int id)
        {
            WorksheetItem objItems = new WorksheetItem();
            try
            {
                
                objItems = _worksheetItemPropertyService.GetPromotionItem(id);
            }
            catch (Exception ex)
            {

            }

            return objItems;
        }
    }
}
