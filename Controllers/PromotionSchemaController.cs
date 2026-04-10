using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionSchemaController : ControllerBase
    {
        private readonly IPromotionSchemaService _promotionSchemaService;
        public PromotionSchemaController(IPromotionSchemaService promotionSchemaService)
        {
            _promotionSchemaService = promotionSchemaService;
        }
        [HttpPost]
        [Route("list")]
        public PromotionSchemaResponse Pricelist()
        {
            PromotionSchemaResponse res = new PromotionSchemaResponse();
            List<PromotionSchema> pricelist = new List<PromotionSchema>();

            try
            {
                string apiKey = "";
                Int32 intUserID = 1;

                ;
                pricelist = _promotionSchemaService.GetAllItemBrand();

                res.flag = "1";
                res.message = "Success";
                res.promotion_data = pricelist;
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
        public PromotionSchemaResponse Insert(PromotionSchema scheme)
        {
            PromotionSchemaResponse res = new PromotionSchemaResponse();
            try
            {
                ;
                Int32 ModuleID = _promotionSchemaService.Insert(scheme);

                res.flag = "1";
                res.message = "Success";
                //res.data = _promotionSchemaService.GetPriceItem(ModuleID);
            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("update")]
        public PromotionSchemaResponse Update(PromotionSchema scheme)
        {
            PromotionSchemaResponse res = new PromotionSchemaResponse();
            try
            {
                ;
                Int32 ModuleID = _promotionSchemaService.Update(scheme);

                res.flag = "1";
                res.message = "Success";
                //res.data = _promotionSchemaService.GetPriceItem(ModuleID);
            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;

            }

            return res;
        }


        [HttpPost]
        [Route("select/{id:int}")]
        public PromotionSchema PromotionSelect(int id)
        {
            PromotionSchema objScheme = new PromotionSchema();
            try
            {
                ;
                objScheme = _promotionSchemaService.GetSchema(id);
            }
            catch (Exception ex)
            {

            }

            return objScheme;
        }


        [HttpPost]
        [Route("delete/{id:int}")]
        public PromotionSchemaResponse DeletePrice(int id)
        {
            PromotionSchemaResponse res = new PromotionSchemaResponse();

            try
            {
                ;

                bool isDeleted = _promotionSchemaService.DeleteSchema(id);

                if (isDeleted)
                {
                    res.flag = "1";
                    res.message = "Success";
                }
                else
                {
                    res.flag = "0";
                    res.message = "Cannot delete.. Promotion is in use..";
                }
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }
    }
}
