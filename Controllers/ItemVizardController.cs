using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailApi.DAL.Interfaces;
using RetailApi.Models;

namespace RetailApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemVizardController : ControllerBase
    {
        private readonly IItemVizardService _itemVizardService;
        public ItemVizardController(IItemVizardService itemVizardService)
        {
            _itemVizardService = itemVizardService;
        }
        [HttpPost]
        [Route("list")]
        public ItemVizardResponse List(ItemVizardInput vizardInput)
        {
            ItemVizardResponse res = new ItemVizardResponse();
            List<ItemVizard> items = new List<ItemVizard>();
            try
            {
                string apiKey = "";
                Int32 intUserID = 1;


                
                items = _itemVizardService.GetAllItems(vizardInput);

                res.flag = 1;
                res.message = "Success";
                res.data = items;
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
        public ItemVizardResponse Insert(ItemVizardStore itemsData)
        {
            ItemVizardResponse res = new ItemVizardResponse();
            //ItemVizardStore res = new ItemVizardStore();

            try
            {
                
                _itemVizardService.Insert(itemsData);
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
        [Route("itempricewizard")]
        public ItemVizardResponse ListItemPriceWizard(ItemPriceWizardInput vizardInput)
        {
            ItemVizardResponse res = new ItemVizardResponse();
            List<ItemPriceWizard> priceWizards = new List<ItemPriceWizard>();
            try
            {
                string apiKey = "";
                Int32 intUserID = 1;


                
                priceWizards = _itemVizardService.GetAllItemPriceWizard(vizardInput);

                res.flag = 1;
                res.message = "Success";
                res.PriceWizardData = priceWizards;
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }
    }
}
