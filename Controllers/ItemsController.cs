using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;
        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        //[HttpPost]
        //[Route("list")]
        //public ItemsResponse List(DateRequest objFilter)
        //{
        //    ItemsResponse res = new ItemsResponse();
        //    List<Items> items = new List<Items>();

        //    try
        //    {
        //        // Ensure filter is not null
        //        if (objFilter == null)
        //        {
        //            objFilter = new DateRequest
        //            {
        //                DATE_FROM = null,
        //                DATE_TO = null
        //            };
        //        }

        //        // Prepare request for service
        //        DateRequest request = new DateRequest
        //        {
        //            DATE_FROM = objFilter.DATE_FROM ?? DateTime.MinValue,
        //            DATE_TO = objFilter.DATE_TO ?? DateTime.MinValue
        //        };

        //        // Get items from service
        //        items = _itemsService.GetAllItems(request);

        //        res.flag = "1";
        //        res.message = "Success";
        //        res.data = items;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.flag = "0";
        //        res.message = ex.Message;
        //        res.data = null;
        //    }

        //    return res;
        //}
        [HttpPost]
        [Route("list")]
        public ItemsResponse List()
        {
            ItemsResponse res = new ItemsResponse();
            try
            {
                var items = _itemsService.GetAllItems();

                res.flag = "1";
                res.message = "Success";
                res.data = items;
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                res.data = null;
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
            var (flag, msg) = _itemsService.Insert(itemsData);

            return new ItemResponse
            {
                flag = flag.ToString(),
                message = msg
            };
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
