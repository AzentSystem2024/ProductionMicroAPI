using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;
        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpPost]
        [Route("list")]
        public ItemsResponse List(MasterFilter? objFilter)
        {
            ItemsResponse res = new ItemsResponse();
            List<Items> items = new List<Items>();
            try
            {
                string apiKey = "";
                int intUserID = 1;

                if (objFilter == null)
                {
                    objFilter = new MasterFilter
                    {
                        MASTER_TYPE = "All",
                        MASTER_VALUE = ""
                    };
                }

                items = _itemsService.GetAllItems(intUserID, objFilter.MASTER_TYPE == "ActiveOnly", objFilter);

                res.flag = "1";
                res.message = "Success";
                res.data = items;
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }



        [HttpPost]
        [Route("select/{id:int}")]
        public Items select(int id)
        {
            Items objItems = new Items();
            try
            {
                
                objItems = _itemsService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objItems;
        }





        [HttpPost]
        [Route("insert")]
        public ItemResponse Insert(Items itemsData)
        {
            ItemResponse res = new ItemResponse();

            try
            {
                
                _itemsService.Insert(itemsData);
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
        [Route("update")]
        public ItemResponse Update(Items itemsData)
        {
            ItemResponse res = new ItemResponse();

            try
            {
                
                _itemsService.Update(itemsData);
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
        [Route("delete/{id:int}")]
        public ItemsResponse Delete(int id)
        {
            ItemsResponse res = new ItemsResponse();

            try
            {
                

                _itemsService.DeleteItem(id);
                res.flag = "1";
                res.message = "Success";
                //res.data = _itemsService.GetItems(id);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }



        [HttpPost]
        [Route("aliasduplicate")]
        public ItemsResponse AliasDuplicate(ALIAS_DUPLICATE vInput)
        {
            ItemsResponse res = new ItemsResponse();

            try
            {
                
                res = _itemsService.Alias(vInput);
            }
            catch (Exception ex)
            {

            }

            return res;
        }
    }
}
